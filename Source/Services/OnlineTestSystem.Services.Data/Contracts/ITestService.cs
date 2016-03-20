namespace OnlineTestSystem.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Linq;
    using OnlineTestSystem.Data.Common.Models;
    using OnlineTestSystem.Data.Models;


    public interface ITestService
    {
        IQueryable<Test> GetAll();

        IList<TestListingModelFromDB> GetAllCompletedForUser(string id);

        Test Create(Test test);

        IQueryable<Test> GetTestByTitle(string title);

        IList<Answer> GetCorrectAnswersForATestByTitle(string title);

        Test Update(int testId, Question question);

        bool SubmitTest(CompletedTest test);

        bool HaveCompletedTest(int testId, string userId);
    }
}
