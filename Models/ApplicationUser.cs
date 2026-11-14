using Microsoft.AspNetCore.Identity;

namespace CinemaFind.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? DisplayName { get; set; }

        public string? ProfilePictureUrl { get; set; }
    }
}