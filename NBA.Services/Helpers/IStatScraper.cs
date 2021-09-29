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
        FullSeason19_20 GameScraper19_20(ChromeDriver driver, int GameNo, string url);
        FullSeason20_21 GameScraper20_21(ChromeDriver driver, int GameNo, string url);
    }
}
