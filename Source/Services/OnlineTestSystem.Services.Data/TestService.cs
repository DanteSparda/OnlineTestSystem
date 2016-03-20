namespace OnlineTestSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using OnlineTestSystem.Data.Common;
    using OnlineTestSystem.Data.Common.Models;
    using OnlineTestSystem.Data.Models;
    using OnlineTestSystem.Services.Data.Contracts;

    public class TestService : ITestService
    {
        private readonly IDbRepository<Test> tests;
        private readonly IDbRepository<CompletedTest> completedTests;

        public TestService(IDbRepository<Test> tests, IDbRepository<CompletedTest> completedTests)
        {
            this.tests = tests;
            this.completedTests = completedTests;
        }

        public Test Create(Test test)
        {
            this.tests.Add(test);
            try
            {
                this.tests.Save();
            }
            catch (Exception)
            {
                return null;
            }

            return test;
        }

        public IQueryable<Test> GetAll()
        {
            return this.tests.All();
        }

        public IList<Answer> GetCorrectAnswersForATestByTitle(string title)
        {
            var desiredTest = this.tests.All().Where(x => x.Title == title).FirstOrDefault();
            var answersToReturn = new List<Answer>();
            foreach (var question in desiredTest.Questions)
            {
                answersToReturn.Add(question.Answers.Where(x => x.IsCorrect).FirstOrDefault());
            }

            return answersToReturn;
        }

        public IQueryable<Test> GetTestByTitle(string title)
        {
            return this.tests.All().Where(x => x.Title == title);
        }

        public bool SubmitTest(CompletedTest test)
        {
            this.completedTests.Add(test);
            this.completedTests.Save();
            return true;
        }

        public Test Update(int testId, Question question)
        {
            var test = this.tests.GetById(testId);
            test.Questions.Add(question);
            this.tests.Update(test);
            this.tests.Save();

            return test;
        }

        public IList<TestListingModelFromDB> GetAllCompletedForUser(string id)
        {
            return this.completedTests.All().Where(x => x.UserId == id).Select(x => new TestListingModelFromDB{ TestId = x.TestId, Percentage = x.Percentage }).ToList();
        }

        public bool HaveCompletedTest(int testId, string userId)
        {
            return this.completedTests.All().Where(x => x.TestId == testId && x.UserId == userId).Any();
        }
    }
}
