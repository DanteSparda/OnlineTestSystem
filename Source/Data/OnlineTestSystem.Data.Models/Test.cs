namespace OnlineTestSystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using OnlineTestSystem.Data.Common.Models;

    public class Test : BaseModel<int>
    {
        public Test()
        {
            this.Questions = new HashSet<Question>();
            this.CompletedByUsers = new HashSet<CompletedTest>();
        }

        [Index(IsUnique = true)]
        [MaxLength(400)]
        [Required]
        public string Title { get; set; }

        public virtual ICollection<Question> Questions { get; set; }

        public virtual ICollection<CompletedTest> CompletedByUsers { get; set; }
    }
}
