namespace OnlineTestSystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using OnlineTestSystem.Data.Common.Models;

    public class CompletedTest : BaseModel<int>
    {
        public string UserId { get; set; }

        public virtual User User { get; set; }

        public int TestId { get; set; }

        public virtual Test Test { get; set; }

        [Range(0, 100)]
        public double Percentage { get; set; }
    }
}
