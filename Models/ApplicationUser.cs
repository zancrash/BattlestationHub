using Microsoft.AspNetCore.Identity;

namespace BattlestationHub.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string DisplayName { get; set; } = string.Empty;
    }
}
