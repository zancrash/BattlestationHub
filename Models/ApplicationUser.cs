using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BattlestationHub.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required(ErrorMessage = "Display name is required.")]
        [StringLength(50, ErrorMessage = "Display name cannot exceed 50 characters.")]
        [UniqueDisplayName]
        public string DisplayName { get; set; } = string.Empty;
    }
}
