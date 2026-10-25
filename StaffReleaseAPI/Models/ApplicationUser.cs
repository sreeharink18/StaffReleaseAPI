using Microsoft.AspNetCore.Identity;

namespace StaffReleaseAPI.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
