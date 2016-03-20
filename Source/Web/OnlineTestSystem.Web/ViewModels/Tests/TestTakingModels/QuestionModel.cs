namespace OnlineTestSystem.Web.ViewModels.Tests.TestTakingModels
{
    using System.Collections.Generic;
    using AutoMapper;
    using OnlineTestSystem.Data.Models;
    using OnlineTestSystem.Web.Infrastructure.Mapping;

    public class QuestionModel : IMapFrom<Question>, IHaveCustomMappings
    {
        public string Content { get; set; }

        public IList<AnswerModel> Answers { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Question, QuestionModel>()
                .ForMember(x => x.Answers, opts => opts.MapFrom(x => x.Answers));
        }
    }
}
