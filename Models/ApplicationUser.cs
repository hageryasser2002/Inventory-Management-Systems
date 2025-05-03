using Microsoft.AspNetCore.Identity;

namespace InventorySystem.Models
{
    public enum UserRole
    {
        Admin,
        Manager,
        User
    }

    public class ApplicationUser:IdentityUser
    {
        public UserRole Role { get; set; }
    }
}
