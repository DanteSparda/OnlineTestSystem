namespace OnlineTestSystem.Web.ViewModels.Tests
{
    using System.Linq;
    using System.Web;
    using AutoMapper;
    using Microsoft.AspNet.Identity;
    using OnlineTestSystem.Data.Models;
    using OnlineTestSystem.Web.Infrastructure.Mapping;

    public class TestListingModel : IMapFrom<Test>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string TotalQuestions { get; set; }

        public bool Completed { get; set; }

        public double Percentage { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            var userId = string.Empty;
            if (HttpContext.Current.User != null)
            {
                userId = HttpContext.Current.User.Identity.GetUserId();
            }

            configuration.CreateMap<Test, TestListingModel>()
                .ForMember(x => x.Completed, opts => opts.MapFrom(g => g.CompletedByUsers.Any(x => x.UserId == userId)))
                .ForMember(x => x.Percentage, opts => opts
                    .MapFrom(g => g.CompletedByUsers
                        .Where(x => x.UserId == userId)
                        .Select(x => x.Percentage)
                        .FirstOrDefault()))
                .ForMember(x => x.TotalQuestions, opts => opts.MapFrom(g => g.Questions.Count));
        }
    }
}
