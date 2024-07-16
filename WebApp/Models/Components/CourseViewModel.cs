using Infrastructure.Entities;

namespace WebApp.Models.Components;

public class CourseViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Ingress { get; set; } = null!;
    public decimal Price { get; set; }
    public int HoursToComplete { get; set; }
    public int LikesPercentage { get; set; }
    public int LikesAmount { get; set; }
    public bool IsSaved { get; set; } = false;
    public AuthorViewModel CourseAuthor { get; set; } = new AuthorViewModel();
    public ImageViewModel Image { get; set; } = new ImageViewModel();
    public CategoryViewModel CourseCategory { get; set; } = null!;


    public static implicit operator CourseViewModel(CourseEntity course)
    {
        return new CourseViewModel
        {
            Id = course.Id,
            Name = course.Name,
            Description = course.Description,
            Ingress = course.Ingress,
            Price = course.Price,
            HoursToComplete = course.HoursToComplete,
            LikesPercentage = course.LikesPercentage,
            LikesAmount = course.LikesAmount,

            Image = new ImageViewModel
            {
                ImageUrl = course.Image.ImageUrl
            },

            CourseAuthor = new AuthorViewModel
            {
                Id = course.CourseAuthorId,
                Name = course.CourseAuthor.Name,
                Description = course.CourseAuthor.Description,
                YoutubeFollowersQty = course.CourseAuthor.YoutubeFollowersQty,
                FacebookFollowersQty = course.CourseAuthor.FacebookFollowersQty,

                Image = new ImageViewModel
                {
                    ImageUrl = course.CourseAuthor.Image.ImageUrl
                }
            },

            CourseCategory = new CategoryViewModel
            {
                Id = course.CourseCategory.Id,
                Name = course.CourseCategory.Name,
            }
        };
    }
}
