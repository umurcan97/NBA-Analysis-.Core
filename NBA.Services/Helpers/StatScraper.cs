namespace NBA.Services.Helpers
{
    using System;
    using System.Threading;
    using NBA.Models;
    using NBA.Models.Entities;
    using OpenQA.Selenium.Firefox;
    using System.Collections.Generic;

    public class StatScraper : IStatScraper
    {
        private readonly IHelpers _helpers;

        public StatScraper(IHelpers helpers)
        {
            this._helpers = helpers;
        }
        public GameTime DateScraper(FirefoxDriver driver, int GameNo, string url)
        {
            driver.Navigate().GoToUrl(url);
            GameTime game = new GameTime();
            string date = "/html/body/div[1]/div[2]/section/div[1]/div[5]/div/div[2]/p[1]";
            game.GameDate = _helpers.DateTimeConverter(driver.FindElementByXPath(date).Text);
            game.GameNo = GameNo;
            return game;
        }
        public List<Players> PlayerInfoScraper(FirefoxDriver driver, int StartNo)
        {
            List<Players> players = new List<Players>();
            driver.Navigate().GoToUrl("https://www.nba.com/players");
            Thread.Sleep(5000);
            driver.FindElementByXPath("/html/body/div[2]/div[3]/div/div/div[2]/div/div/button").Click();
            Thread.Sleep(500);
            driver.FindElementByXPath("/html/body/div[1]/div[2]/div[3]/section/div/div[2]/div[1]/div[7]/div/div[3]/div/label/div/select/option[1]").Click();
            string asd = driver
                .FindElementByXPath("/html/body/div[1]/div[2]/div[3]/section/div/div[2]/div[1]/div[7]/div/div[1]")
                .Text;
            asd = asd.Substring(0, asd.IndexOf(' '));
            int totalplayerno = int.Parse(asd);
            for (int i = StartNo; i <= totalplayerno; i++)
            {
                Players player = new Players();
                player.Name = driver
                    .FindElementByXPath(
                        "/html/body/div[1]/div[2]/div[3]/section/div/div[2]/div[2]/div/div/div/table/tbody/tr[" + i +
                        "]/td[1]/a/div[2]").Text.Replace("\n", " ").Replace("\r", "");
                try
                {
                    player.Team = _helpers.GetTeamEnumByTeamShortName(driver.FindElementByXPath(
                        "/html/body/div[1]/div[2]/div[3]/section/div/div[2]/div[2]/div/div/div/table/tbody/tr[" + i +
                        "]/td[2]/a").Text);
                }
                catch (Exception)
                {
                    player.Team = Team.Error;
                }
                try
                {
                    player.Number = int.Parse(driver
                        .FindElementByXPath(
                            "/html/body/div[1]/div[2]/div[3]/section/div/div[2]/div[2]/div/div/div/table/tbody/tr[" + i +
                            "]/td[3]").Text);
                }
                catch (Exception)
                {
                    player.Number = -1;
                }
                player.Position = driver
                    .FindElementByXPath(
                        "/html/body/div[1]/div[2]/div[3]/section/div/div[2]/div[2]/div/div/div/table/tbody/tr[" + i +
                        "]/td[4]")
                    .Text;
                string height = driver.FindElementByXPath(
                    "/html/body/div[1]/div[2]/div[3]/section/div/div[2]/div[2]/div/div/div/table/tbody/tr[" + i +
                    "]/td[5]").Text;
                int feet = int.Parse(height.Substring(0, 1));
                int inches = int.Parse(height.Remove(0, 2));
                player.Height = Convert.ToInt16(Math.Round(30.48 * feet + 2.54 * inches));
                string weight = driver
                    .FindElementByXPath(
                        "/html/body/div[1]/div[2]/div[3]/section/div/div[2]/div[2]/div/div/div/table/tbody/tr[" + i +
                        "]/td[6]").Text;
                player.Weight = Convert.ToInt16(Math.Round(int.Parse(weight.Substring(0, 3)) / 2.2));
                player.Country = driver
                    .FindElementByXPath(
                        "/html/body/div[1]/div[2]/div[3]/section/div/div[2]/div[2]/div/div/div/table/tbody/tr[" + i +
                        "]/td[8]").Text;
                players.Add(player);
            }
            return players;
        }
    }
}
