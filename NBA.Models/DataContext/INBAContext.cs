namespace NBA.Models.DataContext
{
    using NBA.Models.Entities;
    using Microsoft.EntityFrameworkCore;
    public interface INBAContext
    {
        DbSet<FullSeason> FullSeason { get; set; }
        DbSet<FullSeasonQuarters> FullSeasonQuarters { get; set; }
        DbSet<Teams> Teams { get; set; }
        DbSet<Players> Players { get; set; }
        DbSet<PlayerStats> PlayerStats { get; set; }
        DbSet<PlayerStatsQuarter> PlayerStatsQuarter { get; set; }
        DbSet<GameTime> GameTime { get; set; }
        DbSet<FullSeason20_21> FullSeason20_21 { get; set; }
        DbSet<FullSeasonQuarters20_21> FullSeasonQuarters20_21 { get; set; }
        DbSet<GameTime20_21> GameTime20_21 { get; set; }
        DbSet<PlayerStats20_21> PlayerStats20_21 { get; set; }
        DbSet<PlayerStatsQuarter20_21> PlayerStatsQuarter20_21 { get; set; }
        DbSet<FullSeason19_20> FullSeason19_20 { get; set; }
        DbSet<FullSeasonQuarters19_20> FullSeasonQuarters19_20 { get; set; }
        DbSet<GameTime19_20> GameTime19_20 { get; set; }
        DbSet<PlayerStats19_20> PlayerStats19_20 { get; set; }
        DbSet<PlayerStatsQuarter19_20> PlayerStatsQuarter19_20 { get; set; }
        DbSet<GameTime18_19> GameTime18_19 { get; set; }
        DbSet<GamePredictions> GamePredictions { get; set; }
        DbSet<QuarterPredictions> QuarterPredictions { get; set; }
    }
}
