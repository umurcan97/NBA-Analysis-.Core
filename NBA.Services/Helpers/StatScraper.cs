namespace NBA.Services.Helpers
{
    using System;
    using System.Threading;
    using NBA.Models;
    using NBA.Services.Interfaces;
    using NBA.Models.Entities;
    using OpenQA.Selenium.Firefox;
    using System.Collections.Generic;
    using OpenQA.Selenium.Chrome;
    using NBA.Services.Abstraction;

    public class StatScraper : IStatScraper
    {
        private readonly IHelpers _helpers;
        private readonly IGameTimesRepository _gameTimesRepository;

        public StatScraper(IHelpers helpers,
            IGameTimesRepository gameTimesRepository)
        {
            this._helpers = helpers;
            this._gameTimesRepository = gameTimesRepository;
        }
        public GameTime DateScraper(ChromeDriver driver, int GameNo, string url)
        {
            driver.Navigate().GoToUrl(url);
            GameTime game = new GameTime();
            string date = "/html/body/div[1]/div[2]/section/div[1]/div[5]/div/div[2]/p[1]";
            game.GameDate = _helpers.DateTimeConverter(driver.FindElement(OpenQA.Selenium.By.XPath(date)).Text);
            game.GameNo = GameNo;
            return game;
        }
        public List<Players> PlayerInfoScraper(ChromeDriver driver, int StartNo)
        {
            List<Players> players = new List<Players>();
            driver.Navigate().GoToUrl("https://www.nba.com/players");
            //Thread.Sleep(5000);
            //driver.FindElement(OpenQA.Selenium.By.XPath("/html/body/div[2]/div[3]/div/div/div[2]/div/div/button")).Click();
            Thread.Sleep(500);
            driver.FindElement(OpenQA.Selenium.By.XPath("/html/body/div[1]/div[2]/div[3]/section/div/div[2]/div[1]/div[7]/div/div[3]/div/label/div/select/option[1]")).Click();
            string asd = driver
                .FindElement(OpenQA.Selenium.By.XPath("/html/body/div[1]/div[2]/div[3]/section/div/div[2]/div[1]/div[7]/div/div[1]")
                ).Text;
            asd = asd.Substring(0, asd.IndexOf(' '));
            int totalplayerno = int.Parse(asd);
            for (int i = StartNo; i <= totalplayerno; i++)
            {
                Players player = new Players();
                player.Name = driver
                    .FindElement(OpenQA.Selenium.By.XPath(
                        "/html/body/div[1]/div[2]/div[3]/section/div/div[2]/div[2]/div/div/div/table/tbody/tr[" + i +
                        "]/td[1]/a/div[2]")).Text.Replace("\n", " ").Replace("\r", "");
                try
                {
                    player.Team = _helpers.GetTeamEnumByTeamShortName(driver.FindElement(OpenQA.Selenium.By.XPath(
                        "/html/body/div[1]/div[2]/div[3]/section/div/div[2]/div[2]/div/div/div/table/tbody/tr[" + i +
                        "]/td[2]/a")).Text);
                }
                catch (Exception)
                {
                    player.Team = Team.Error;
                }
                try
                {
                    player.Number = int.Parse(driver
                        .FindElement(OpenQA.Selenium.By.XPath(
                            "/html/body/div[1]/div[2]/div[3]/section/div/div[2]/div[2]/div/div/div/table/tbody/tr[" + i +
                            "]/td[3]")).Text);
                }
                catch (Exception)
                {
                    player.Number = -1;
                }
                player.Position = driver
                    .FindElement(OpenQA.Selenium.By.XPath(
                        "/html/body/div[1]/div[2]/div[3]/section/div/div[2]/div[2]/div/div/div/table/tbody/tr[" + i +
                        "]/td[4]")
                    ).Text;
                string height = driver.FindElement(OpenQA.Selenium.By.XPath(
                    "/html/body/div[1]/div[2]/div[3]/section/div/div[2]/div[2]/div/div/div/table/tbody/tr[" + i +
                    "]/td[5]")).Text;
                int feet = int.Parse(height.Substring(0, 1));
                int inches = int.Parse(height.Remove(0, 2));
                player.Height = Convert.ToInt16(Math.Round(30.48 * feet + 2.54 * inches));
                string weight = driver
                    .FindElement(OpenQA.Selenium.By.XPath(
                        "/html/body/div[1]/div[2]/div[3]/section/div/div[2]/div[2]/div/div/div/table/tbody/tr[" + i +
                        "]/td[6]")).Text;
                player.Weight = Convert.ToInt16(Math.Round(int.Parse(weight.Substring(0, 3)) / 2.2));
                player.Country = driver
                    .FindElement(OpenQA.Selenium.By.XPath(
                        "/html/body/div[1]/div[2]/div[3]/section/div/div[2]/div[2]/div/div/div/table/tbody/tr[" + i +
                        "]/td[8]")).Text;
                players.Add(player);
            }
            return players;
        }
        public FullSeason GameScraper(ChromeDriver driver, int GameNo, string url)
        {
            driver.Navigate().GoToUrl(url);
            Thread.Sleep(1000);
            FullSeason stat = new FullSeason();
            var result = (ServiceResult<GameTime>)_gameTimesRepository.GetGameTime(GameNo);
            stat.GameDate = result.Data;
            string AwayPITP =
                "/html/body/div[1]/div[2]/div[4]/section[1]/div/div/table[2]/tbody/tr[1]/td[1]";
            string AwayFastBreakPoints =
                "/html/body/div[1]/div[2]/div[4]/section[1]/div/div/table[2]/tbody/tr[1]/td[2]";
            string AwayBenchPoints =
                "/html/body/div[1]/div[2]/div[4]/section[1]/div/div/table[2]/tbody/tr[1]/td[4]";
            string AwayTeamRebounds =
                "/html/body/div[1]/div[2]/div[4]/section[1]/div/div/table[2]/tbody/tr[1]/td[5]";
            string AwayTeamTurnovers =
                "/html/body/div[1]/div[2]/div[4]/section[1]/div/div/table[2]/tbody/tr[1]/td[7]";
            string AwayPointsofTO =
                "/html/body/div[1]/div[2]/div[4]/section[1]/div/div/table[2]/tbody/tr[1]/td[8]";
            string HomePITP =
                "/html/body/div[1]/div[2]/div[4]/section[1]/div/div/table[2]/tbody/tr[2]/td[1]";
            string HomeFastBreakPoints =
                "/html/body/div[1]/div[2]/div[4]/section[1]/div/div/table[2]/tbody/tr[2]/td[2]";
            string HomeBenchPoints =
                "/html/body/div[1]/div[2]/div[4]/section[1]/div/div/table[2]/tbody/tr[2]/td[4]";
            string HomeTeamRebounds =
                "/html/body/div[1]/div[2]/div[4]/section[1]/div/div/table[2]/tbody/tr[2]/td[5]";
            string HomeTeamTurnovers =
                "/html/body/div[1]/div[2]/div[4]/section[1]/div/div/table[2]/tbody/tr[2]/td[7]";
            string HomePointsofTO =
                "/html/body/div[1]/div[2]/div[4]/section[1]/div/div/table[2]/tbody/tr[2]/td[8]";
            stat.AwayPITP = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayPITP)).Text);
            stat.AwayFastBreakPoints = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayFastBreakPoints)).Text);
            stat.AwayBenchPoints = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayBenchPoints)).Text);
            int awayteamreb = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayTeamRebounds)).Text);
            int awayteamto = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayTeamTurnovers)).Text);
            stat.AwayPointsofTO = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayPointsofTO)).Text);
            stat.HomePITP = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomePITP)).Text);
            stat.HomeFastBreakPoints = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeFastBreakPoints)).Text);
            stat.HomeBenchPoints = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeBenchPoints)).Text);
            int hometeamreb = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeTeamRebounds)).Text);
            int hometeamto = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeTeamTurnovers)).Text);
            stat.HomePointsofTO = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomePointsofTO)).Text);
            driver.Navigate().GoToUrl(url + "/box-score?range=0-28800");
            Thread.Sleep(1000);
            int line = 25;
            for (; line >= 5; line--)
            {
                try
                {
                    int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath("/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[3]/a")).Text);
                    break;
                }
                catch (Exception)
                {
                    continue;
                }
            }

            string AwayTeam =
                "/html/body/div[1]/div[2]/div[4]/section[2]/div[1]/h1/span";
            string HomeTeam =
                "/html/body/div[1]/div[2]/div[4]/section[3]/div[1]/h1/span";
            string AwayFGM =
                "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[3]";
            string AwayFGA =
                "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[4]";
            string Away3PM =
                "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[6]";
            string Away3PA =
                "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[7]";
            string AwayFTM =
                "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[9]";
            string AwayFTA =
                "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[10]";
            string AwayOffensiveRebounds =
                "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[12]";
            string AwayDefensiveRebounds =
                "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[13]";
            string AwayAssists =
                "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[15]";
            string AwaySteals =
                "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[16]";
            string AwayBlocks =
                "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[17]";
            string AwayTurnovers =
                "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[18]";
            line = 25;
            for (; line >= 5; line--)
            {
                try
                {
                    int.Parse(driver
                        .FindElement(OpenQA.Selenium.By.XPath(
                            "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + line +
                            "]/td[3]/a")).Text);
                    break;
                }
                catch (Exception)
                {
                    continue;
                }
            }

            string HomeFGM =
                "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[3]";
            string HomeFGA =
                "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[4]";
            string Home3PM =
                "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[6]";
            string Home3PA =
                "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[7]";
            string HomeFTM =
                "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[9]";
            string HomeFTA =
                "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[10]";
            string HomeOffensiveRebounds =
                "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[12]";
            string HomeDefensiveRebounds =
                "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[13]";
            string HomeAssists =
                "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[15]";
            string HomeSteals =
                "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[16]";
            string HomeBlocks =
                "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[17]";
            string HomeTurnovers =
                "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[18]";
            stat.GameNo = GameNo;
            stat.HomeTeam = _helpers.GetTeamEnumByTeamName(driver.FindElement(OpenQA.Selenium.By.XPath(HomeTeam)).Text.Replace(" ", "").ToUpper()
                .Replace('İ', 'I'));
            stat.AwayTeam = _helpers.GetTeamEnumByTeamName(driver.FindElement(OpenQA.Selenium.By.XPath(AwayTeam)).Text.Replace(" ", "").ToUpper()
                .Replace('İ', 'I'));
            try
            {
                stat.HomeFGA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeFGA)).Text);
            }
            catch (Exception)
            {
                stat.HomeFGA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeFGA + "/a")).Text);
            }

            try
            {
                stat.HomeFGM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeFGM)).Text);
            }
            catch (Exception)
            {
                stat.HomeFGM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeFGM + "/a")).Text);
            }

            try
            {
                stat.Home3PA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(Home3PA)).Text);
            }
            catch (Exception)
            {
                stat.Home3PA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(Home3PA + "/a")).Text);
            }

            try
            {
                stat.Home3PM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(Home3PM)).Text);
            }
            catch (Exception)
            {
                stat.Home3PM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(Home3PM + "/a")).Text);
            }

            try
            {
                stat.HomeFTA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeFTA)).Text);
            }
            catch (Exception)
            {
                stat.HomeFTA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeFTA + "/a")).Text);
            }

            try
            {
                stat.HomeFTM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeFTM)).Text);
            }
            catch (Exception)
            {
                stat.HomeFTM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeFTM + "/a")).Text);
            }

            try
            {
                stat.HomeAssists = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeAssists)).Text);
            }
            catch (Exception)
            {
                stat.HomeAssists = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeAssists + "/a")).Text);
            }

            try
            {
                stat.HomeDefensiveRebounds = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeDefensiveRebounds)).Text);
            }
            catch (Exception)
            {
                stat.HomeDefensiveRebounds = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeDefensiveRebounds + "/a")).Text);
            }

            try
            {
                stat.HomeOffensiveRebounds = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeOffensiveRebounds)).Text);
            }
            catch (Exception)
            {
                stat.HomeOffensiveRebounds = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeOffensiveRebounds + "/a")).Text);
            }

            try
            {
                stat.HomeBlocks = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeBlocks)).Text);
            }
            catch (Exception)
            {
                stat.HomeBlocks = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeBlocks + "/a")).Text);
            }

            try
            {
                stat.HomeTurnovers = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeTurnovers)).Text) + hometeamto;
            }
            catch (Exception)
            {
                stat.HomeTurnovers = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeTurnovers + "/a")).Text) + hometeamto;
            }

            try
            {
                stat.HomeSteals = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeSteals)).Text);
            }
            catch (Exception)
            {
                stat.HomeSteals = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeSteals + "/a")).Text);
            }

            stat.HomePoints = stat.HomeFGM * 2 + stat.Home3PM + stat.HomeFTM;
            stat.HomeTotalRebounds = stat.HomeDefensiveRebounds + stat.HomeOffensiveRebounds + hometeamreb;
            try
            {
                stat.AwayFGA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayFGA)).Text);
            }
            catch (Exception)
            {
                stat.AwayFGA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayFGA + "/a")).Text);
            }

            try
            {
                stat.AwayFGM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayFGM)).Text);
            }
            catch (Exception)
            {
                stat.AwayFGM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayFGM + "/a")).Text);
            }

            try
            {
                stat.Away3PA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(Away3PA)).Text);
            }
            catch (Exception)
            {
                stat.Away3PA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(Away3PA + "/a")).Text);
            }

            try
            {
                stat.Away3PM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(Away3PM)).Text);
            }
            catch (Exception)
            {
                stat.Away3PM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(Away3PM + "/a")).Text);
            }

            try
            {
                stat.AwayFTA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayFTA)).Text);
            }
            catch (Exception)
            {
                stat.AwayFTA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayFTA + "/a")).Text);
            }

            try
            {
                stat.AwayFTM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayFTM)).Text);
            }
            catch (Exception)
            {
                stat.AwayFTM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayFTM + "/a")).Text);
            }

            try
            {
                stat.AwayAssists = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayAssists)).Text);
            }
            catch (Exception)
            {
                stat.AwayAssists = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayAssists + "/a")).Text);
            }

            try
            {
                stat.AwayDefensiveRebounds = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayDefensiveRebounds)).Text);
            }
            catch (Exception)
            {
                stat.AwayDefensiveRebounds = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayDefensiveRebounds + "/a")).Text);
            }

            try
            {
                stat.AwayOffensiveRebounds = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayOffensiveRebounds)).Text);
            }
            catch (Exception)
            {
                stat.AwayOffensiveRebounds = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayOffensiveRebounds + "/a")).Text);
            }

            try
            {
                stat.AwayBlocks = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayBlocks)).Text);
            }
            catch (Exception)
            {
                stat.AwayBlocks = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayBlocks + "/a")).Text);
            }

            try
            {
                stat.AwayTurnovers = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayTurnovers)).Text) + awayteamto;
            }
            catch (Exception)
            {
                stat.AwayTurnovers = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayTurnovers + "/a")).Text) + awayteamto;
            }

            try
            {
                stat.AwaySteals = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwaySteals)).Text);
            }
            catch (Exception)
            {
                stat.AwaySteals = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwaySteals + "/a")).Text);
            }

            stat.AwayPoints = stat.AwayFGM * 2 + stat.Away3PM + stat.AwayFTM;
            stat.AwayTotalRebounds = stat.AwayDefensiveRebounds + stat.AwayOffensiveRebounds + awayteamreb;
            stat.OverTime = false || stat.HomePoints == stat.AwayPoints;
            return stat;
        }
        public FullSeason20_21 GameScraper20_21(ChromeDriver driver, int GameNo, string url)
        {
            driver.Navigate().GoToUrl(url);
            Thread.Sleep(3000);
            FullSeason20_21 stat = new FullSeason20_21();
            var result = (ServiceResult<GameTime20_21>)_gameTimesRepository.GetGameTime20_21(GameNo);
            stat.GameDate = result.Data;
            string AwayPITP =
                "/html/body/div[1]/div[2]/div[4]/section[1]/div/div/table[2]/tbody/tr[1]/td[1]";
            string AwayFastBreakPoints =
                "/html/body/div[1]/div[2]/div[4]/section[1]/div/div/table[2]/tbody/tr[1]/td[2]";
            string AwayBenchPoints =
                "/html/body/div[1]/div[2]/div[4]/section[1]/div/div/table[2]/tbody/tr[1]/td[4]";
            string AwayTeamRebounds =
                "/html/body/div[1]/div[2]/div[4]/section[1]/div/div/table[2]/tbody/tr[1]/td[5]";
            string AwayTeamTurnovers =
                "/html/body/div[1]/div[2]/div[4]/section[1]/div/div/table[2]/tbody/tr[1]/td[7]";
            string AwayPointsofTO =
                "/html/body/div[1]/div[2]/div[4]/section[1]/div/div/table[2]/tbody/tr[1]/td[8]";
            string HomePITP =
                "/html/body/div[1]/div[2]/div[4]/section[1]/div/div/table[2]/tbody/tr[2]/td[1]";
            string HomeFastBreakPoints =
                "/html/body/div[1]/div[2]/div[4]/section[1]/div/div/table[2]/tbody/tr[2]/td[2]";
            string HomeBenchPoints =
                "/html/body/div[1]/div[2]/div[4]/section[1]/div/div/table[2]/tbody/tr[2]/td[4]";
            string HomeTeamRebounds =
                "/html/body/div[1]/div[2]/div[4]/section[1]/div/div/table[2]/tbody/tr[2]/td[5]";
            string HomeTeamTurnovers =
                "/html/body/div[1]/div[2]/div[4]/section[1]/div/div/table[2]/tbody/tr[2]/td[7]";
            string HomePointsofTO =
                "/html/body/div[1]/div[2]/div[4]/section[1]/div/div/table[2]/tbody/tr[2]/td[8]";
            stat.AwayPITP = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayPITP)).Text);
            stat.AwayFastBreakPoints = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayFastBreakPoints)).Text);
            stat.AwayBenchPoints = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayBenchPoints)).Text);
            int awayteamreb = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayTeamRebounds)).Text);
            int awayteamto = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayTeamTurnovers)).Text);
            stat.AwayPointsofTO = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayPointsofTO)).Text);
            stat.HomePITP = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomePITP)).Text);
            stat.HomeFastBreakPoints = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeFastBreakPoints)).Text);
            stat.HomeBenchPoints = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeBenchPoints)).Text);
            int hometeamreb = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeTeamRebounds)).Text);
            int hometeamto = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeTeamTurnovers)).Text);
            stat.HomePointsofTO = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomePointsofTO)).Text);
            driver.Navigate().GoToUrl(url + "/box-score?range=0-28800");
            Thread.Sleep(1000);
            int line = 16;
            for (; line >= 5; line--)
            {
                try
                {
                    int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath("/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[3]/a")).Text);
                    break;
                }
                catch (Exception)
                {
                    continue;
                }
            }

            string AwayTeam =
                "/html/body/div[1]/div[2]/div[4]/section[2]/div[1]/h1/span";
            string HomeTeam =
                "/html/body/div[1]/div[2]/div[4]/section[3]/div[1]/h1/span";
            string AwayFGM =
                "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[3]";
            string AwayFGA =
                "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[4]";
            string Away3PM =
                "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[6]";
            string Away3PA =
                "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[7]";
            string AwayFTM =
                "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[9]";
            string AwayFTA =
                "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[10]";
            string AwayOffensiveRebounds =
                "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[12]";
            string AwayDefensiveRebounds =
                "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[13]";
            string AwayAssists =
                "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[15]";
            string AwaySteals =
                "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[16]";
            string AwayBlocks =
                "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[17]";
            string AwayTurnovers =
                "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[18]";
            line = 16;
            for (; line >= 5; line--)
            {
                try
                {
                    int.Parse(driver
                        .FindElement(OpenQA.Selenium.By.XPath(
                            "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + line +
                            "]/td[3]/a")).Text);
                    break;
                }
                catch (Exception)
                {
                    continue;
                }
            }

            string HomeFGM =
                "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[3]";
            string HomeFGA =
                "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[4]";
            string Home3PM =
                "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[6]";
            string Home3PA =
                "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[7]";
            string HomeFTM =
                "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[9]";
            string HomeFTA =
                "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[10]";
            string HomeOffensiveRebounds =
                "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[12]";
            string HomeDefensiveRebounds =
                "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[13]";
            string HomeAssists =
                "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[15]";
            string HomeSteals =
                "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[16]";
            string HomeBlocks =
                "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[17]";
            string HomeTurnovers =
                "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[18]";
            stat.GameNo = GameNo;
            stat.HomeTeam = _helpers.GetTeamEnumByTeamName(driver.FindElement(OpenQA.Selenium.By.XPath(HomeTeam)).Text.Replace(" ", "").ToUpper()
                .Replace('İ', 'I'));
            stat.AwayTeam = _helpers.GetTeamEnumByTeamName(driver.FindElement(OpenQA.Selenium.By.XPath(AwayTeam)).Text.Replace(" ", "").ToUpper()
                .Replace('İ', 'I'));
            try
            {
                stat.HomeFGA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeFGA)).Text);
            }
            catch (Exception)
            {
                stat.HomeFGA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeFGA + "/a")).Text);
            }

            try
            {
                stat.HomeFGM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeFGM)).Text);
            }
            catch (Exception)
            {
                stat.HomeFGM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeFGM + "/a")).Text);
            }

            try
            {
                stat.Home3PA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(Home3PA)).Text);
            }
            catch (Exception)
            {
                stat.Home3PA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(Home3PA + "/a")).Text);
            }

            try
            {
                stat.Home3PM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(Home3PM)).Text);
            }
            catch (Exception)
            {
                stat.Home3PM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(Home3PM + "/a")).Text);
            }

            try
            {
                stat.HomeFTA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeFTA)).Text);
            }
            catch (Exception)
            {
                stat.HomeFTA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeFTA + "/a")).Text);
            }

            try
            {
                stat.HomeFTM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeFTM)).Text);
            }
            catch (Exception)
            {
                stat.HomeFTM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeFTM + "/a")).Text);
            }

            try
            {
                stat.HomeAssists = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeAssists)).Text);
            }
            catch (Exception)
            {
                stat.HomeAssists = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeAssists + "/a")).Text);
            }

            try
            {
                stat.HomeDefensiveRebounds = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeDefensiveRebounds)).Text);
            }
            catch (Exception)
            {
                stat.HomeDefensiveRebounds = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeDefensiveRebounds + "/a")).Text);
            }

            try
            {
                stat.HomeOffensiveRebounds = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeOffensiveRebounds)).Text);
            }
            catch (Exception)
            {
                stat.HomeOffensiveRebounds = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeOffensiveRebounds + "/a")).Text);
            }

            try
            {
                stat.HomeBlocks = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeBlocks)).Text);
            }
            catch (Exception)
            {
                stat.HomeBlocks = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeBlocks + "/a")).Text);
            }

            try
            {
                stat.HomeTurnovers = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeTurnovers)).Text) + hometeamto;
            }
            catch (Exception)
            {
                stat.HomeTurnovers = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeTurnovers + "/a")).Text) + hometeamto;
            }

            try
            {
                stat.HomeSteals = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeSteals)).Text);
            }
            catch (Exception)
            {
                stat.HomeSteals = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeSteals + "/a")).Text);
            }

            stat.HomePoints = stat.HomeFGM * 2 + stat.Home3PM + stat.HomeFTM;
            stat.HomeTotalRebounds = stat.HomeDefensiveRebounds + stat.HomeOffensiveRebounds + hometeamreb;
            try
            {
                stat.AwayFGA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayFGA)).Text);
            }
            catch (Exception)
            {
                stat.AwayFGA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayFGA + "/a")).Text);
            }

            try
            {
                stat.AwayFGM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayFGM)).Text);
            }
            catch (Exception)
            {
                stat.AwayFGM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayFGM + "/a")).Text);
            }

            try
            {
                stat.Away3PA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(Away3PA)).Text);
            }
            catch (Exception)
            {
                stat.Away3PA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(Away3PA + "/a")).Text);
            }

            try
            {
                stat.Away3PM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(Away3PM)).Text);
            }
            catch (Exception)
            {
                stat.Away3PM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(Away3PM + "/a")).Text);
            }

            try
            {
                stat.AwayFTA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayFTA)).Text);
            }
            catch (Exception)
            {
                stat.AwayFTA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayFTA + "/a")).Text);
            }

            try
            {
                stat.AwayFTM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayFTM)).Text);
            }
            catch (Exception)
            {
                stat.AwayFTM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayFTM + "/a")).Text);
            }

            try
            {
                stat.AwayAssists = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayAssists)).Text);
            }
            catch (Exception)
            {
                stat.AwayAssists = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayAssists + "/a")).Text);
            }

            try
            {
                stat.AwayDefensiveRebounds = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayDefensiveRebounds)).Text);
            }
            catch (Exception)
            {
                stat.AwayDefensiveRebounds = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayDefensiveRebounds + "/a")).Text);
            }

            try
            {
                stat.AwayOffensiveRebounds = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayOffensiveRebounds)).Text);
            }
            catch (Exception)
            {
                stat.AwayOffensiveRebounds = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayOffensiveRebounds + "/a")).Text);
            }

            try
            {
                stat.AwayBlocks = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayBlocks)).Text);
            }
            catch (Exception)
            {
                stat.AwayBlocks = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayBlocks + "/a")).Text);
            }

            try
            {
                stat.AwayTurnovers = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayTurnovers)).Text) + awayteamto;
            }
            catch (Exception)
            {
                stat.AwayTurnovers = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayTurnovers + "/a")).Text) + awayteamto;
            }

            try
            {
                stat.AwaySteals = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwaySteals)).Text);
            }
            catch (Exception)
            {
                stat.AwaySteals = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwaySteals + "/a")).Text);
            }

            stat.AwayPoints = stat.AwayFGM * 2 + stat.Away3PM + stat.AwayFTM;
            stat.AwayTotalRebounds = stat.AwayDefensiveRebounds + stat.AwayOffensiveRebounds + awayteamreb;
            stat.OverTime = false || stat.HomePoints == stat.AwayPoints;
            return stat;
        }
        public FullSeason19_20 GameScraper19_20(ChromeDriver driver, int GameNo, string url)
        {
            driver.Navigate().GoToUrl(url);
            Thread.Sleep(3000);
            driver.Navigate().Refresh();
            FullSeason19_20 stat = new FullSeason19_20();
            var result = (ServiceResult<GameTime19_20>)_gameTimesRepository.GetGameTime19_20(GameNo);
            stat.GameDate = result.Data;
            string AwayPITP =
                "/html/body/div[1]/div[2]/div[4]/section[1]/div/div/table[2]/tbody/tr[1]/td[1]";
            string AwayFastBreakPoints =
                "/html/body/div[1]/div[2]/div[4]/section[1]/div/div/table[2]/tbody/tr[1]/td[2]";
            string AwayBenchPoints =
                "/html/body/div[1]/div[2]/div[4]/section[1]/div/div/table[2]/tbody/tr[1]/td[4]";
            string AwayTeamRebounds =
                "/html/body/div[1]/div[2]/div[4]/section[1]/div/div/table[2]/tbody/tr[1]/td[5]";
            string AwayTeamTurnovers =
                "/html/body/div[1]/div[2]/div[4]/section[1]/div/div/table[2]/tbody/tr[1]/td[7]";
            string AwayPointsofTO =
                "/html/body/div[1]/div[2]/div[4]/section[1]/div/div/table[2]/tbody/tr[1]/td[8]";
            string HomePITP =
                "/html/body/div[1]/div[2]/div[4]/section[1]/div/div/table[2]/tbody/tr[2]/td[1]";
            string HomeFastBreakPoints =
                "/html/body/div[1]/div[2]/div[4]/section[1]/div/div/table[2]/tbody/tr[2]/td[2]";
            string HomeBenchPoints =
                "/html/body/div[1]/div[2]/div[4]/section[1]/div/div/table[2]/tbody/tr[2]/td[4]";
            string HomeTeamRebounds =
                "/html/body/div[1]/div[2]/div[4]/section[1]/div/div/table[2]/tbody/tr[2]/td[5]";
            string HomeTeamTurnovers =
                "/html/body/div[1]/div[2]/div[4]/section[1]/div/div/table[2]/tbody/tr[2]/td[7]";
            string HomePointsofTO =
                "/html/body/div[1]/div[2]/div[4]/section[1]/div/div/table[2]/tbody/tr[2]/td[8]";
            stat.AwayPITP = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayPITP)).Text);
            stat.AwayFastBreakPoints = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayFastBreakPoints)).Text);
            stat.AwayBenchPoints = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayBenchPoints)).Text);
            int awayteamreb = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayTeamRebounds)).Text);
            int awayteamto = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayTeamTurnovers)).Text);
            stat.AwayPointsofTO = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayPointsofTO)).Text);
            stat.HomePITP = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomePITP)).Text);
            stat.HomeFastBreakPoints = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeFastBreakPoints)).Text);
            stat.HomeBenchPoints = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeBenchPoints)).Text);
            int hometeamreb = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeTeamRebounds)).Text);
            int hometeamto = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeTeamTurnovers)).Text);
            stat.HomePointsofTO = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomePointsofTO)).Text);
            driver.Navigate().GoToUrl(url+ "/box-score?range=0-28800");
            Thread.Sleep(3000);
            int line = 16;
            for (; line >= 5; line--)
            {
                try
                {
                    int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath("/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[3]/a")).Text);
                    break;
                }
                catch (Exception)
                {
                    continue;
                }
            }

            string AwayTeam =
                "/html/body/div[1]/div[2]/div[4]/section[2]/div[1]/h1/span";
            string HomeTeam =
                "/html/body/div[1]/div[2]/div[4]/section[3]/div[1]/h1/span";
            string AwayFGM =
                "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[3]";
            string AwayFGA =
                "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[4]";
            string Away3PM =
                "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[6]";
            string Away3PA =
                "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[7]";
            string AwayFTM =
                "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[9]";
            string AwayFTA =
                "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[10]";
            string AwayOffensiveRebounds =
                "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[12]";
            string AwayDefensiveRebounds =
                "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[13]";
            string AwayAssists =
                "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[15]";
            string AwaySteals =
                "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[16]";
            string AwayBlocks =
                "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[17]";
            string AwayTurnovers =
                "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[18]";
            line = 16;
            for (; line >= 5; line--)
            {
                try
                {
                    int.Parse(driver
                        .FindElement(OpenQA.Selenium.By.XPath(
                            "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + line +
                            "]/td[3]/a")).Text);
                    break;
                }
                catch (Exception)
                {
                    continue;
                }
            }

            string HomeFGM =
                "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[3]";
            string HomeFGA =
                "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[4]";
            string Home3PM =
                "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[6]";
            string Home3PA =
                "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[7]";
            string HomeFTM =
                "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[9]";
            string HomeFTA =
                "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[10]";
            string HomeOffensiveRebounds =
                "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[12]";
            string HomeDefensiveRebounds =
                "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[13]";
            string HomeAssists =
                "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[15]";
            string HomeSteals =
                "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[16]";
            string HomeBlocks =
                "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[17]";
            string HomeTurnovers =
                "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[18]";
            stat.GameNo = GameNo;
            stat.HomeTeam = _helpers.GetTeamEnumByTeamName(driver.FindElement(OpenQA.Selenium.By.XPath(HomeTeam)).Text.Replace(" ", "").ToUpper()
                .Replace('İ', 'I'));
            stat.AwayTeam = _helpers.GetTeamEnumByTeamName(driver.FindElement(OpenQA.Selenium.By.XPath(AwayTeam)).Text.Replace(" ", "").ToUpper()
                .Replace('İ', 'I'));
            try
            {
                stat.HomeFGA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeFGA)).Text);
            }
            catch (Exception)
            {
                stat.HomeFGA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeFGA + "/a")).Text);
            }

            try
            {
                stat.HomeFGM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeFGM)).Text);
            }
            catch (Exception)
            {
                stat.HomeFGM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeFGM + "/a")).Text);
            }

            try
            {
                stat.Home3PA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(Home3PA)).Text);
            }
            catch (Exception)
            {
                stat.Home3PA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(Home3PA + "/a")).Text);
            }

            try
            {
                stat.Home3PM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(Home3PM)).Text);
            }
            catch (Exception)
            {
                stat.Home3PM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(Home3PM + "/a")).Text);
            }

            try
            {
                stat.HomeFTA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeFTA)).Text);
            }
            catch (Exception)
            {
                stat.HomeFTA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeFTA + "/a")).Text);
            }

            try
            {
                stat.HomeFTM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeFTM)).Text);
            }
            catch (Exception)
            {
                stat.HomeFTM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeFTM + "/a")).Text);
            }

            try
            {
                stat.HomeAssists = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeAssists)).Text);
            }
            catch (Exception)
            {
                stat.HomeAssists = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeAssists + "/a")).Text);
            }

            try
            {
                stat.HomeDefensiveRebounds = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeDefensiveRebounds)).Text);
            }
            catch (Exception)
            {
                stat.HomeDefensiveRebounds = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeDefensiveRebounds + "/a")).Text);
            }

            try
            {
                stat.HomeOffensiveRebounds = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeOffensiveRebounds)).Text);
            }
            catch (Exception)
            {
                stat.HomeOffensiveRebounds = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeOffensiveRebounds + "/a")).Text);
            }

            try
            {
                stat.HomeBlocks = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeBlocks)).Text);
            }
            catch (Exception)
            {
                stat.HomeBlocks = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeBlocks + "/a")).Text);
            }

            try
            {
                stat.HomeTurnovers = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeTurnovers)).Text) + hometeamto;
            }
            catch (Exception)
            {
                stat.HomeTurnovers = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeTurnovers + "/a")).Text) + hometeamto;
            }

            try
            {
                stat.HomeSteals = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeSteals)).Text);
            }
            catch (Exception)
            {
                stat.HomeSteals = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeSteals + "/a")).Text);
            }

            stat.HomePoints = stat.HomeFGM * 2 + stat.Home3PM + stat.HomeFTM;
            stat.HomeTotalRebounds = stat.HomeDefensiveRebounds + stat.HomeOffensiveRebounds + hometeamreb;
            try
            {
                stat.AwayFGA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayFGA)).Text);
            }
            catch (Exception)
            {
                stat.AwayFGA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayFGA + "/a")).Text);
            }

            try
            {
                stat.AwayFGM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayFGM)).Text);
            }
            catch (Exception)
            {
                stat.AwayFGM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayFGM + "/a")).Text);
            }

            try
            {
                stat.Away3PA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(Away3PA)).Text);
            }
            catch (Exception)
            {
                stat.Away3PA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(Away3PA + "/a")).Text);
            }

            try
            {
                stat.Away3PM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(Away3PM)).Text);
            }
            catch (Exception)
            {
                stat.Away3PM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(Away3PM + "/a")).Text);
            }

            try
            {
                stat.AwayFTA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayFTA)).Text);
            }
            catch (Exception)
            {
                stat.AwayFTA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayFTA + "/a")).Text);
            }

            try
            {
                stat.AwayFTM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayFTM)).Text);
            }
            catch (Exception)
            {
                stat.AwayFTM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayFTM + "/a")).Text);
            }

            try
            {
                stat.AwayAssists = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayAssists)).Text);
            }
            catch (Exception)
            {
                stat.AwayAssists = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayAssists + "/a")).Text);
            }

            try
            {
                stat.AwayDefensiveRebounds = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayDefensiveRebounds)).Text);
            }
            catch (Exception)
            {
                stat.AwayDefensiveRebounds = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayDefensiveRebounds + "/a")).Text);
            }

            try
            {
                stat.AwayOffensiveRebounds = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayOffensiveRebounds)).Text);
            }
            catch (Exception)
            {
                stat.AwayOffensiveRebounds = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayOffensiveRebounds + "/a")).Text);
            }

            try
            {
                stat.AwayBlocks = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayBlocks)).Text);
            }
            catch (Exception)
            {
                stat.AwayBlocks = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayBlocks + "/a")).Text);
            }

            try
            {
                stat.AwayTurnovers = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayTurnovers)).Text) + awayteamto;
            }
            catch (Exception)
            {
                stat.AwayTurnovers = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayTurnovers + "/a")).Text) + awayteamto;
            }

            try
            {
                stat.AwaySteals = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwaySteals)).Text);
            }
            catch (Exception)
            {
                stat.AwaySteals = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwaySteals + "/a")).Text);
            }

            stat.AwayPoints = stat.AwayFGM * 2 + stat.Away3PM + stat.AwayFTM;
            stat.AwayTotalRebounds = stat.AwayDefensiveRebounds + stat.AwayOffensiveRebounds + awayteamreb;
            stat.OverTime = false || stat.HomePoints == stat.AwayPoints;
            return stat;
        }
        public FullSeasonQuarters QuarterScraper(ChromeDriver driver, int GameNo, int QuarterNo)
        {
            FullSeasonQuarters stat = new FullSeasonQuarters();
            int line = 25;
            for (; line >= 5; line--)
            {
                try
                {
                    int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath("/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[3]/a")).Text);
                    break;
                }
                catch (Exception)
                {
                    continue;
                }
            }

            string AwayTeam =
                "/html/body/div[1]/div[2]/div[4]/section[2]/div[1]/h1/span";
            string HomeTeam =
                "/html/body/div[1]/div[2]/div[4]/section[3]/div[1]/h1/span";
            string AwayFGM =
                "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[3]";
            string AwayFGA =
                "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[4]";
            string Away3PM =
                "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[6]";
            string Away3PA =
                "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[7]";
            string AwayFTM =
                "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[9]";
            string AwayFTA =
                "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[10]";
            string AwayOffensiveRebounds =
                "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[12]";
            string AwayDefensiveRebounds =
                "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[13]";
            string AwayAssists =
                "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[15]";
            string AwaySteals =
                "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[16]";
            string AwayBlocks =
                "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[17]";
            string AwayTurnovers =
                "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[18]";
            line = 25;
            for (; line >= 5; line--)
            {
                try
                {
                    int.Parse(driver
                        .FindElement(OpenQA.Selenium.By.XPath(
                            "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + line +
                            "]/td[3]/a")).Text);
                    break;
                }
                catch (Exception)
                {
                    continue;
                }
            }

            string HomeFGM =
                "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[3]";
            string HomeFGA =
                "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[4]";
            string Home3PM =
                "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[6]";
            string Home3PA =
                "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[7]";
            string HomeFTM =
                "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[9]";
            string HomeFTA =
                "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[10]";
            string HomeOffensiveRebounds =
                "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[12]";
            string HomeDefensiveRebounds =
                "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[13]";
            string HomeAssists =
                "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[15]";
            string HomeSteals =
                "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[16]";
            string HomeBlocks =
                "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[17]";
            string HomeTurnovers =
                "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[18]";
            stat.GameNo = GameNo;
            stat.QuarterNo = QuarterNo;
            for (int i = 0; i < 1; i++)
            {
                try
                {
                    stat.HomeTeam = _helpers.GetTeamEnumByTeamName(driver.FindElement(OpenQA.Selenium.By.XPath(HomeTeam)).Text.Replace(" ", "").ToUpper()
                    .Replace('İ', 'I'));
                }
                catch (Exception)
                {
                    i--;
                    Thread.Sleep(3000);
                    driver.Navigate().Refresh();
                }
            }
            stat.AwayTeam = _helpers.GetTeamEnumByTeamName(driver.FindElement(OpenQA.Selenium.By.XPath(AwayTeam)).Text.Replace(" ", "").ToUpper()
                .Replace('İ', 'I'));
            try
            {
                stat.HomeFGA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeFGA)).Text);
            }
            catch (Exception)
            {
                stat.HomeFGA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeFGA + "/a")).Text);
            }

            try
            {
                stat.HomeFGM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeFGM)).Text);
            }
            catch (Exception)
            {
                stat.HomeFGM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeFGM + "/a")).Text);
            }

            try
            {
                stat.Home3PA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(Home3PA)).Text);
            }
            catch (Exception)
            {
                stat.Home3PA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(Home3PA + "/a")).Text);
            }

            try
            {
                stat.Home3PM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(Home3PM)).Text);
            }
            catch (Exception)
            {
                stat.Home3PM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(Home3PM + "/a")).Text);
            }

            try
            {
                stat.HomeFTA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeFTA)).Text);
            }
            catch (Exception)
            {
                stat.HomeFTA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeFTA + "/a")).Text);
            }

            try
            {
                stat.HomeFTM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeFTM)).Text);
            }
            catch (Exception)
            {
                stat.HomeFTM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeFTM + "/a")).Text);
            }

            try
            {
                stat.HomeAssists = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeAssists)).Text);
            }
            catch (Exception)
            {
                stat.HomeAssists = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeAssists + "/a")).Text);
            }

            try
            {
                stat.HomeDefensiveRebounds = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeDefensiveRebounds)).Text);
            }
            catch (Exception)
            {
                stat.HomeDefensiveRebounds = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeDefensiveRebounds + "/a")).Text);
            }

            try
            {
                stat.HomeOffensiveRebounds = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeOffensiveRebounds)).Text);
            }
            catch (Exception)
            {
                stat.HomeOffensiveRebounds = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeOffensiveRebounds + "/a")).Text);
            }

            try
            {
                stat.HomeBlocks = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeBlocks)).Text);
            }
            catch (Exception)
            {
                stat.HomeBlocks = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeBlocks + "/a")).Text);
            }

            try
            {
                stat.HomeTurnovers = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeTurnovers)).Text);
            }
            catch (Exception)
            {
                stat.HomeTurnovers = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeTurnovers + "/a")).Text);
            }

            try
            {
                stat.HomeSteals = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeSteals)).Text);
            }
            catch (Exception)
            {
                stat.HomeSteals = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeSteals + "/a")).Text);
            }

            stat.HomePoints = stat.HomeFGM * 2 + stat.Home3PM + stat.HomeFTM;
            try
            {
                stat.AwayFGA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayFGA)).Text);
            }
            catch (Exception)
            {
                stat.AwayFGA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayFGA + "/a")).Text);
            }

            try
            {
                stat.AwayFGM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayFGM)).Text);
            }
            catch (Exception)
            {
                stat.AwayFGM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayFGM + "/a")).Text);
            }

            try
            {
                stat.Away3PA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(Away3PA)).Text);
            }
            catch (Exception)
            {
                stat.Away3PA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(Away3PA + "/a")).Text);
            }

            try
            {
                stat.Away3PM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(Away3PM)).Text);
            }
            catch (Exception)
            {
                stat.Away3PM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(Away3PM + "/a")).Text);
            }

            try
            {
                stat.AwayFTA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayFTA)).Text);
            }
            catch (Exception)
            {
                stat.AwayFTA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayFTA + "/a")).Text);
            }

            try
            {
                stat.AwayFTM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayFTM)).Text);
            }
            catch (Exception)
            {
                stat.AwayFTM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayFTM + "/a")).Text);
            }

            try
            {
                stat.AwayAssists = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayAssists)).Text);
            }
            catch (Exception)
            {
                stat.AwayAssists = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayAssists + "/a")).Text);
            }

            try
            {
                stat.AwayDefensiveRebounds = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayDefensiveRebounds)).Text);
            }
            catch (Exception)
            {
                stat.AwayDefensiveRebounds = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayDefensiveRebounds + "/a")).Text);
            }

            try
            {
                stat.AwayOffensiveRebounds = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayOffensiveRebounds)).Text);
            }
            catch (Exception)
            {
                stat.AwayOffensiveRebounds = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayOffensiveRebounds + "/a")).Text);
            }

            try
            {
                stat.AwayBlocks = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayBlocks)).Text);
            }
            catch (Exception)
            {
                stat.AwayBlocks = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayBlocks + "/a")).Text);
            }

            try
            {
                stat.AwayTurnovers = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayTurnovers)).Text);
            }
            catch (Exception)
            {
                stat.AwayTurnovers = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayTurnovers + "/a")).Text);
            }

            try
            {
                stat.AwaySteals = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwaySteals)).Text);
            }
            catch (Exception)
            {
                stat.AwaySteals = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwaySteals + "/a")).Text);
            }

            stat.AwayPoints = stat.AwayFGM * 2 + stat.Away3PM + stat.AwayFTM;
            return stat;
        }
        public FullSeasonQuarters20_21 QuarterScraper20_21(ChromeDriver driver, int GameNo, int QuarterNo)
        {
            FullSeasonQuarters20_21 stat = new FullSeasonQuarters20_21();
            int line = 16;
            for (; line >= 5; line--)
            {
                try
                {
                    int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath("/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[3]/a")).Text);
                    break;
                }
                catch (Exception)
                {
                    continue;
                }
            }

            string AwayTeam =
                "/html/body/div[1]/div[2]/div[4]/section[2]/div[1]/h1/span";
            string HomeTeam =
                "/html/body/div[1]/div[2]/div[4]/section[3]/div[1]/h1/span";
            string AwayFGM =
                "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[3]";
            string AwayFGA =
                "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[4]";
            string Away3PM =
                "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[6]";
            string Away3PA =
                "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[7]";
            string AwayFTM =
                "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[9]";
            string AwayFTA =
                "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[10]";
            string AwayOffensiveRebounds =
                "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[12]";
            string AwayDefensiveRebounds =
                "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[13]";
            string AwayAssists =
                "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[15]";
            string AwaySteals =
                "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[16]";
            string AwayBlocks =
                "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[17]";
            string AwayTurnovers =
                "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[18]";
            line = 16;
            for (; line >= 5; line--)
            {
                try
                {
                    int.Parse(driver
                        .FindElement(OpenQA.Selenium.By.XPath(
                            "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + line +
                            "]/td[3]/a")).Text);
                    break;
                }
                catch (Exception)
                {
                    continue;
                }
            }

            string HomeFGM =
                "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[3]";
            string HomeFGA =
                "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[4]";
            string Home3PM =
                "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[6]";
            string Home3PA =
                "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[7]";
            string HomeFTM =
                "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[9]";
            string HomeFTA =
                "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[10]";
            string HomeOffensiveRebounds =
                "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[12]";
            string HomeDefensiveRebounds =
                "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[13]";
            string HomeAssists =
                "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[15]";
            string HomeSteals =
                "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[16]";
            string HomeBlocks =
                "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[17]";
            string HomeTurnovers =
                "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[18]";
            stat.GameNo = GameNo;
            stat.QuarterNo = QuarterNo;
            stat.HomeTeam = _helpers.GetTeamEnumByTeamName(driver.FindElement(OpenQA.Selenium.By.XPath(HomeTeam)).Text.Replace(" ", "").ToUpper()
                .Replace('İ', 'I'));
            stat.AwayTeam = _helpers.GetTeamEnumByTeamName(driver.FindElement(OpenQA.Selenium.By.XPath(AwayTeam)).Text.Replace(" ", "").ToUpper()
                .Replace('İ', 'I'));
            try
            {
                stat.HomeFGA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeFGA)).Text);
            }
            catch (Exception)
            {
                stat.HomeFGA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeFGA + "/a")).Text);
            }

            try
            {
                stat.HomeFGM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeFGM)).Text);
            }
            catch (Exception)
            {
                stat.HomeFGM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeFGM + "/a")).Text);
            }

            try
            {
                stat.Home3PA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(Home3PA)).Text);
            }
            catch (Exception)
            {
                stat.Home3PA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(Home3PA + "/a")).Text);
            }

            try
            {
                stat.Home3PM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(Home3PM)).Text);
            }
            catch (Exception)
            {
                stat.Home3PM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(Home3PM + "/a")).Text);
            }

            try
            {
                stat.HomeFTA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeFTA)).Text);
            }
            catch (Exception)
            {
                stat.HomeFTA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeFTA + "/a")).Text);
            }

            try
            {
                stat.HomeFTM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeFTM)).Text);
            }
            catch (Exception)
            {
                stat.HomeFTM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeFTM + "/a")).Text);
            }

            try
            {
                stat.HomeAssists = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeAssists)).Text);
            }
            catch (Exception)
            {
                stat.HomeAssists = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeAssists + "/a")).Text);
            }

            try
            {
                stat.HomeDefensiveRebounds = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeDefensiveRebounds)).Text);
            }
            catch (Exception)
            {
                stat.HomeDefensiveRebounds = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeDefensiveRebounds + "/a")).Text);
            }

            try
            {
                stat.HomeOffensiveRebounds = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeOffensiveRebounds)).Text);
            }
            catch (Exception)
            {
                stat.HomeOffensiveRebounds = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeOffensiveRebounds + "/a")).Text);
            }

            try
            {
                stat.HomeBlocks = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeBlocks)).Text);
            }
            catch (Exception)
            {
                stat.HomeBlocks = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeBlocks + "/a")).Text);
            }

            try
            {
                stat.HomeTurnovers = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeTurnovers)).Text);
            }
            catch (Exception)
            {
                stat.HomeTurnovers = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeTurnovers + "/a")).Text);
            }

            try
            {
                stat.HomeSteals = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeSteals)).Text);
            }
            catch (Exception)
            {
                stat.HomeSteals = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeSteals + "/a")).Text);
            }

            stat.HomePoints = stat.HomeFGM * 2 + stat.Home3PM + stat.HomeFTM;
            try
            {
                stat.AwayFGA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayFGA)).Text);
            }
            catch (Exception)
            {
                stat.AwayFGA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayFGA + "/a")).Text);
            }

            try
            {
                stat.AwayFGM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayFGM)).Text);
            }
            catch (Exception)
            {
                stat.AwayFGM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayFGM + "/a")).Text);
            }

            try
            {
                stat.Away3PA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(Away3PA)).Text);
            }
            catch (Exception)
            {
                stat.Away3PA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(Away3PA + "/a")).Text);
            }

            try
            {
                stat.Away3PM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(Away3PM)).Text);
            }
            catch (Exception)
            {
                stat.Away3PM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(Away3PM + "/a")).Text);
            }

            try
            {
                stat.AwayFTA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayFTA)).Text);
            }
            catch (Exception)
            {
                stat.AwayFTA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayFTA + "/a")).Text);
            }

            try
            {
                stat.AwayFTM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayFTM)).Text);
            }
            catch (Exception)
            {
                stat.AwayFTM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayFTM + "/a")).Text);
            }

            try
            {
                stat.AwayAssists = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayAssists)).Text);
            }
            catch (Exception)
            {
                stat.AwayAssists = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayAssists + "/a")).Text);
            }

            try
            {
                stat.AwayDefensiveRebounds = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayDefensiveRebounds)).Text);
            }
            catch (Exception)
            {
                stat.AwayDefensiveRebounds = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayDefensiveRebounds + "/a")).Text);
            }

            try
            {
                stat.AwayOffensiveRebounds = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayOffensiveRebounds)).Text);
            }
            catch (Exception)
            {
                stat.AwayOffensiveRebounds = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayOffensiveRebounds + "/a")).Text);
            }

            try
            {
                stat.AwayBlocks = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayBlocks)).Text);
            }
            catch (Exception)
            {
                stat.AwayBlocks = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayBlocks + "/a")).Text);
            }

            try
            {
                stat.AwayTurnovers = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayTurnovers)).Text);
            }
            catch (Exception)
            {
                stat.AwayTurnovers = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayTurnovers + "/a")).Text);
            }

            try
            {
                stat.AwaySteals = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwaySteals)).Text);
            }
            catch (Exception)
            {
                stat.AwaySteals = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwaySteals + "/a")).Text);
            }

            stat.AwayPoints = stat.AwayFGM * 2 + stat.Away3PM + stat.AwayFTM;
            return stat;
        }
        public FullSeasonQuarters19_20 QuarterScraper19_20(ChromeDriver driver, int GameNo, int QuarterNo)
        {
            FullSeasonQuarters19_20 stat = new FullSeasonQuarters19_20();
            int line = 16;
            for (; line >= 5; line--)
            {
                try
                {
                    int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath("/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[3]/a")).Text);
                    break;
                }
                catch (Exception)
                {
                    continue;
                }
            }

            string AwayTeam =
                "/html/body/div[1]/div[2]/div[4]/section[2]/div[1]/h1/span";
            string HomeTeam =
                "/html/body/div[1]/div[2]/div[4]/section[3]/div[1]/h1/span";
            string AwayFGM =
                "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[3]";
            string AwayFGA =
                "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[4]";
            string Away3PM =
                "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[6]";
            string Away3PA =
                "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[7]";
            string AwayFTM =
                "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[9]";
            string AwayFTA =
                "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[10]";
            string AwayOffensiveRebounds =
                "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[12]";
            string AwayDefensiveRebounds =
                "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[13]";
            string AwayAssists =
                "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[15]";
            string AwaySteals =
                "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[16]";
            string AwayBlocks =
                "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[17]";
            string AwayTurnovers =
                "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[18]";
            line = 16;
            for (; line >= 5; line--)
            {
                try
                {
                    int.Parse(driver
                        .FindElement(OpenQA.Selenium.By.XPath(
                            "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + line +
                            "]/td[3]/a")).Text);
                    break;
                }
                catch (Exception)
                {
                    continue;
                }
            }

            string HomeFGM =
                "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[3]";
            string HomeFGA =
                "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[4]";
            string Home3PM =
                "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[6]";
            string Home3PA =
                "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[7]";
            string HomeFTM =
                "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[9]";
            string HomeFTA =
                "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[10]";
            string HomeOffensiveRebounds =
                "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[12]";
            string HomeDefensiveRebounds =
                "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[13]";
            string HomeAssists =
                "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[15]";
            string HomeSteals =
                "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[16]";
            string HomeBlocks =
                "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[17]";
            string HomeTurnovers =
                "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + line + "]/td[18]";
            stat.GameNo = GameNo;
            stat.QuarterNo = QuarterNo;
            stat.HomeTeam = _helpers.GetTeamEnumByTeamName(driver.FindElement(OpenQA.Selenium.By.XPath(HomeTeam)).Text.Replace(" ", "").ToUpper()
                .Replace('İ', 'I'));
            stat.AwayTeam = _helpers.GetTeamEnumByTeamName(driver.FindElement(OpenQA.Selenium.By.XPath(AwayTeam)).Text.Replace(" ", "").ToUpper()
                .Replace('İ', 'I'));
            try
            {
                stat.HomeFGA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeFGA)).Text);
            }
            catch (Exception)
            {
                stat.HomeFGA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeFGA + "/a")).Text);
            }

            try
            {
                stat.HomeFGM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeFGM)).Text);
            }
            catch (Exception)
            {
                stat.HomeFGM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeFGM + "/a")).Text);
            }

            try
            {
                stat.Home3PA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(Home3PA)).Text);
            }
            catch (Exception)
            {
                stat.Home3PA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(Home3PA + "/a")).Text);
            }

            try
            {
                stat.Home3PM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(Home3PM)).Text);
            }
            catch (Exception)
            {
                stat.Home3PM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(Home3PM + "/a")).Text);
            }

            try
            {
                stat.HomeFTA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeFTA)).Text);
            }
            catch (Exception)
            {
                stat.HomeFTA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeFTA + "/a")).Text);
            }

            try
            {
                stat.HomeFTM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeFTM)).Text);
            }
            catch (Exception)
            {
                stat.HomeFTM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeFTM + "/a")).Text);
            }

            try
            {
                stat.HomeAssists = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeAssists)).Text);
            }
            catch (Exception)
            {
                stat.HomeAssists = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeAssists + "/a")).Text);
            }

            try
            {
                stat.HomeDefensiveRebounds = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeDefensiveRebounds)).Text);
            }
            catch (Exception)
            {
                stat.HomeDefensiveRebounds = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeDefensiveRebounds + "/a")).Text);
            }

            try
            {
                stat.HomeOffensiveRebounds = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeOffensiveRebounds)).Text);
            }
            catch (Exception)
            {
                stat.HomeOffensiveRebounds = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeOffensiveRebounds + "/a")).Text);
            }

            try
            {
                stat.HomeBlocks = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeBlocks)).Text);
            }
            catch (Exception)
            {
                stat.HomeBlocks = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeBlocks + "/a")).Text);
            }

            try
            {
                stat.HomeTurnovers = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeTurnovers)).Text);
            }
            catch (Exception)
            {
                stat.HomeTurnovers = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeTurnovers + "/a")).Text);
            }

            try
            {
                stat.HomeSteals = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeSteals)).Text);
            }
            catch (Exception)
            {
                stat.HomeSteals = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeSteals + "/a")).Text);
            }

            stat.HomePoints = stat.HomeFGM * 2 + stat.Home3PM + stat.HomeFTM;
            try
            {
                stat.AwayFGA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayFGA)).Text);
            }
            catch (Exception)
            {
                stat.AwayFGA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayFGA + "/a")).Text);
            }

            try
            {
                stat.AwayFGM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayFGM)).Text);
            }
            catch (Exception)
            {
                stat.AwayFGM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayFGM + "/a")).Text);
            }

            try
            {
                stat.Away3PA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(Away3PA)).Text);
            }
            catch (Exception)
            {
                stat.Away3PA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(Away3PA + "/a")).Text);
            }

            try
            {
                stat.Away3PM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(Away3PM)).Text);
            }
            catch (Exception)
            {
                stat.Away3PM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(Away3PM + "/a")).Text);
            }

            try
            {
                stat.AwayFTA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayFTA)).Text);
            }
            catch (Exception)
            {
                stat.AwayFTA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayFTA + "/a")).Text);
            }

            try
            {
                stat.AwayFTM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayFTM)).Text);
            }
            catch (Exception)
            {
                stat.AwayFTM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayFTM + "/a")).Text);
            }

            try
            {
                stat.AwayAssists = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayAssists)).Text);
            }
            catch (Exception)
            {
                stat.AwayAssists = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayAssists + "/a")).Text);
            }

            try
            {
                stat.AwayDefensiveRebounds = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayDefensiveRebounds)).Text);
            }
            catch (Exception)
            {
                stat.AwayDefensiveRebounds = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayDefensiveRebounds + "/a")).Text);
            }

            try
            {
                stat.AwayOffensiveRebounds = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayOffensiveRebounds)).Text);
            }
            catch (Exception)
            {
                stat.AwayOffensiveRebounds = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayOffensiveRebounds + "/a")).Text);
            }

            try
            {
                stat.AwayBlocks = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayBlocks)).Text);
            }
            catch (Exception)
            {
                stat.AwayBlocks = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayBlocks + "/a")).Text);
            }

            try
            {
                stat.AwayTurnovers = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayTurnovers)).Text);
            }
            catch (Exception)
            {
                stat.AwayTurnovers = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayTurnovers + "/a")).Text);
            }

            try
            {
                stat.AwaySteals = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwaySteals)).Text);
            }
            catch (Exception)
            {
                stat.AwaySteals = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwaySteals + "/a")).Text);
            }

            stat.AwayPoints = stat.AwayFGM * 2 + stat.Away3PM + stat.AwayFTM;
            return stat;
        }
        public List<PlayerStats> PlayerStatScraper(ChromeDriver driver, int GameNo)
        {
            Thread.Sleep(1500);
            string AwayTeam =
                "/html/body/div[1]/div[2]/div[4]/section[2]/div[1]/h1/span";
            string HomeTeam =
                "/html/body/div[1]/div[2]/div[4]/section[3]/div[1]/h1/span";
            List<Players> homePlayers = new List<Players>();
            List<Players> awayPlayers = new List<Players>();
            Team Home = _helpers.GetTeamEnumByTeamName(driver.FindElement(OpenQA.Selenium.By.XPath(HomeTeam)).Text.Replace(" ", "").ToUpper()
                .Replace('İ', 'I'));
            Team Away = _helpers.GetTeamEnumByTeamName(driver.FindElement(OpenQA.Selenium.By.XPath(AwayTeam)).Text.Replace(" ", "").ToUpper()
                .Replace('İ', 'I'));
            int line = 25;
            for (; line >= 1; line--)
            {
                try
                {
                    int.Parse(driver
                        .FindElement(OpenQA.Selenium.By.XPath(
                            "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + line +
                            "]/td[21]")).Text);
                    Players Player = _helpers.GetPlayerWithName(
                        driver.FindElement(OpenQA.Selenium.By.XPath(
                            "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + line +
                            "]/td[1]/div/a/span")).Text);
                    Player.line = line;
                    awayPlayers.Add(Player);
                }
                catch (Exception)
                {
                    continue;
                }
            }

            line = 25;
            for (; line >= 1; line--)
            {
                try
                {
                    int.Parse(driver
                        .FindElement(OpenQA.Selenium.By.XPath(
                            "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + line +
                            "]/td[21]")).Text);
                    Players Player = _helpers.GetPlayerWithName(
                        driver.FindElement(OpenQA.Selenium.By.XPath(
                            "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + line +
                            "]/td[1]/div/a/span")).Text);
                    Player.line = line;
                    homePlayers.Add(Player);
                }
                catch (Exception)
                {
                    continue;
                }
            }
            List<PlayerStats> Stats = new List<PlayerStats>();
            foreach (var player in homePlayers)
            {
                PlayerStats stat = new PlayerStats();
                stat.Player = player;
                stat.GameNo = GameNo;
                stat.Team = Home;
                string HomeMinutes =
                    "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[2]";
                string HomeFGM =
                    "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[3]";
                string HomeFGA =
                    "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[4]";
                string Home3PM =
                    "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[6]";
                string Home3PA =
                    "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[7]";
                string HomeFTM =
                    "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[9]";
                string HomeFTA =
                    "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[10]";
                string HomeOffensiveRebounds =
                    "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[12]";
                string HomeDefensiveRebounds =
                    "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[13]";
                string HomeAssists =
                    "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[15]";
                string HomeSteals =
                    "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[16]";
                string HomeBlocks =
                    "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[17]";
                string HomeTurnovers =
                    "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[18]";
                string HomeFouls =
                    "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[19]";
                string HomePlusMinus =
                    "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[21]";

                string time = driver.FindElement(OpenQA.Selenium.By.XPath(HomeMinutes)).Text;
                stat.Minutes = int.Parse(time.Substring(0, time.IndexOf(':')));
                stat.Seconds = int.Parse(time.Substring(time.IndexOf(':') + 1, 2));
                stat.PlayerFouls = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeFouls)).Text);
                stat.PlayerPlusMinus = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomePlusMinus)).Text);
                try
                {
                    stat.PlayerFGA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeFGA)).Text);
                }
                catch (Exception)
                {
                    stat.PlayerFGA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeFGA + "/a")).Text);
                }

                try
                {
                    stat.PlayerFGM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeFGM)).Text);
                }
                catch (Exception)
                {
                    stat.PlayerFGM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeFGM + "/a")).Text);
                }

                try
                {
                    stat.Player3PA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(Home3PA)).Text);
                }
                catch (Exception)
                {
                    stat.Player3PA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(Home3PA + "/a")).Text);
                }

                try
                {
                    stat.Player3PM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(Home3PM)).Text);
                }
                catch (Exception)
                {
                    stat.Player3PM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(Home3PM + "/a")).Text);
                }

                try
                {
                    stat.PlayerFTA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeFTA)).Text);
                }
                catch (Exception)
                {
                    stat.PlayerFTA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeFTA + "/a")).Text);
                }

                try
                {
                    stat.PlayerFTM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeFTM)).Text);
                }
                catch (Exception)
                {
                    stat.PlayerFTM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeFTM + "/a")).Text);
                }

                try
                {
                    stat.PlayerAssists = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeAssists)).Text);
                }
                catch (Exception)
                {
                    stat.PlayerAssists = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeAssists + "/a")).Text);
                }

                try
                {
                    stat.PlayerDefensiveRebounds = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeDefensiveRebounds)).Text);
                }
                catch (Exception)
                {
                    stat.PlayerDefensiveRebounds = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeDefensiveRebounds + "/a")).Text);
                }

                try
                {
                    stat.PlayerOffensiveRebounds = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeOffensiveRebounds)).Text);
                }
                catch (Exception)
                {
                    stat.PlayerOffensiveRebounds = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeOffensiveRebounds + "/a")).Text);
                }

                try
                {
                    stat.PlayerBlocks = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeBlocks)).Text);
                }
                catch (Exception)
                {
                    stat.PlayerBlocks = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeBlocks + "/a")).Text);
                }

                try
                {
                    stat.PlayerTurnovers = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeTurnovers)).Text);
                }
                catch (Exception)
                {
                    stat.PlayerTurnovers = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeTurnovers + "/a")).Text);
                }

                try
                {
                    stat.PlayerSteals = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeSteals)).Text);
                }
                catch (Exception)
                {
                    stat.PlayerSteals = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeSteals + "/a")).Text);
                }

                stat.PlayerPoints = stat.PlayerFGM * 2 + stat.Player3PM + stat.PlayerFTM;
                Stats.Add(stat);
            }
            foreach (var player in awayPlayers)
            {
                PlayerStats stat = new PlayerStats();
                stat.Player = player;
                stat.GameNo = GameNo;
                stat.Team = Away;
                string AwayMinutes =
                    "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[2]";
                string AwayFGM =
                    "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[3]";
                string AwayFGA =
                    "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[4]";
                string Away3PM =
                    "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[6]";
                string Away3PA =
                    "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[7]";
                string AwayFTM =
                    "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[9]";
                string AwayFTA =
                    "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[10]";
                string AwayOffensiveRebounds =
                    "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[12]";
                string AwayDefensiveRebounds =
                    "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[13]";
                string AwayAssists =
                    "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[15]";
                string AwaySteals =
                    "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[16]";
                string AwayBlocks =
                    "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[17]";
                string AwayTurnovers =
                    "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[18]";
                string AwayFouls =
                    "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[19]";
                string AwayPlusMinus =
                    "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[21]";

                string time = driver.FindElement(OpenQA.Selenium.By.XPath(AwayMinutes)).Text;
                stat.Minutes = int.Parse(time.Substring(0, time.IndexOf(':')));
                stat.Seconds = int.Parse(time.Substring(time.IndexOf(':') + 1, 2));
                stat.PlayerFouls = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayFouls)).Text);
                stat.PlayerPlusMinus = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayPlusMinus)).Text);
                try
                {
                    stat.PlayerFGA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayFGA)).Text);
                }
                catch (Exception)
                {
                    stat.PlayerFGA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayFGA + "/a")).Text);
                }

                try
                {
                    stat.PlayerFGM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayFGM)).Text);
                }
                catch (Exception)
                {
                    stat.PlayerFGM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayFGM + "/a")).Text);
                }

                try
                {
                    stat.Player3PA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(Away3PA)).Text);
                }
                catch (Exception)
                {
                    stat.Player3PA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(Away3PA + "/a")).Text);
                }

                try
                {
                    stat.Player3PM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(Away3PM)).Text);
                }
                catch (Exception)
                {
                    stat.Player3PM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(Away3PM + "/a")).Text);
                }

                try
                {
                    stat.PlayerFTA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayFTA)).Text);
                }
                catch (Exception)
                {
                    stat.PlayerFTA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayFTA + "/a")).Text);
                }

                try
                {
                    stat.PlayerFTM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayFTM)).Text);
                }
                catch (Exception)
                {
                    stat.PlayerFTM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayFTM + "/a")).Text);
                }

                try
                {
                    stat.PlayerAssists = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayAssists)).Text);
                }
                catch (Exception)
                {
                    stat.PlayerAssists = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayAssists + "/a")).Text);
                }

                try
                {
                    stat.PlayerDefensiveRebounds = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayDefensiveRebounds)).Text);
                }
                catch (Exception)
                {
                    stat.PlayerDefensiveRebounds = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayDefensiveRebounds + "/a")).Text);
                }

                try
                {
                    stat.PlayerOffensiveRebounds = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayOffensiveRebounds)).Text);
                }
                catch (Exception)
                {
                    stat.PlayerOffensiveRebounds = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayOffensiveRebounds + "/a")).Text);
                }

                try
                {
                    stat.PlayerBlocks = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayBlocks)).Text);
                }
                catch (Exception)
                {
                    stat.PlayerBlocks = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayBlocks + "/a")).Text);
                }

                try
                {
                    stat.PlayerTurnovers = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayTurnovers)).Text);
                }
                catch (Exception)
                {
                    stat.PlayerTurnovers = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayTurnovers + "/a")).Text);
                }

                try
                {
                    stat.PlayerSteals = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwaySteals)).Text);
                }
                catch (Exception)
                {
                    stat.PlayerSteals = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwaySteals + "/a")).Text);
                }

                stat.PlayerPoints = stat.PlayerFGM * 2 + stat.Player3PM + stat.PlayerFTM;
                Stats.Add(stat);
            }

            return Stats;
        }
        public List<PlayerStats20_21> PlayerStatScraper20_21(ChromeDriver driver, int GameNo)
        {
            Thread.Sleep(1500);
            string AwayTeam =
                "/html/body/div[1]/div[2]/div[4]/section[2]/div[1]/h1/span";
            string HomeTeam =
                "/html/body/div[1]/div[2]/div[4]/section[3]/div[1]/h1/span";
            List<Players> homePlayers = new List<Players>();
            List<Players> awayPlayers = new List<Players>();
            Team Home = _helpers.GetTeamEnumByTeamName(driver.FindElement(OpenQA.Selenium.By.XPath(HomeTeam)).Text.Replace(" ", "").ToUpper()
                .Replace('İ', 'I'));
            Team Away = _helpers.GetTeamEnumByTeamName(driver.FindElement(OpenQA.Selenium.By.XPath(AwayTeam)).Text.Replace(" ", "").ToUpper()
                .Replace('İ', 'I'));
            int line = 16;
            for (; line >= 1; line--)
            {
                try
                {
                    int.Parse(driver
                        .FindElement(OpenQA.Selenium.By.XPath(
                            "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + line +
                            "]/td[21]")).Text);
                    Players Player = _helpers.GetPlayerWithName(
                        driver.FindElement(OpenQA.Selenium.By.XPath(
                            "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + line +
                            "]/td[1]/div/a/span")).Text);
                    Player.line = line;
                    awayPlayers.Add(Player);
                }
                catch (Exception)
                {
                    continue;
                }
            }

            line = 16;
            for (; line >= 1; line--)
            {
                try
                {
                    int.Parse(driver
                        .FindElement(OpenQA.Selenium.By.XPath(
                            "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + line +
                            "]/td[21]")).Text);
                    Players Player = _helpers.GetPlayerWithName(
                        driver.FindElement(OpenQA.Selenium.By.XPath(
                            "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + line +
                            "]/td[1]/div/a/span")).Text);
                    Player.line = line;
                    homePlayers.Add(Player);
                }
                catch (Exception)
                {
                    continue;
                }
            }
            List<PlayerStats20_21> Stats = new List<PlayerStats20_21>();
            foreach (var player in homePlayers)
            {
                PlayerStats20_21 stat = new PlayerStats20_21();
                stat.Player = player;
                stat.GameNo = GameNo;
                stat.Team = Home;
                string HomeMinutes =
                    "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[2]";
                string HomeFGM =
                    "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[3]";
                string HomeFGA =
                    "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[4]";
                string Home3PM =
                    "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[6]";
                string Home3PA =
                    "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[7]";
                string HomeFTM =
                    "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[9]";
                string HomeFTA =
                    "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[10]";
                string HomeOffensiveRebounds =
                    "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[12]";
                string HomeDefensiveRebounds =
                    "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[13]";
                string HomeAssists =
                    "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[15]";
                string HomeSteals =
                    "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[16]";
                string HomeBlocks =
                    "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[17]";
                string HomeTurnovers =
                    "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[18]";
                string HomeFouls =
                    "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[19]";
                string HomePlusMinus =
                    "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[21]";

                string time = driver.FindElement(OpenQA.Selenium.By.XPath(HomeMinutes)).Text;
                stat.Minutes = int.Parse(time.Substring(0, time.IndexOf(':')));
                stat.Seconds = int.Parse(time.Substring(time.IndexOf(':') + 1, 2));
                stat.PlayerFouls = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeFouls)).Text);
                stat.PlayerPlusMinus = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomePlusMinus)).Text);
                try
                {
                    stat.PlayerFGA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeFGA)).Text);
                }
                catch (Exception)
                {
                    stat.PlayerFGA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeFGA + "/a")).Text);
                }

                try
                {
                    stat.PlayerFGM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeFGM)).Text);
                }
                catch (Exception)
                {
                    stat.PlayerFGM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeFGM + "/a")).Text);
                }

                try
                {
                    stat.Player3PA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(Home3PA)).Text);
                }
                catch (Exception)
                {
                    stat.Player3PA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(Home3PA + "/a")).Text);
                }

                try
                {
                    stat.Player3PM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(Home3PM)).Text);
                }
                catch (Exception)
                {
                    stat.Player3PM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(Home3PM + "/a")).Text);
                }

                try
                {
                    stat.PlayerFTA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeFTA)).Text);
                }
                catch (Exception)
                {
                    stat.PlayerFTA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeFTA + "/a")).Text);
                }

                try
                {
                    stat.PlayerFTM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeFTM)).Text);
                }
                catch (Exception)
                {
                    stat.PlayerFTM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeFTM + "/a")).Text);
                }

                try
                {
                    stat.PlayerAssists = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeAssists)).Text);
                }
                catch (Exception)
                {
                    stat.PlayerAssists = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeAssists + "/a")).Text);
                }

                try
                {
                    stat.PlayerDefensiveRebounds = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeDefensiveRebounds)).Text);
                }
                catch (Exception)
                {
                    stat.PlayerDefensiveRebounds = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeDefensiveRebounds + "/a")).Text);
                }

                try
                {
                    stat.PlayerOffensiveRebounds = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeOffensiveRebounds)).Text);
                }
                catch (Exception)
                {
                    stat.PlayerOffensiveRebounds = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeOffensiveRebounds + "/a")).Text);
                }

                try
                {
                    stat.PlayerBlocks = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeBlocks)).Text);
                }
                catch (Exception)
                {
                    stat.PlayerBlocks = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeBlocks + "/a")).Text);
                }

                try
                {
                    stat.PlayerTurnovers = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeTurnovers)).Text);
                }
                catch (Exception)
                {
                    stat.PlayerTurnovers = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeTurnovers + "/a")).Text);
                }

                try
                {
                    stat.PlayerSteals = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeSteals)).Text);
                }
                catch (Exception)
                {
                    stat.PlayerSteals = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeSteals + "/a")).Text);
                }

                stat.PlayerPoints = stat.PlayerFGM * 2 + stat.Player3PM + stat.PlayerFTM;
                Stats.Add(stat);
            }
            foreach (var player in awayPlayers)
            {
                PlayerStats20_21 stat = new PlayerStats20_21();
                stat.Player = player;
                stat.GameNo = GameNo;
                stat.Team = Away;
                string AwayMinutes =
                    "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[2]";
                string AwayFGM =
                    "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[3]";
                string AwayFGA =
                    "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[4]";
                string Away3PM =
                    "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[6]";
                string Away3PA =
                    "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[7]";
                string AwayFTM =
                    "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[9]";
                string AwayFTA =
                    "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[10]";
                string AwayOffensiveRebounds =
                    "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[12]";
                string AwayDefensiveRebounds =
                    "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[13]";
                string AwayAssists =
                    "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[15]";
                string AwaySteals =
                    "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[16]";
                string AwayBlocks =
                    "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[17]";
                string AwayTurnovers =
                    "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[18]";
                string AwayFouls =
                    "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[19]";
                string AwayPlusMinus =
                    "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[21]";

                string time = driver.FindElement(OpenQA.Selenium.By.XPath(AwayMinutes)).Text;
                stat.Minutes = int.Parse(time.Substring(0, time.IndexOf(':')));
                stat.Seconds = int.Parse(time.Substring(time.IndexOf(':') + 1, 2));
                stat.PlayerFouls = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayFouls)).Text);
                stat.PlayerPlusMinus = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayPlusMinus)).Text);
                try
                {
                    stat.PlayerFGA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayFGA)).Text);
                }
                catch (Exception)
                {
                    stat.PlayerFGA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayFGA + "/a")).Text);
                }

                try
                {
                    stat.PlayerFGM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayFGM)).Text);
                }
                catch (Exception)
                {
                    stat.PlayerFGM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayFGM + "/a")).Text);
                }

                try
                {
                    stat.Player3PA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(Away3PA)).Text);
                }
                catch (Exception)
                {
                    stat.Player3PA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(Away3PA + "/a")).Text);
                }

                try
                {
                    stat.Player3PM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(Away3PM)).Text);
                }
                catch (Exception)
                {
                    stat.Player3PM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(Away3PM + "/a")).Text);
                }

                try
                {
                    stat.PlayerFTA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayFTA)).Text);
                }
                catch (Exception)
                {
                    stat.PlayerFTA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayFTA + "/a")).Text);
                }

                try
                {
                    stat.PlayerFTM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayFTM)).Text);
                }
                catch (Exception)
                {
                    stat.PlayerFTM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayFTM + "/a")).Text);
                }

                try
                {
                    stat.PlayerAssists = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayAssists)).Text);
                }
                catch (Exception)
                {
                    stat.PlayerAssists = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayAssists + "/a")).Text);
                }

                try
                {
                    stat.PlayerDefensiveRebounds = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayDefensiveRebounds)).Text);
                }
                catch (Exception)
                {
                    stat.PlayerDefensiveRebounds = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayDefensiveRebounds + "/a")).Text);
                }

                try
                {
                    stat.PlayerOffensiveRebounds = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayOffensiveRebounds)).Text);
                }
                catch (Exception)
                {
                    stat.PlayerOffensiveRebounds = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayOffensiveRebounds + "/a")).Text);
                }

                try
                {
                    stat.PlayerBlocks = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayBlocks)).Text);
                }
                catch (Exception)
                {
                    stat.PlayerBlocks = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayBlocks + "/a")).Text);
                }

                try
                {
                    stat.PlayerTurnovers = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayTurnovers)).Text);
                }
                catch (Exception)
                {
                    stat.PlayerTurnovers = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayTurnovers + "/a")).Text);
                }

                try
                {
                    stat.PlayerSteals = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwaySteals)).Text);
                }
                catch (Exception)
                {
                    stat.PlayerSteals = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwaySteals + "/a")).Text);
                }

                stat.PlayerPoints = stat.PlayerFGM * 2 + stat.Player3PM + stat.PlayerFTM;
                Stats.Add(stat);
            }

            return Stats;
        }
        public List<PlayerStats19_20> PlayerStatScraper19_20(ChromeDriver driver, int GameNo)
        {
            Thread.Sleep(1500);
            string AwayTeam =
                "/html/body/div[1]/div[2]/div[4]/section[2]/div[1]/h1/span";
            string HomeTeam =
                "/html/body/div[1]/div[2]/div[4]/section[3]/div[1]/h1/span";
            List<Players> homePlayers = new List<Players>();
            List<Players> awayPlayers = new List<Players>();
            Team Home = _helpers.GetTeamEnumByTeamName(driver.FindElement(OpenQA.Selenium.By.XPath(HomeTeam)).Text.Replace(" ", "").ToUpper()
                .Replace('İ', 'I'));
            Team Away = _helpers.GetTeamEnumByTeamName(driver.FindElement(OpenQA.Selenium.By.XPath(AwayTeam)).Text.Replace(" ", "").ToUpper()
                .Replace('İ', 'I'));
            int line = 16;
            for (; line >= 1; line--)
            {
                try
                {
                    int.Parse(driver
                        .FindElement(OpenQA.Selenium.By.XPath(
                            "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + line +
                            "]/td[21]")).Text);
                    Players Player = _helpers.GetPlayerWithName(
                        driver.FindElement(OpenQA.Selenium.By.XPath(
                            "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + line +
                            "]/td[1]/div/a/span")).Text);
                    Player.line = line;
                    awayPlayers.Add(Player);
                }
                catch (Exception)
                {
                    continue;
                }
            }

            line = 16;
            for (; line >= 1; line--)
            {
                try
                {
                    int.Parse(driver
                        .FindElement(OpenQA.Selenium.By.XPath(
                            "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + line +
                            "]/td[21]")).Text);
                    Players Player = _helpers.GetPlayerWithName(
                        driver.FindElement(OpenQA.Selenium.By.XPath(
                            "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + line +
                            "]/td[1]/div/a/span")).Text);
                    Player.line = line;
                    homePlayers.Add(Player);
                }
                catch (Exception)
                {
                    continue;
                }
            }
            List<PlayerStats19_20> Stats = new List<PlayerStats19_20>();
            foreach (var player in homePlayers)
            {
                PlayerStats19_20 stat = new PlayerStats19_20();
                stat.Player = player;
                stat.GameNo = GameNo;
                stat.Team = Home;
                string HomeMinutes =
                    "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[2]";
                string HomeFGM =
                    "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[3]";
                string HomeFGA =
                    "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[4]";
                string Home3PM =
                    "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[6]";
                string Home3PA =
                    "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[7]";
                string HomeFTM =
                    "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[9]";
                string HomeFTA =
                    "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[10]";
                string HomeOffensiveRebounds =
                    "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[12]";
                string HomeDefensiveRebounds =
                    "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[13]";
                string HomeAssists =
                    "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[15]";
                string HomeSteals =
                    "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[16]";
                string HomeBlocks =
                    "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[17]";
                string HomeTurnovers =
                    "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[18]";
                string HomeFouls =
                    "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[19]";
                string HomePlusMinus =
                    "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[21]";

                string time = driver.FindElement(OpenQA.Selenium.By.XPath(HomeMinutes)).Text;
                stat.Minutes = int.Parse(time.Substring(0, time.IndexOf(':')));
                stat.Seconds = int.Parse(time.Substring(time.IndexOf(':') + 1, 2));
                stat.PlayerFouls = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeFouls)).Text);
                stat.PlayerPlusMinus = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomePlusMinus)).Text);
                try
                {
                    stat.PlayerFGA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeFGA)).Text);
                }
                catch (Exception)
                {
                    stat.PlayerFGA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeFGA + "/a")).Text);
                }

                try
                {
                    stat.PlayerFGM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeFGM)).Text);
                }
                catch (Exception)
                {
                    stat.PlayerFGM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeFGM + "/a")).Text);
                }

                try
                {
                    stat.Player3PA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(Home3PA)).Text);
                }
                catch (Exception)
                {
                    stat.Player3PA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(Home3PA + "/a")).Text);
                }

                try
                {
                    stat.Player3PM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(Home3PM)).Text);
                }
                catch (Exception)
                {
                    stat.Player3PM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(Home3PM + "/a")).Text);
                }

                try
                {
                    stat.PlayerFTA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeFTA)).Text);
                }
                catch (Exception)
                {
                    stat.PlayerFTA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeFTA + "/a")).Text);
                }

                try
                {
                    stat.PlayerFTM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeFTM)).Text);
                }
                catch (Exception)
                {
                    stat.PlayerFTM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeFTM + "/a")).Text);
                }

                try
                {
                    stat.PlayerAssists = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeAssists)).Text);
                }
                catch (Exception)
                {
                    stat.PlayerAssists = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeAssists + "/a")).Text);
                }

                try
                {
                    stat.PlayerDefensiveRebounds = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeDefensiveRebounds)).Text);
                }
                catch (Exception)
                {
                    stat.PlayerDefensiveRebounds = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeDefensiveRebounds + "/a")).Text);
                }

                try
                {
                    stat.PlayerOffensiveRebounds = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeOffensiveRebounds)).Text);
                }
                catch (Exception)
                {
                    stat.PlayerOffensiveRebounds = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeOffensiveRebounds + "/a")).Text);
                }

                try
                {
                    stat.PlayerBlocks = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeBlocks)).Text);
                }
                catch (Exception)
                {
                    stat.PlayerBlocks = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeBlocks + "/a")).Text);
                }

                try
                {
                    stat.PlayerTurnovers = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeTurnovers)).Text);
                }
                catch (Exception)
                {
                    stat.PlayerTurnovers = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeTurnovers + "/a")).Text);
                }

                try
                {
                    stat.PlayerSteals = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeSteals)).Text);
                }
                catch (Exception)
                {
                    stat.PlayerSteals = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeSteals + "/a")).Text);
                }

                stat.PlayerPoints = stat.PlayerFGM * 2 + stat.Player3PM + stat.PlayerFTM;
                Stats.Add(stat);
            }
            foreach (var player in awayPlayers)
            {
                PlayerStats19_20 stat = new PlayerStats19_20();
                stat.Player = player;
                stat.GameNo = GameNo;
                stat.Team = Away;
                string AwayMinutes =
                    "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[2]";
                string AwayFGM =
                    "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[3]";
                string AwayFGA =
                    "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[4]";
                string Away3PM =
                    "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[6]";
                string Away3PA =
                    "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[7]";
                string AwayFTM =
                    "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[9]";
                string AwayFTA =
                    "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[10]";
                string AwayOffensiveRebounds =
                    "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[12]";
                string AwayDefensiveRebounds =
                    "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[13]";
                string AwayAssists =
                    "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[15]";
                string AwaySteals =
                    "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[16]";
                string AwayBlocks =
                    "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[17]";
                string AwayTurnovers =
                    "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[18]";
                string AwayFouls =
                    "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[19]";
                string AwayPlusMinus =
                    "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[21]";

                string time = driver.FindElement(OpenQA.Selenium.By.XPath(AwayMinutes)).Text;
                stat.Minutes = int.Parse(time.Substring(0, time.IndexOf(':')));
                stat.Seconds = int.Parse(time.Substring(time.IndexOf(':') + 1, 2));
                stat.PlayerFouls = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayFouls)).Text);
                stat.PlayerPlusMinus = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayPlusMinus)).Text);
                try
                {
                    stat.PlayerFGA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayFGA)).Text);
                }
                catch (Exception)
                {
                    stat.PlayerFGA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayFGA + "/a")).Text);
                }

                try
                {
                    stat.PlayerFGM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayFGM)).Text);
                }
                catch (Exception)
                {
                    stat.PlayerFGM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayFGM + "/a")).Text);
                }

                try
                {
                    stat.Player3PA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(Away3PA)).Text);
                }
                catch (Exception)
                {
                    stat.Player3PA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(Away3PA + "/a")).Text);
                }

                try
                {
                    stat.Player3PM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(Away3PM)).Text);
                }
                catch (Exception)
                {
                    stat.Player3PM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(Away3PM + "/a")).Text);
                }

                try
                {
                    stat.PlayerFTA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayFTA)).Text);
                }
                catch (Exception)
                {
                    stat.PlayerFTA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayFTA + "/a")).Text);
                }

                try
                {
                    stat.PlayerFTM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayFTM)).Text);
                }
                catch (Exception)
                {
                    stat.PlayerFTM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayFTM + "/a")).Text);
                }

                try
                {
                    stat.PlayerAssists = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayAssists)).Text);
                }
                catch (Exception)
                {
                    stat.PlayerAssists = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayAssists + "/a")).Text);
                }

                try
                {
                    stat.PlayerDefensiveRebounds = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayDefensiveRebounds)).Text);
                }
                catch (Exception)
                {
                    stat.PlayerDefensiveRebounds = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayDefensiveRebounds + "/a")).Text);
                }

                try
                {
                    stat.PlayerOffensiveRebounds = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayOffensiveRebounds)).Text);
                }
                catch (Exception)
                {
                    stat.PlayerOffensiveRebounds = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayOffensiveRebounds + "/a")).Text);
                }

                try
                {
                    stat.PlayerBlocks = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayBlocks)).Text);
                }
                catch (Exception)
                {
                    stat.PlayerBlocks = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayBlocks + "/a")).Text);
                }

                try
                {
                    stat.PlayerTurnovers = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayTurnovers)).Text);
                }
                catch (Exception)
                {
                    stat.PlayerTurnovers = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayTurnovers + "/a")).Text);
                }

                try
                {
                    stat.PlayerSteals = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwaySteals)).Text);
                }
                catch (Exception)
                {
                    stat.PlayerSteals = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwaySteals + "/a")).Text);
                }

                stat.PlayerPoints = stat.PlayerFGM * 2 + stat.Player3PM + stat.PlayerFTM;
                Stats.Add(stat);
            }

            return Stats;
        }
        public List<PlayerStatsQuarter> PlayerStatQuarterScraper(ChromeDriver driver, int GameNo, int QuarterNo)
        {
            Thread.Sleep(1500);
            string AwayTeam =
                "/html/body/div[1]/div[2]/div[4]/section[2]/div[1]/h1/span";
            string HomeTeam =
                "/html/body/div[1]/div[2]/div[4]/section[3]/div[1]/h1/span";
            List<Players> homePlayers = new List<Players>();
            List<Players> awayPlayers = new List<Players>();
            Team Home = _helpers.GetTeamEnumByTeamName(driver.FindElement(OpenQA.Selenium.By.XPath(HomeTeam)).Text.Replace(" ", "").ToUpper()
                .Replace('İ', 'I'));
            Team Away = _helpers.GetTeamEnumByTeamName(driver.FindElement(OpenQA.Selenium.By.XPath(AwayTeam)).Text.Replace(" ", "").ToUpper()
                .Replace('İ', 'I'));
            int line = 25;
            for (; line >= 1; line--)
            {
                try
                {
                    int.Parse(driver
                        .FindElement(OpenQA.Selenium.By.XPath(
                            "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + line +
                            "]/td[21]")).Text);
                    Players Player = _helpers.GetPlayerWithName(
                        driver.FindElement(OpenQA.Selenium.By.XPath(
                            "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + line +
                            "]/td[1]/div/a/span")).Text);
                    Player.line = line;
                    awayPlayers.Add(Player);
                }
                catch (Exception)
                {
                    continue;
                }
            }

            line = 25;
            for (; line >= 1; line--)
            {
                try
                {
                    int.Parse(driver
                        .FindElement(OpenQA.Selenium.By.XPath(
                            "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + line +
                            "]/td[21]")).Text);
                    Players Player = _helpers.GetPlayerWithName(
                        driver.FindElement(OpenQA.Selenium.By.XPath(
                            "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + line +
                            "]/td[1]/div/a/span")).Text);
                    Player.line = line;
                    homePlayers.Add(Player);
                }
                catch (Exception)
                {
                    continue;
                }
            }
            List<PlayerStatsQuarter> Stats = new List<PlayerStatsQuarter>();
            foreach (var player in homePlayers)
            {
                PlayerStatsQuarter stat = new PlayerStatsQuarter();
                stat.Player = player;
                stat.GameNo = GameNo;
                stat.QuarterNo = QuarterNo;
                stat.Team = Home;
                string HomeMinutes =
                    "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[2]";
                string HomeFGM =
                    "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[3]";
                string HomeFGA =
                    "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[4]";
                string Home3PM =
                    "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[6]";
                string Home3PA =
                    "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[7]";
                string HomeFTM =
                    "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[9]";
                string HomeFTA =
                    "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[10]";
                string HomeOffensiveRebounds =
                    "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[12]";
                string HomeDefensiveRebounds =
                    "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[13]";
                string HomeAssists =
                    "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[15]";
                string HomeSteals =
                    "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[16]";
                string HomeBlocks =
                    "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[17]";
                string HomeTurnovers =
                    "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[18]";
                string HomeFouls =
                    "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[19]";
                string HomePlusMinus =
                    "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[21]";

                string time = driver.FindElement(OpenQA.Selenium.By.XPath(HomeMinutes)).Text;
                stat.Minutes = int.Parse(time.Substring(0, time.IndexOf(':')));
                stat.Seconds = int.Parse(time.Substring(time.IndexOf(':') + 1, 2));
                stat.PlayerFouls = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeFouls)).Text);
                stat.PlayerPlusMinus = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomePlusMinus)).Text);
                try
                {
                    stat.PlayerFGA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeFGA)).Text);
                }
                catch (Exception)
                {
                    stat.PlayerFGA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeFGA + "/a")).Text);
                }

                try
                {
                    stat.PlayerFGM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeFGM)).Text);
                }
                catch (Exception)
                {
                    stat.PlayerFGM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeFGM + "/a")).Text);
                }

                try
                {
                    stat.Player3PA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(Home3PA)).Text);
                }
                catch (Exception)
                {
                    stat.Player3PA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(Home3PA + "/a")).Text);
                }

                try
                {
                    stat.Player3PM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(Home3PM)).Text);
                }
                catch (Exception)
                {
                    stat.Player3PM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(Home3PM + "/a")).Text);
                }

                try
                {
                    stat.PlayerFTA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeFTA)).Text);
                }
                catch (Exception)
                {
                    stat.PlayerFTA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeFTA + "/a")).Text);
                }

                try
                {
                    stat.PlayerFTM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeFTM)).Text);
                }
                catch (Exception)
                {
                    stat.PlayerFTM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeFTM + "/a")).Text);
                }

                try
                {
                    stat.PlayerAssists = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeAssists)).Text);
                }
                catch (Exception)
                {
                    stat.PlayerAssists = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeAssists + "/a")).Text);
                }

                try
                {
                    stat.PlayerDefensiveRebounds = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeDefensiveRebounds)).Text);
                }
                catch (Exception)
                {
                    stat.PlayerDefensiveRebounds = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeDefensiveRebounds + "/a")).Text);
                }

                try
                {
                    stat.PlayerOffensiveRebounds = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeOffensiveRebounds)).Text);
                }
                catch (Exception)
                {
                    stat.PlayerOffensiveRebounds = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeOffensiveRebounds + "/a")).Text);
                }

                try
                {
                    stat.PlayerBlocks = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeBlocks)).Text);
                }
                catch (Exception)
                {
                    stat.PlayerBlocks = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeBlocks + "/a")).Text);
                }

                try
                {
                    stat.PlayerTurnovers = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeTurnovers)).Text);
                }
                catch (Exception)
                {
                    stat.PlayerTurnovers = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeTurnovers + "/a")).Text);
                }

                try
                {
                    stat.PlayerSteals = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeSteals)).Text);
                }
                catch (Exception)
                {
                    stat.PlayerSteals = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeSteals + "/a")).Text);
                }

                stat.PlayerPoints = stat.PlayerFGM * 2 + stat.Player3PM + stat.PlayerFTM;
                Stats.Add(stat);
            }
            foreach (var player in awayPlayers)
            {
                PlayerStatsQuarter stat = new PlayerStatsQuarter();
                stat.Player = player;
                stat.GameNo = GameNo;
                stat.QuarterNo = QuarterNo;
                stat.Team = Away;
                string AwayMinutes =
                    "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[2]";
                string AwayFGM =
                    "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[3]";
                string AwayFGA =
                    "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[4]";
                string Away3PM =
                    "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[6]";
                string Away3PA =
                    "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[7]";
                string AwayFTM =
                    "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[9]";
                string AwayFTA =
                    "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[10]";
                string AwayOffensiveRebounds =
                    "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[12]";
                string AwayDefensiveRebounds =
                    "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[13]";
                string AwayAssists =
                    "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[15]";
                string AwaySteals =
                    "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[16]";
                string AwayBlocks =
                    "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[17]";
                string AwayTurnovers =
                    "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[18]";
                string AwayFouls =
                    "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[19]";
                string AwayPlusMinus =
                    "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[21]";

                string time = driver.FindElement(OpenQA.Selenium.By.XPath(AwayMinutes)).Text;
                stat.Minutes = int.Parse(time.Substring(0, time.IndexOf(':')));
                stat.Seconds = int.Parse(time.Substring(time.IndexOf(':') + 1, 2));
                stat.PlayerFouls = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayFouls)).Text);
                stat.PlayerPlusMinus = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayPlusMinus)).Text);
                try
                {
                    stat.PlayerFGA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayFGA)).Text);
                }
                catch (Exception)
                {
                    stat.PlayerFGA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayFGA + "/a")).Text);
                }

                try
                {
                    stat.PlayerFGM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayFGM)).Text);
                }
                catch (Exception)
                {
                    stat.PlayerFGM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayFGM + "/a")).Text);
                }

                try
                {
                    stat.Player3PA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(Away3PA)).Text);
                }
                catch (Exception)
                {
                    stat.Player3PA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(Away3PA + "/a")).Text);
                }

                try
                {
                    stat.Player3PM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(Away3PM)).Text);
                }
                catch (Exception)
                {
                    stat.Player3PM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(Away3PM + "/a")).Text);
                }

                try
                {
                    stat.PlayerFTA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayFTA)).Text);
                }
                catch (Exception)
                {
                    stat.PlayerFTA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayFTA + "/a")).Text);
                }

                try
                {
                    stat.PlayerFTM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayFTM)).Text);
                }
                catch (Exception)
                {
                    stat.PlayerFTM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayFTM + "/a")).Text);
                }

                try
                {
                    stat.PlayerAssists = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayAssists)).Text);
                }
                catch (Exception)
                {
                    stat.PlayerAssists = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayAssists + "/a")).Text);
                }

                try
                {
                    stat.PlayerDefensiveRebounds = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayDefensiveRebounds)).Text);
                }
                catch (Exception)
                {
                    stat.PlayerDefensiveRebounds = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayDefensiveRebounds + "/a")).Text);
                }

                try
                {
                    stat.PlayerOffensiveRebounds = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayOffensiveRebounds)).Text);
                }
                catch (Exception)
                {
                    stat.PlayerOffensiveRebounds = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayOffensiveRebounds + "/a")).Text);
                }

                try
                {
                    stat.PlayerBlocks = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayBlocks)).Text);
                }
                catch (Exception)
                {
                    stat.PlayerBlocks = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayBlocks + "/a")).Text);
                }

                try
                {
                    stat.PlayerTurnovers = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayTurnovers)).Text);
                }
                catch (Exception)
                {
                    stat.PlayerTurnovers = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayTurnovers + "/a")).Text);
                }

                try
                {
                    stat.PlayerSteals = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwaySteals)).Text);
                }
                catch (Exception)
                {
                    stat.PlayerSteals = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwaySteals + "/a")).Text);
                }

                stat.PlayerPoints = stat.PlayerFGM * 2 + stat.Player3PM + stat.PlayerFTM;
                Stats.Add(stat);
            }

            return Stats;
        }
        public List<PlayerStatsQuarter20_21> PlayerStatQuarterScraper20_21(ChromeDriver driver, int GameNo, int QuarterNo)
        {
            Thread.Sleep(1500);
            string AwayTeam =
                "/html/body/div[1]/div[2]/div[4]/section[2]/div[1]/h1/span";
            string HomeTeam =
                "/html/body/div[1]/div[2]/div[4]/section[3]/div[1]/h1/span";
            List<Players> homePlayers = new List<Players>();
            List<Players> awayPlayers = new List<Players>();
            Team Home = _helpers.GetTeamEnumByTeamName(driver.FindElement(OpenQA.Selenium.By.XPath(HomeTeam)).Text.Replace(" ", "").ToUpper()
                .Replace('İ', 'I'));
            Team Away = _helpers.GetTeamEnumByTeamName(driver.FindElement(OpenQA.Selenium.By.XPath(AwayTeam)).Text.Replace(" ", "").ToUpper()
                .Replace('İ', 'I'));
            int line = 16;
            for (; line >= 1; line--)
            {
                try
                {
                    int.Parse(driver
                        .FindElement(OpenQA.Selenium.By.XPath(
                            "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + line +
                            "]/td[21]")).Text);
                    Players Player = _helpers.GetPlayerWithName(
                        driver.FindElement(OpenQA.Selenium.By.XPath(
                            "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + line +
                            "]/td[1]/div/a/span")).Text);
                    Player.line = line;
                    awayPlayers.Add(Player);
                }
                catch (Exception)
                {
                    continue;
                }
            }

            line = 16;
            for (; line >= 1; line--)
            {
                try
                {
                    int.Parse(driver
                        .FindElement(OpenQA.Selenium.By.XPath(
                            "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + line +
                            "]/td[21]")).Text);
                    Players Player = _helpers.GetPlayerWithName(
                        driver.FindElement(OpenQA.Selenium.By.XPath(
                            "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + line +
                            "]/td[1]/div/a/span")).Text);
                    Player.line = line;
                    homePlayers.Add(Player);
                }
                catch (Exception)
                {
                    continue;
                }
            }
            List<PlayerStatsQuarter20_21> Stats = new List<PlayerStatsQuarter20_21>();
            foreach (var player in homePlayers)
            {
                PlayerStatsQuarter20_21 stat = new PlayerStatsQuarter20_21();
                stat.Player = player;
                stat.GameNo = GameNo;
                stat.QuarterNo = QuarterNo;
                stat.Team = Home;
                string HomeMinutes =
                    "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[2]";
                string HomeFGM =
                    "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[3]";
                string HomeFGA =
                    "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[4]";
                string Home3PM =
                    "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[6]";
                string Home3PA =
                    "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[7]";
                string HomeFTM =
                    "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[9]";
                string HomeFTA =
                    "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[10]";
                string HomeOffensiveRebounds =
                    "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[12]";
                string HomeDefensiveRebounds =
                    "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[13]";
                string HomeAssists =
                    "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[15]";
                string HomeSteals =
                    "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[16]";
                string HomeBlocks =
                    "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[17]";
                string HomeTurnovers =
                    "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[18]";
                string HomeFouls =
                    "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[19]";
                string HomePlusMinus =
                    "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[21]";

                string time = driver.FindElement(OpenQA.Selenium.By.XPath(HomeMinutes)).Text;
                stat.Minutes = int.Parse(time.Substring(0, time.IndexOf(':')));
                stat.Seconds = int.Parse(time.Substring(time.IndexOf(':') + 1, 2));
                stat.PlayerFouls = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeFouls)).Text);
                stat.PlayerPlusMinus = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomePlusMinus)).Text);
                try
                {
                    stat.PlayerFGA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeFGA)).Text);
                }
                catch (Exception)
                {
                    stat.PlayerFGA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeFGA + "/a")).Text);
                }

                try
                {
                    stat.PlayerFGM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeFGM)).Text);
                }
                catch (Exception)
                {
                    stat.PlayerFGM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeFGM + "/a")).Text);
                }

                try
                {
                    stat.Player3PA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(Home3PA)).Text);
                }
                catch (Exception)
                {
                    stat.Player3PA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(Home3PA + "/a")).Text);
                }

                try
                {
                    stat.Player3PM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(Home3PM)).Text);
                }
                catch (Exception)
                {
                    stat.Player3PM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(Home3PM + "/a")).Text);
                }

                try
                {
                    stat.PlayerFTA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeFTA)).Text);
                }
                catch (Exception)
                {
                    stat.PlayerFTA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeFTA + "/a")).Text);
                }

                try
                {
                    stat.PlayerFTM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeFTM)).Text);
                }
                catch (Exception)
                {
                    stat.PlayerFTM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeFTM + "/a")).Text);
                }

                try
                {
                    stat.PlayerAssists = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeAssists)).Text);
                }
                catch (Exception)
                {
                    stat.PlayerAssists = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeAssists + "/a")).Text);
                }

                try
                {
                    stat.PlayerDefensiveRebounds = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeDefensiveRebounds)).Text);
                }
                catch (Exception)
                {
                    stat.PlayerDefensiveRebounds = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeDefensiveRebounds + "/a")).Text);
                }

                try
                {
                    stat.PlayerOffensiveRebounds = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeOffensiveRebounds)).Text);
                }
                catch (Exception)
                {
                    stat.PlayerOffensiveRebounds = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeOffensiveRebounds + "/a")).Text);
                }

                try
                {
                    stat.PlayerBlocks = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeBlocks)).Text);
                }
                catch (Exception)
                {
                    stat.PlayerBlocks = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeBlocks + "/a")).Text);
                }

                try
                {
                    stat.PlayerTurnovers = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeTurnovers)).Text);
                }
                catch (Exception)
                {
                    stat.PlayerTurnovers = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeTurnovers + "/a")).Text);
                }

                try
                {
                    stat.PlayerSteals = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeSteals)).Text);
                }
                catch (Exception)
                {
                    stat.PlayerSteals = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeSteals + "/a")).Text);
                }

                stat.PlayerPoints = stat.PlayerFGM * 2 + stat.Player3PM + stat.PlayerFTM;
                Stats.Add(stat);
            }
            foreach (var player in awayPlayers)
            {
                PlayerStatsQuarter20_21 stat = new PlayerStatsQuarter20_21();
                stat.Player = player;
                stat.GameNo = GameNo;
                stat.QuarterNo = QuarterNo;
                stat.Team = Away;
                string AwayMinutes =
                    "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[2]";
                string AwayFGM =
                    "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[3]";
                string AwayFGA =
                    "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[4]";
                string Away3PM =
                    "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[6]";
                string Away3PA =
                    "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[7]";
                string AwayFTM =
                    "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[9]";
                string AwayFTA =
                    "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[10]";
                string AwayOffensiveRebounds =
                    "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[12]";
                string AwayDefensiveRebounds =
                    "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[13]";
                string AwayAssists =
                    "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[15]";
                string AwaySteals =
                    "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[16]";
                string AwayBlocks =
                    "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[17]";
                string AwayTurnovers =
                    "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[18]";
                string AwayFouls =
                    "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[19]";
                string AwayPlusMinus =
                    "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[21]";

                string time = driver.FindElement(OpenQA.Selenium.By.XPath(AwayMinutes)).Text;
                stat.Minutes = int.Parse(time.Substring(0, time.IndexOf(':')));
                stat.Seconds = int.Parse(time.Substring(time.IndexOf(':') + 1, 2));
                stat.PlayerFouls = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayFouls)).Text);
                stat.PlayerPlusMinus = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayPlusMinus)).Text);
                try
                {
                    stat.PlayerFGA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayFGA)).Text);
                }
                catch (Exception)
                {
                    stat.PlayerFGA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayFGA + "/a")).Text);
                }

                try
                {
                    stat.PlayerFGM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayFGM)).Text);
                }
                catch (Exception)
                {
                    stat.PlayerFGM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayFGM + "/a")).Text);
                }

                try
                {
                    stat.Player3PA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(Away3PA)).Text);
                }
                catch (Exception)
                {
                    stat.Player3PA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(Away3PA + "/a")).Text);
                }

                try
                {
                    stat.Player3PM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(Away3PM)).Text);
                }
                catch (Exception)
                {
                    stat.Player3PM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(Away3PM + "/a")).Text);
                }

                try
                {
                    stat.PlayerFTA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayFTA)).Text);
                }
                catch (Exception)
                {
                    stat.PlayerFTA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayFTA + "/a")).Text);
                }

                try
                {
                    stat.PlayerFTM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayFTM)).Text);
                }
                catch (Exception)
                {
                    stat.PlayerFTM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayFTM + "/a")).Text);
                }

                try
                {
                    stat.PlayerAssists = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayAssists)).Text);
                }
                catch (Exception)
                {
                    stat.PlayerAssists = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayAssists + "/a")).Text);
                }

                try
                {
                    stat.PlayerDefensiveRebounds = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayDefensiveRebounds)).Text);
                }
                catch (Exception)
                {
                    stat.PlayerDefensiveRebounds = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayDefensiveRebounds + "/a")).Text);
                }

                try
                {
                    stat.PlayerOffensiveRebounds = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayOffensiveRebounds)).Text);
                }
                catch (Exception)
                {
                    stat.PlayerOffensiveRebounds = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayOffensiveRebounds + "/a")).Text);
                }

                try
                {
                    stat.PlayerBlocks = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayBlocks)).Text);
                }
                catch (Exception)
                {
                    stat.PlayerBlocks = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayBlocks + "/a")).Text);
                }

                try
                {
                    stat.PlayerTurnovers = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayTurnovers)).Text);
                }
                catch (Exception)
                {
                    stat.PlayerTurnovers = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayTurnovers + "/a")).Text);
                }

                try
                {
                    stat.PlayerSteals = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwaySteals)).Text);
                }
                catch (Exception)
                {
                    stat.PlayerSteals = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwaySteals + "/a")).Text);
                }

                stat.PlayerPoints = stat.PlayerFGM * 2 + stat.Player3PM + stat.PlayerFTM;
                Stats.Add(stat);
            }

            return Stats;
        }
        public List<PlayerStatsQuarter19_20> PlayerStatQuarterScraper19_20(ChromeDriver driver, int GameNo, int QuarterNo)
        {
            Thread.Sleep(1500);
            string AwayTeam =
                "/html/body/div[1]/div[2]/div[4]/section[2]/div[1]/h1/span";
            string HomeTeam =
                "/html/body/div[1]/div[2]/div[4]/section[3]/div[1]/h1/span";
            List<Players> homePlayers = new List<Players>();
            List<Players> awayPlayers = new List<Players>();
            Team Home = _helpers.GetTeamEnumByTeamName(driver.FindElement(OpenQA.Selenium.By.XPath(HomeTeam)).Text.Replace(" ", "").ToUpper()
                .Replace('İ', 'I'));
            Team Away = _helpers.GetTeamEnumByTeamName(driver.FindElement(OpenQA.Selenium.By.XPath(AwayTeam)).Text.Replace(" ", "").ToUpper()
                .Replace('İ', 'I'));
            int line = 16;
            for (; line >= 1; line--)
            {
                try
                {
                    int.Parse(driver
                        .FindElement(OpenQA.Selenium.By.XPath(
                            "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + line +
                            "]/td[21]")).Text);
                    Players Player = _helpers.GetPlayerWithName(
                        driver.FindElement(OpenQA.Selenium.By.XPath(
                            "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + line +
                            "]/td[1]/div/a/span")).Text);
                    Player.line = line;
                    awayPlayers.Add(Player);
                }
                catch (Exception)
                {
                    continue;
                }
            }

            line = 16;
            for (; line >= 1; line--)
            {
                try
                {
                    int.Parse(driver
                        .FindElement(OpenQA.Selenium.By.XPath(
                            "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + line +
                            "]/td[21]")).Text);
                    Players Player = _helpers.GetPlayerWithName(
                        driver.FindElement(OpenQA.Selenium.By.XPath(
                            "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + line +
                            "]/td[1]/div/a/span")).Text);
                    Player.line = line;
                    homePlayers.Add(Player);
                }
                catch (Exception)
                {
                    continue;
                }
            }
            List<PlayerStatsQuarter19_20> Stats = new List<PlayerStatsQuarter19_20>();
            foreach (var player in homePlayers)
            {
                PlayerStatsQuarter19_20 stat = new PlayerStatsQuarter19_20();
                stat.Player = player;
                stat.GameNo = GameNo;
                stat.QuarterNo = QuarterNo;
                stat.Team = Home;
                string HomeMinutes =
                    "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[2]";
                string HomeFGM =
                    "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[3]";
                string HomeFGA =
                    "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[4]";
                string Home3PM =
                    "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[6]";
                string Home3PA =
                    "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[7]";
                string HomeFTM =
                    "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[9]";
                string HomeFTA =
                    "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[10]";
                string HomeOffensiveRebounds =
                    "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[12]";
                string HomeDefensiveRebounds =
                    "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[13]";
                string HomeAssists =
                    "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[15]";
                string HomeSteals =
                    "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[16]";
                string HomeBlocks =
                    "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[17]";
                string HomeTurnovers =
                    "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[18]";
                string HomeFouls =
                    "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[19]";
                string HomePlusMinus =
                    "/html/body/div[1]/div[2]/div[4]/section[3]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[21]";

                string time = driver.FindElement(OpenQA.Selenium.By.XPath(HomeMinutes)).Text;
                stat.Minutes = int.Parse(time.Substring(0, time.IndexOf(':')));
                stat.Seconds = int.Parse(time.Substring(time.IndexOf(':') + 1, 2));
                stat.PlayerFouls = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeFouls)).Text);
                stat.PlayerPlusMinus = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomePlusMinus)).Text);
                try
                {
                    stat.PlayerFGA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeFGA)).Text);
                }
                catch (Exception)
                {
                    stat.PlayerFGA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeFGA + "/a")).Text);
                }

                try
                {
                    stat.PlayerFGM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeFGM)).Text);
                }
                catch (Exception)
                {
                    stat.PlayerFGM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeFGM + "/a")).Text);
                }

                try
                {
                    stat.Player3PA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(Home3PA)).Text);
                }
                catch (Exception)
                {
                    stat.Player3PA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(Home3PA + "/a")).Text);
                }

                try
                {
                    stat.Player3PM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(Home3PM)).Text);
                }
                catch (Exception)
                {
                    stat.Player3PM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(Home3PM + "/a")).Text);
                }

                try
                {
                    stat.PlayerFTA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeFTA)).Text);
                }
                catch (Exception)
                {
                    stat.PlayerFTA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeFTA + "/a")).Text);
                }

                try
                {
                    stat.PlayerFTM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeFTM)).Text);
                }
                catch (Exception)
                {
                    stat.PlayerFTM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeFTM + "/a")).Text);
                }

                try
                {
                    stat.PlayerAssists = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeAssists)).Text);
                }
                catch (Exception)
                {
                    stat.PlayerAssists = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeAssists + "/a")).Text);
                }

                try
                {
                    stat.PlayerDefensiveRebounds = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeDefensiveRebounds)).Text);
                }
                catch (Exception)
                {
                    stat.PlayerDefensiveRebounds = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeDefensiveRebounds + "/a")).Text);
                }

                try
                {
                    stat.PlayerOffensiveRebounds = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeOffensiveRebounds)).Text);
                }
                catch (Exception)
                {
                    stat.PlayerOffensiveRebounds = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeOffensiveRebounds + "/a")).Text);
                }

                try
                {
                    stat.PlayerBlocks = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeBlocks)).Text);
                }
                catch (Exception)
                {
                    stat.PlayerBlocks = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeBlocks + "/a")).Text);
                }

                try
                {
                    stat.PlayerTurnovers = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeTurnovers)).Text);
                }
                catch (Exception)
                {
                    stat.PlayerTurnovers = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeTurnovers + "/a")).Text);
                }

                try
                {
                    stat.PlayerSteals = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeSteals)).Text);
                }
                catch (Exception)
                {
                    stat.PlayerSteals = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(HomeSteals + "/a")).Text);
                }

                stat.PlayerPoints = stat.PlayerFGM * 2 + stat.Player3PM + stat.PlayerFTM;
                Stats.Add(stat);
            }
            foreach (var player in awayPlayers)
            {
                PlayerStatsQuarter19_20 stat = new PlayerStatsQuarter19_20();
                stat.Player = player;
                stat.GameNo = GameNo;
                stat.QuarterNo = QuarterNo;
                stat.Team = Away;
                string AwayMinutes =
                    "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[2]";
                string AwayFGM =
                    "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[3]";
                string AwayFGA =
                    "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[4]";
                string Away3PM =
                    "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[6]";
                string Away3PA =
                    "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[7]";
                string AwayFTM =
                    "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[9]";
                string AwayFTA =
                    "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[10]";
                string AwayOffensiveRebounds =
                    "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[12]";
                string AwayDefensiveRebounds =
                    "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[13]";
                string AwayAssists =
                    "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[15]";
                string AwaySteals =
                    "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[16]";
                string AwayBlocks =
                    "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[17]";
                string AwayTurnovers =
                    "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[18]";
                string AwayFouls =
                    "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[19]";
                string AwayPlusMinus =
                    "/html/body/div[1]/div[2]/div[4]/section[2]/div[2]/div[2]/div/table/tbody/tr[" + player.line + "]/td[21]";

                string time = driver.FindElement(OpenQA.Selenium.By.XPath(AwayMinutes)).Text;
                stat.Minutes = int.Parse(time.Substring(0, time.IndexOf(':')));
                stat.Seconds = int.Parse(time.Substring(time.IndexOf(':') + 1, 2));
                stat.PlayerFouls = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayFouls)).Text);
                stat.PlayerPlusMinus = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayPlusMinus)).Text);
                try
                {
                    stat.PlayerFGA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayFGA)).Text);
                }
                catch (Exception)
                {
                    stat.PlayerFGA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayFGA + "/a")).Text);
                }

                try
                {
                    stat.PlayerFGM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayFGM)).Text);
                }
                catch (Exception)
                {
                    stat.PlayerFGM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayFGM + "/a")).Text);
                }

                try
                {
                    stat.Player3PA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(Away3PA)).Text);
                }
                catch (Exception)
                {
                    stat.Player3PA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(Away3PA + "/a")).Text);
                }

                try
                {
                    stat.Player3PM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(Away3PM)).Text);
                }
                catch (Exception)
                {
                    stat.Player3PM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(Away3PM + "/a")).Text);
                }

                try
                {
                    stat.PlayerFTA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayFTA)).Text);
                }
                catch (Exception)
                {
                    stat.PlayerFTA = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayFTA + "/a")).Text);
                }

                try
                {
                    stat.PlayerFTM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayFTM)).Text);
                }
                catch (Exception)
                {
                    stat.PlayerFTM = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayFTM + "/a")).Text);
                }

                try
                {
                    stat.PlayerAssists = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayAssists)).Text);
                }
                catch (Exception)
                {
                    stat.PlayerAssists = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayAssists + "/a")).Text);
                }

                try
                {
                    stat.PlayerDefensiveRebounds = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayDefensiveRebounds)).Text);
                }
                catch (Exception)
                {
                    stat.PlayerDefensiveRebounds = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayDefensiveRebounds + "/a")).Text);
                }

                try
                {
                    stat.PlayerOffensiveRebounds = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayOffensiveRebounds)).Text);
                }
                catch (Exception)
                {
                    stat.PlayerOffensiveRebounds = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayOffensiveRebounds + "/a")).Text);
                }

                try
                {
                    stat.PlayerBlocks = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayBlocks)).Text);
                }
                catch (Exception)
                {
                    stat.PlayerBlocks = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayBlocks + "/a")).Text);
                }

                try
                {
                    stat.PlayerTurnovers = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayTurnovers)).Text);
                }
                catch (Exception)
                {
                    stat.PlayerTurnovers = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwayTurnovers + "/a")).Text);
                }

                try
                {
                    stat.PlayerSteals = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwaySteals)).Text);
                }
                catch (Exception)
                {
                    stat.PlayerSteals = int.Parse(driver.FindElement(OpenQA.Selenium.By.XPath(AwaySteals + "/a")).Text);
                }

                stat.PlayerPoints = stat.PlayerFGM * 2 + stat.Player3PM + stat.PlayerFTM;
                Stats.Add(stat);
            }

            return Stats;
        }
    }
}
