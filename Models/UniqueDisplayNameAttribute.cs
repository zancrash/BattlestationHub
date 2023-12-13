using BattlestationHub.Data;
using System.ComponentModel.DataAnnotations;

namespace BattlestationHub.Models
{
    public class UniqueDisplayNameAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var dbContext = (ApplicationDbContext)validationContext.GetService(typeof(ApplicationDbContext));

            var displayName = (string)value;
            var existingUser = dbContext.Users.FirstOrDefault(u => u.DisplayName == displayName);

            if (existingUser != null)
            {
                return new ValidationResult("Display name is already in use.");
            }

            return ValidationResult.Success;
        }
    }
}
