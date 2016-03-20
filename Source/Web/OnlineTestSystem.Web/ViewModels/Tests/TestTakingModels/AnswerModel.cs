namespace OnlineTestSystem.Web.ViewModels.Tests.TestTakingModels
{
    using OnlineTestSystem.Data.Models;
    using OnlineTestSystem.Web.Infrastructure.Mapping;

    public class AnswerModel : IMapFrom<Answer>
    {
        public int Id { get; set; }

        public bool IsCorrect { get; set; }

        public bool Checked { get; set; }

        public string Content { get; set; }
    }
}
