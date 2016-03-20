namespace OnlineTestSystem.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Caching;
    using System.Web.Mvc;
    using Data.Models;
    using Microsoft.AspNet.Identity;
    using OnlineTestSystem.Common;
    using OnlineTestSystem.Services.Data.Contracts;
    using OnlineTestSystem.Web.Infrastructure.Mapping;
    using OnlineTestSystem.Web.ViewModels.Tests.TestTakingModels;

    [Authorize]
    public class TestEngineController : BaseController
    {
        private ITestService tests;

        public TestEngineController(ITestService tests)
        {
            this.tests = tests;
        }

        [HttpGet]
        public ActionResult TakeTest(string title, int question = 1)
        {
            var test = this.tests.GetTestByTitle(title).To<TestTakingModel>().FirstOrDefault();

            if (test == null || test.Questions.Count < question)
            {
                this.TempData[GlobalConstants.MessageNameError] = "The test you're trying to open doesn't excist!";
                return this.Redirect("/");
            }

            // If the user've already completed the test
            if (this.tests.HaveCompletedTest(test.Id, this.User.Identity.GetUserId()))
            {
                this.TempData[GlobalConstants.MessageNameError] = "You've already done this test!";
                return this.Redirect("/");
            }

            var listOfCompleted = new List<int>();
            var username = this.User.Identity.Name;
            var saveString = $"{username}-{title}-{question}";

            // Gets the completed questions for information on the submit button
            for (int i = 1; i <= test.Questions.Count; i++)
            {
                if (HttpRuntime.Cache[$"{username}-{title}-{i}"] != null)
                {
                    listOfCompleted.Add(i);
                }
            }

            test.CompletedQuestions = listOfCompleted;

            // Gets the answers that've already been marked
            if (HttpRuntime.Cache[saveString] != null)
            {
                int answerId = (int)HttpRuntime.Cache[saveString];
                test.Questions[question - 1].Answers.Where(x => x.Id == answerId).FirstOrDefault().Checked = true;
            }

            test.QuestionNumber = question;

            // Randomizes the answers so they don't appear in the same order
            test.Questions[question - 1].Answers = test.Questions[question - 1].Answers.OrderBy(x => Guid.NewGuid()).ToList();

            return this.View(test);
        }

        /// <summary>
        /// Adding the selected answer to the cache
        /// </summary>
        /// <param name="id">Id of the answer</param>
        /// <param name="title">Test title</param>
        /// <param name="question">Question number</param>
        /// <returns>Bool for success</returns>
        [HttpPost]
        public ActionResult SaveAnswer(int id, string title, int question)
        {
            var username = this.User.Identity.Name;
            var saveString = $"{username}-{title}-{question}";
            HttpRuntime.Cache.Insert(
                saveString,
                id,
                null,
                DateTime.Now.AddSeconds(24 * 60 * 60),
                Cache.NoSlidingExpiration);
            return this.Json(new { success = true });
        }

        [HttpPost]
        public ActionResult SubmitTest(string title)
        {
            var correctAnswers = this.tests.GetCorrectAnswersForATestByTitle(title).Select(x => x.Id).ToList();
            var totalQuestions = correctAnswers.Count();
            var gotRight = 0;
            var answersIds = new List<int>();
            var username = this.User.Identity.Name;

            // Geting the all answers from the cache
            for (int i = 1; i <= totalQuestions; i++)
            {
                var saveString = $"{username}-{title}-{i}";
                if (saveString != null)
                {
                    var answerId = (int)HttpRuntime.Cache[saveString];
                    answersIds.Add(answerId);

                    // Checks if the answer is correct
                    if (correctAnswers.Contains(answerId))
                    {
                        gotRight++;
                    }

                    // Clears the cache
                    HttpRuntime.Cache.Remove(saveString);
                }
            }

            if (answersIds.Count < totalQuestions)
            {
                this.TempData[GlobalConstants.MessageNameError] = "Not all questions are answered!";
                return this.RedirectToRoute("/TakeTest/" + title);
            }

            var percentage = (gotRight / (double)totalQuestions) * 100;
            var testId = this.tests.GetTestByTitle(title).FirstOrDefault().Id;
            var completed = new CompletedTest()
            {
                Percentage = percentage,
                TestId = testId,
                UserId = this.User.Identity.GetUserId()
            };

            var addedToDatabase = this.tests.SubmitTest(completed);
            if (!addedToDatabase)
            {
                this.TempData[GlobalConstants.MessageNameError] = "Something went wrong, sorry";
                return this.Json(new { success = true });
            }

            this.TempData[GlobalConstants.MessageNameSuccess] = "You solved " + title;
            return this.Json(new { success = true });
        }
    }
}
