namespace OnlineTestSystem.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Infrastructure.Mapping;
    using Microsoft.AspNet.Identity;
    using Services.Data.Contracts;
    using ViewModels.Tests;

    [Authorize]
    public class HomeController : BaseController
    {
        private readonly ITestService tests;

        public HomeController(ITestService tests)
        {
            this.tests = tests;
        }

        public ActionResult Index()
        {
            try
            {
                // If the DB is missing it will throw
                var testsToDisplay = this.tests.GetAll().To<TestListingModel>().ToList();
                var testsDone = this.tests.GetAllCompletedForUser(this.User.Identity.GetUserId());
                foreach (var test in testsToDisplay)
                {
                    foreach (var done in testsDone)
                    {
                        if (done.TestId == test.Id)
                        {
                            test.Completed = true;
                            test.Percentage = done.Percentage;
                        }
                    }
                }

                return this.View(testsToDisplay);
            }
            catch (Exception)
            {
                return this.View(new List<TestListingModel>());
            }
        }
    }
}
