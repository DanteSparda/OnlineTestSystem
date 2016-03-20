namespace OnlineTestSystem.Web.ViewModels.Tests.TestTakingModels
{
    using OnlineTestSystem.Data.Models;
    using OnlineTestSystem.Web.Infrastructure.Mapping;

    public class AnswerGradeModel : IMapFrom<Answer>
    {
        public int Id { get; set; }
    }
}
