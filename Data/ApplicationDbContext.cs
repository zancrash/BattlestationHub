using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BattlestationHub.Models;

namespace BattlestationHub.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<BattlestationHub.Models.Setup> Battlestation { get; set; } = default!;

        public DbSet<Favourite> Favourites { get; set; }

    }
}
