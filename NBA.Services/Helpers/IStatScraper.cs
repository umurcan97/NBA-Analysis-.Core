namespace NBA.Services.Helpers
{
    using System.Collections.Generic;
    using NBA.Models;
    using NBA.Models.Entities;
    using OpenQA.Selenium.Chrome;
    public interface IStatScraper
    {
        GameTime DateScraper(ChromeDriver driver, int GameNo, string url);
        List<Players> PlayerInfoScraper(ChromeDriver driver, int StartNo);
        FullSeason GameScraper(ChromeDriver driver, int GameNo, string url);
        FullSeason20_21 GameScraper20_21(ChromeDriver driver, int GameNo, string url);
        FullSeason19_20 GameScraper19_20(ChromeDriver driver, int GameNo, string url);
        FullSeasonQuarters QuarterScraper(ChromeDriver driver, int GameNo, int QuarterNo);
        FullSeasonQuarters20_21 QuarterScraper20_21(ChromeDriver driver, int GameNo, int QuarterNo);
        FullSeasonQuarters19_20 QuarterScraper19_20(ChromeDriver driver, int GameNo, int QuarterNo);
        List<PlayerStats> PlayerStatScraper(ChromeDriver driver, int GameNo);
        List<PlayerStats20_21> PlayerStatScraper20_21(ChromeDriver driver, int GameNo);
        List<PlayerStats19_20> PlayerStatScraper19_20(ChromeDriver driver, int GameNo);
        List<PlayerStatsQuarter> PlayerStatQuarterScraper(ChromeDriver driver, int GameNo, int QuarterNo);
        List<PlayerStatsQuarter20_21> PlayerStatQuarterScraper20_21(ChromeDriver driver, int GameNo, int QuarterNo);
        List<PlayerStatsQuarter19_20> PlayerStatQuarterScraper19_20(ChromeDriver driver, int GameNo, int QuarterNo);
    }
}
