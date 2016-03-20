namespace OnlineTestSystem.Web.ViewModels.Tests.TestTakingModels
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using AutoMapper;
    using Microsoft.AspNet.Identity;
    using OnlineTestSystem.Data.Models;
    using OnlineTestSystem.Web.Infrastructure.Mapping;

    public class TestGradeModel : IMapFrom<Test>, IHaveCustomMappings
    {
        public string Title { get; set; }

        public IList<QuestionGradeModel> Questions { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Test, TestGradeModel>()
                .ForMember(x => x.Questions, opts => opts.MapFrom(x => x.Questions));
        }
    }
}
