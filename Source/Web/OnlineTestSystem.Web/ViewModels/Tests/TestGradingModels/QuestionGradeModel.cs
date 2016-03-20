namespace OnlineTestSystem.Web.ViewModels.Tests.TestTakingModels
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using OnlineTestSystem.Data.Models;
    using OnlineTestSystem.Web.Infrastructure.Mapping;

    public class QuestionGradeModel : IMapFrom<Question>, IHaveCustomMappings
    {
        public IList<AnswerModel> Answers { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Question, QuestionModel>()
                .ForMember(x => x.Answers, opts => opts.MapFrom(x => x.Answers.Select(a => a.IsCorrect)));
        }
    }
}
