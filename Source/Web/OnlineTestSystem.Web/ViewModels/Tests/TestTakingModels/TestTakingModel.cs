namespace OnlineTestSystem.Web.ViewModels.Tests.TestTakingModels
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using AutoMapper;
    using Microsoft.AspNet.Identity;
    using OnlineTestSystem.Data.Models;
    using OnlineTestSystem.Web.Infrastructure.Mapping;

    public class TestTakingModel : IMapFrom<Test>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int QuestionNumber { get; set; }

        public IList<QuestionModel> Questions { get; set; }

        public IList<int> CompletedQuestions { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            var userId = string.Empty;
            if (HttpContext.Current.User != null)
            {
                userId = HttpContext.Current.User.Identity.GetUserId();
            }

            configuration.CreateMap<Test, TestTakingModel>()
                .ForMember(x => x.Questions, opts => opts.MapFrom(x => x.Questions));
        }
    }
}
