using System.ComponentModel.DataAnnotations.Schema;

namespace BattlestationHub.Models
{
    public class Setup
    {
        public int Id { get; set; }

        public string UserId {  get; set; } = string.Empty;

        public string SetupName { get; set; }

        public string SetupDesc { get; set; }

        [NotMapped]
        public IFormFile SetupImgFile { get; set; }

        public string ImgPath { get; set; } = string.Empty;

        public ICollection<Favourite> Favourites { get; set; } = new List<Favourite>();

        public Setup()
        {
            
        }

        // Method to check if the setup is saved for a specific user
        public bool IsSaved(string userId, int setupId)
        {
            Console.WriteLine("Favourites count: " + Favourites.Count);
            return Favourites.Any(f => f.UserId == userId && f.SetupId == setupId);
        }
    }
}
