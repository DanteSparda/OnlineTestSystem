namespace OnlineTestSystem.Web.ViewModels.Tests
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class TestCreateModel
    {
        [Required]
        [MaxLength(400)]
        [Index(IsUnique = true)]
        public string Title { get; set; }

        public ICollection<QuestionCreateModel> Questions { get; set; }
    }
}
