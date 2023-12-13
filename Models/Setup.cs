using System.ComponentModel.DataAnnotations.Schema;

namespace BattlestationHub.Models
{
    public class Setup
    {
        public int Id { get; set; }

        public string SetupName { get; set; }

        public string SetupDesc { get; set; }

        [NotMapped]
        public IFormFile SetupImgFile { get; set; }

        public string ImgPath { get; set; } = string.Empty;

        public Setup()
        {
            
        }
    }
}
