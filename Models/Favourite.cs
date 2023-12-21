namespace BattlestationHub.Models
{
    public class Favourite
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public int SetupId { get; set; }

        // Navigation properties
        public ApplicationUser User { get; set; }
        public Setup Setup { get; set; }

        public Favourite()
        {

        }

        // Method to check if the setup is saved for a specific user
        public bool IsSaved(string userId, int setupId)
        {
            return UserId == userId && SetupId == setupId;
        }
    }
}
