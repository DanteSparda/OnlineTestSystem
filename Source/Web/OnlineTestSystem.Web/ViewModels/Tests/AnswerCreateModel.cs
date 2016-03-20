namespace OnlineTestSystem.Web.ViewModels.Tests
{
    using System.ComponentModel.DataAnnotations;

    public class AnswerCreateModel
    {
        [MaxLength(200, ErrorMessage = "The answer can't be longer than 200 symbuls!")]
        [Required]
        public string Content { get; set; }

        public bool IsCorrect { get; set; }
    }
}
