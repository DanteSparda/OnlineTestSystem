namespace OnlineTestSystem.Web.ViewModels.Tests
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using OnlineTestSystem.Common;

    public class QuestionCreateModel
    {
        public int TestId { get; set; }

        [MaxLength(500)]
        [Required]
        public string Content { get; set; }

        [QuestionValidate(ErrorMessage ="The question must have 4 answers one of which must be true!")]
        public ICollection<AnswerCreateModel> Answers { get; set; }
    }
}
