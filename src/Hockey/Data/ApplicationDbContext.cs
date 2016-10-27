using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Hockey.Models;

namespace Hockey.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }
        public DbSet<Player> Player { get; set; }
        public DbSet<Conference> Conference { get; set; }
        public DbSet<Division> Division { get; set; }
        public DbSet<CardManufacture> CardManufacture { get; set; }
        public DbSet<Position> Position { get; set; }
        public DbSet<NhlTeam> NhlTeam { get; set; }
        public DbSet<TeamImage> TeamImage {get;set;}
        public DbSet<Season> Season { get; set; }
        public DbSet<Image> Image { get; set; }
        public DbSet<Nationality> Nationality { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
