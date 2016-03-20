namespace OnlineTestSystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Common.Models;

    public class Question : BaseModel<int>
    {
        public Question()
        {
            this.Answers = new HashSet<Answer>();
        }

        [MaxLength(500)]
        [Required]
        public string Content { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }
    }
}
