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
        public DbSet<PlayerStatsQuarter> PlayerStatsQuarter { get; set; }
        public DbSet<GameTime> GameTime { get; set; }
        public DbSet<FullSeason20_21> FullSeason20_21 { get; set; }
        public DbSet<FullSeasonQuarters20_21> FullSeasonQuarters20_21 { get; set; }
        public DbSet<GameTime20_21> GameTime20_21 { get; set; }
        public DbSet<PlayerStats20_21> PlayerStats20_21 { get; set; }
        public DbSet<PlayerStatsQuarter20_21> PlayerStatsQuarter20_21 { get; set; }
        public DbSet<FullSeason19_20> FullSeason19_20 { get; set; }
        public DbSet<FullSeasonQuarters19_20> FullSeasonQuarters19_20 { get; set; }
        public DbSet<GameTime19_20> GameTime19_20 { get; set; }
        public DbSet<PlayerStats19_20> PlayerStats19_20 { get; set; }
        public DbSet<PlayerStatsQuarter19_20> PlayerStatsQuarter19_20 { get; set; }
        public DbSet<GameTime18_19> GameTime18_19 { get; set; }
        public DbSet<GamePredictions> GamePredictions { get; set; }
        public DbSet<QuarterPredictions> QuarterPredictions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source = LAPTOP-IGSRU4M0; Initial Catalog = NBA; Integrated Security=True;");
        }
    }
}
