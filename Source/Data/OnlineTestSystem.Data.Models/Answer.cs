namespace OnlineTestSystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using OnlineTestSystem.Data.Common.Models;

    public class Answer : BaseModel<int>
    {
        public int QuestionId { get; set; }

        public virtual Question Question { get; set; }

        public bool IsCorrect { get; set; }

        [MaxLength(200)]
        [Required]
        public string Content { get; set; }
    }
}
