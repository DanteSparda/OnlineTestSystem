namespace HelpMyBook.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using OnlineTestSystem.Common;
    using OnlineTestSystem.Data.Models;
    using OnlineTestSystem.Services.Data.Contracts;
    using OnlineTestSystem.Web.Controllers;
    using OnlineTestSystem.Web.Infrastructure.Mapping;
    using OnlineTestSystem.Web.ViewModels.Tests;

    [Authorize]
    public class TestsController : BaseController
    {
        private readonly ITestService tests;

        public TestsController(ITestService tests)
        {
            this.tests = tests;
        }

        [HttpGet]
        public ActionResult Create()
        {
            return this.View();
        }

        [HttpGet]
        public ActionResult EditAll()
        {
            var testsToDisplay = this.tests.GetAll().To<TestListingModel>().ToList();
            return this.View(testsToDisplay);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TestCreateModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var test = new Test()
            {
                Title = model.Title
            };

            var createdTest = this.tests.Create(test);

            if (createdTest == null)
            {
                this.ModelState.AddModelError("Title", "The title you're trying to use already exicts!");
                return this.View(model);
            }

            this.TempData[GlobalConstants.MessageNameSuccess] = "You've successfuly created a test!";
            return this.RedirectToAction("AddQuestion", new { id = createdTest.Id });
        }

        [HttpGet]
        public ActionResult AddQuestion(int id)
        {
            var model = new QuestionCreateModel() { TestId = id };
            return this.View(model);
        }

        [HttpPost]
        public ActionResult AddQuestion(QuestionCreateModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var question = new Question() { Content = model.Content };
            foreach (var item in model.Answers)
            {
                var answer = new Answer() { Content = item.Content, IsCorrect = item.IsCorrect };
                question.Answers.Add(answer);
            }

            this.tests.Update(model.TestId, question);

            this.TempData[GlobalConstants.MessageNameSuccess] = "You've successfuly added a question!";
            return this.View();
        }
    }
}
