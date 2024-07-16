namespace Infrastructure.Entities;

public class CourseAuthorEntity
{
    public int Id {  get; set; }

    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;

    public int YoutubeFollowersQty { get; set; }
    public int FacebookFollowersQty { get; set; }

    public int ImageId { get; set; }
    public CourseAuthorImageEntity Image { get; set; } = null!;
}