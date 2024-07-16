namespace Infrastructure.Entities;

public class SavedCoursesEntity
{
    public int CourseId { get; set; }
    public string UserProfileId { get; set; } = null!;
}
