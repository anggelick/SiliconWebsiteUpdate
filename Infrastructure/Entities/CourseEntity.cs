using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Entities;

public class CourseEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Ingress { get; set; } = null!;

    [DataType("money")]
    public decimal Price { get; set; }
    public int HoursToComplete { get; set; }
    public int LikesPercentage { get; set; }
    public int LikesAmount { get; set; }

    public int CourseCategoryId { get; set; }
    public virtual CourseCategoryEntity? CourseCategory { get; set; }

    public int ImageId { get; set; }
    public virtual CourseImageEntity? Image { get; set; }

    public int CourseAuthorId { get; set; }
    public virtual CourseAuthorEntity? CourseAuthor { get; set; }


}
