using Infrastructure.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities
{
    [Table("UserProfiles")]
    public class UserProfileEntity
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;

        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Sorry try again")]
        public string Email { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public string? Bio { get; set; }

        public int? AddressId { get; set; }
        public AddressEntity? Address { get; set; }

        public int? ProfilePictureId { get; set; }
        public ProfilePictureEntity? ProfilePicture { get; set; }

        public ICollection<SavedCoursesEntity>? SavedItems { get; set; }

        public virtual ApplicationUser User { get; set; } = null!;
    }
}