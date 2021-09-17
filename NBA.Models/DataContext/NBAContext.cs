namespace NBA.Models.DataContext
{
    using Microsoft.EntityFrameworkCore;
    using NBA.Models.Entities;

    public class NBAContext : DbContext, INBAContext
    {
        public DbSet<FullSeason> FullSeason { get; set; }
        public DbSet<FullSeasonQuarters> FullSeasonQuarters { get; set; }
        public DbSet<Teams> Teams { get; set; }
        public DbSet<Players> Players { get; set; }
        public DbSet<PlayerStats> PlayerStats { get; set; }
        public DbSet<GameTime> GameTime { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source = LAPTOP-IGSRU4M0; Initial Catalog = NBA; Integrated Security=True;");
        }
    }
}
