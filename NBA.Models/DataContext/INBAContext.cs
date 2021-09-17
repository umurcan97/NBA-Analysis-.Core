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
        DbSet<GameTime> GameTime { get; set; }
    }
}
