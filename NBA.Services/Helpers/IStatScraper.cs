namespace NBA.Services.Helpers
{
    using System.Collections.Generic;
    using NBA.Models;
    using NBA.Models.Entities;
    using OpenQA.Selenium.Firefox;
    public interface IStatScraper
    {
        GameTime DateScraper(FirefoxDriver driver, int GameNo, string url);
        List<Players> PlayerInfoScraper(FirefoxDriver driver, int StartNo);
    }
}
