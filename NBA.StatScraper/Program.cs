using System.Threading.Tasks;
using NBA.Services.Calculations;

namespace NBA.StatScraper
{
    using NBA.Models;
    using NBA.Models.DataContext;
    using NBA.Services.Abstraction;
    using NBA.Services.Interfaces;
    using System;
    using OpenQA.Selenium.Chrome;
    using NBA.Services.Helpers;
    using System.Threading;
    using NBA.Models.Entities;
    using System.Collections.Generic;
    using System.Linq;
    using NBA.Services.Repositories;
    using System.Net.Http;
    using System.IO;

    public class Program
    {
        static void Main(string[] args)
        {
            INBAContext _db = new NBAContext();
            IUnitOfWork _uw = new UnitOfWork(_db);
            IPlayerRepository playerRepository = new PlayerRepository(_db, _uw);
            IHelpers _helpers = new Helpers(_db);
            IGameTimesRepository _gameTimesRepository = new GameTimesRepository(_db, _uw);
            IFullSeasonRepository _fullSeasonRepository = new FullSeasonRepository(_db, _uw, _gameTimesRepository);
            IFullSeasonQuartersRepository _fullSeasonQuartersRepository =
                new FullSeasonQuartersRepository(_db, _uw, _gameTimesRepository);
            IPlayerRepository _playerRepository = new PlayerRepository(_db, _uw);
            IPlayerStatRepository _playerStatRepository = new PlayerStatRepository(_db, _uw, _gameTimesRepository);
            IPlayerStatQuarterRepository _playerStatQuarterRepository = new PlayerStatQuarterRepository(_db, _uw);
            IStatScraper _statScraper = new StatScraper(_helpers, _gameTimesRepository);
            IMathOperations _mathOperations = new MathOperations();
            ISimulator _simulator = new Simulator(_mathOperations, _gameTimesRepository, _fullSeasonRepository,
                _fullSeasonQuartersRepository);


            //FullScraper

            var result = (ServiceResult<List<GameTime>>)_gameTimesRepository.GetGamesTill(DateTime.Now.AddHours(-8));
            var gamesplayed = result.Data;
            var driverOptions = new ChromeOptions();
            driverOptions.AddArguments(new List<string>() { "headless", "disable-gpu" });
            var driver = new ChromeDriver(driverOptions);
            driver.Manage().Window.Maximize();
            //Thread.Sleep(3000);
            driver.Navigate().GoToUrl("https://www.nba.com/schedule");
            //Thread.Sleep(3000);
            int pageNo = 0;
            for (int i = 0; i < gamesplayed.Count; i++)
            {
                pageNo++;
                try
                {
                    int gameNo = gamesplayed[i].GameNo;
                    var stats = _db.FullSeason.FirstOrDefault(x => x.GameNo == gameNo);
                    if (stats != null)
                        continue;
                    if (pageNo % 20 == 0)
                    {
                        driver.Dispose();
                        driver = new ChromeDriver(driverOptions);
                        //driver.Manage().Window.Maximize();
                        //Thread.Sleep(1000);
                        //driver.Navigate().GoToUrl("https://www.nba.com/schedule");
                        //Thread.Sleep(1000);
                    }
                    driver.Navigate().GoToUrl("https://www.nba.com/game/002210" + gameNo.ToString("0000") + "/box-score");
                    var fullseason = _statScraper.GameScraper(driver, gameNo,
                        "https://www.nba.com/game/002210" + gameNo.ToString("0000"));
                    //Thread.Sleep(1000);
                    var playerstats = _statScraper.PlayerStatScraper(driver, gameNo);
                    var quarterstats = new List<FullSeasonQuarters>();
                    string period = driver.FindElement(OpenQA.Selenium.By.XPath("/html/body/div[1]/div[2]/div[4]/section[1]/div/form/div[2]/label/div/select")).Text;
                    var playerstatsquarter = new List<PlayerStatsQuarter>();
                    int TotalQuarters;
                    switch (period.Length)
                    {
                        case 47:
                            TotalQuarters = 4;
                            break;
                        case 52:
                            TotalQuarters = 5;
                            break;
                        default:
                            TotalQuarters = (period.Length - 55) / 5 + 4;
                            break;
                    }
                    for (int k = 1; k <= TotalQuarters; k++)
                    {
                        driver.Navigate().GoToUrl("https://www.nba.com/game/002210" +
                                                  gameNo.ToString("0000") + GetQuarter(k));
                        quarterstats.Add(_statScraper.QuarterScraper(driver, gameNo, k));
                        //Thread.Sleep(1000);
                        playerstatsquarter.AddRange(_statScraper.PlayerStatQuarterScraper(driver, gameNo, k));
                    }

                    _fullSeasonRepository.AddGame(fullseason);
                    foreach (var stat in playerstats)
                    {
                        _playerStatRepository.AddPlayerStat(stat);
                    }

                    foreach (var stat in quarterstats)
                    {
                        _fullSeasonQuartersRepository.AddQuarter(stat);
                    }

                    foreach (var stat in playerstatsquarter)
                    {
                        _playerStatQuarterRepository.AddPlayerStatsQuarter(stat);
                    }
                }
                catch (Exception)
                {
                    i--;
                    Thread.Sleep(1000);
                    //driver.Navigate().Refresh();
                }
            }
            driver.Dispose();



            //Predictions

            var result2 = (ServiceResult<List<GameTime>>)_gameTimesRepository.GetGamesToBePlayedToday();
            var gamesToBePlayed = result2.Data;
            foreach (var game in gamesToBePlayed)
            {
                if (_db.GamePredictions.FirstOrDefault(x => x.GameNo == game.GameNo) != null)
                    continue;
                GamePredictions prediction = _simulator.FullMatchSimulator(game.HomeTeam, game.AwayTeam, game.GameNo);
                List<QuarterPredictions> predictions = new List<QuarterPredictions>();
                for (int i = 1; i <= 4; i++)
                {
                    predictions.Add(_simulator.QuarterSimulator(game.HomeTeam, game.AwayTeam, i, game.GameNo));
                }
                _db.GamePredictions.Add(prediction);
                _db.QuarterPredictions.AddRange(predictions);
                _uw.Commit();
            }


            //Predictions To Text File

            string date = DateTime.Now.ToLongDateString();
            string fileName = $@"C:\Users\caner\Desktop\NBA\{date}.txt";
            var quarters = new List<QuarterPredictions>();
            foreach (var game in gamesToBePlayed)
            {
                quarters.AddRange(_db.QuarterPredictions.Where(x => x.GameNo == game.GameNo));
            }
            using (StreamWriter sw = File.CreateText(fileName))
            {
                foreach(var quarter in quarters)
                {
                    sw.WriteLine("Maç Kodu : " + quarter.GameNo + "\tQ" + quarter.QuarterNo + "\tTarih : " + gamesToBePlayed.First(x => x.GameNo == quarter.GameNo).GameDate.ToShortTimeString() + "\n");
                    sw.WriteLine(_helpers.GetTeamNameByEnum(quarter.HomeTeam) + $"\t{quarter.HomePoints} - {quarter.AwayPoints} \t" + _helpers.GetTeamNameByEnum(quarter.AwayTeam) + "\n");
                    sw.WriteLine($"\tFGA\t {quarter.HomeFGA} - {quarter.AwayFGA}");
                    sw.WriteLine($"\tFGM\t {quarter.HomeFGM} - {quarter.AwayFGM}");
                    sw.WriteLine($"\t3PA\t {quarter.Home3PA} - {quarter.Away3PA}");
                    sw.WriteLine($"\t3PM\t {quarter.Home3PM} - {quarter.Away3PM}");
                    sw.WriteLine($"\tFTA\t {quarter.HomeFTA} - {quarter.AwayFTA}");
                    sw.WriteLine($"\tFTM\t {quarter.HomeFTM} - {quarter.AwayFTM}");
                    sw.WriteLine($"\tAST\t {quarter.HomeAssists} - {quarter.AwayAssists}");
                    sw.WriteLine($"\tOREB\t {quarter.HomeOffensiveRebounds} - {quarter.AwayOffensiveRebounds}");
                    sw.WriteLine($"\tDREB\t {quarter.HomeDefensiveRebounds} - {quarter.AwayDefensiveRebounds}");
                    sw.WriteLine($"\tSTL\t {quarter.HomeSteals} - {quarter.AwaySteals}");
                    sw.WriteLine($"\tBLK\t {quarter.HomeBlocks} - {quarter.AwayBlocks}");
                    sw.WriteLine($"\tTO\t {quarter.HomeTurnovers} - {quarter.AwayTurnovers}\n\n");
                }
            }



            ////PlayerInfoScraper

            //var driverOptions = new ChromeOptions();
            //driverOptions.AddArguments(new List<string>() { "headless", "disable-gpu" });
            //using (var driver = new ChromeDriver(driverOptions))
            //{
            //    driver.Manage().Window.Maximize();
            //    List<Players> players = _statScraper.PlayerInfoScraper(driver, 1);
            //    foreach (var player in players)
            //    {
            //        try
            //        {
            //            var asd = (ServiceResult<Players>)_playerRepository.GetPlayerWithName(player.Name);
            //            if (asd.Data.Number != player.Number || asd.Data.Team == player.Team)
            //                _playerRepository.UpdatePlayer(asd.Data);
            //        }
            //        catch (Exception)
            //        {
            //            _playerRepository.AddPlayer(player);
            //        }
            //    }
            //}

            ////GameTime Scraper
            //int i = 0;
            //int k = 1;
            //var games = (ServiceResult<List<GameTime>>)_gameTimesRepository.GetFullSeason();
            //for (; i < 13; i++)
            //{
            //    var driverOptions = new ChromeOptions();
            //    driverOptions.AddArguments(new List<string>() { "headless", "disable-gpu" });
            //    using (var driver = new ChromeDriver(driverOptions))
            //    {
            //        //driver.Manage().Window.Maximize();
            //        //driver.Navigate().GoToUrl("https://www.nba.com/schedule");
            //        //Thread.Sleep(1000);
            //        //driver.FindElement(OpenQA.Selenium.By.XPath("/html/body/div[2]/div[3]/div/div/div[2]/div/div/button")).Click();
            //        for (; k < 100; k++)
            //        {
            //            if (i * 100 + k > 1230)
            //                return;
            //            try
            //            {
            //                driver.Navigate().GoToUrl("https://www.nba.com/game/002210" + (i * 100 + k).ToString("0000"));
            //                GameTime game = new GameTime();
            //                game.GameNo = i * 100 + k;
            //                string source = driver.PageSource;
            //                int index1 = source.IndexOf("NBA Game - ");
            //                source = source.Substring(index1 + 11);
            //                index1 = source.IndexOf(' ');
            //                game.AwayTeam = _helpers.GetTeamEnumByTeamMascotName(source.Substring(0, index1).ToUpper().Replace('İ', 'I'));
            //                source = source.Substring(index1 + 5);
            //                if (game.AwayTeam == Team.Error)
            //                {
            //                    game.AwayTeam = Team.PortlandTrailBlazers;
            //                    source = source.Substring(8);
            //                }
            //                index1 = source.IndexOf(',');
            //                game.HomeTeam = _helpers.GetTeamEnumByTeamMascotName(source.Substring(0, index1 - 1).ToUpper().Replace('İ', 'I'));
            //                int index2 = source.IndexOf("startDate");
            //                string date = source.Substring(index2 + 12, 10);
            //                string hour = source.Substring(index2 + 23, 8);
            //                game.GameDate = DateTime.Parse(date + " " + hour);
            //                var old = games.Data.FirstOrDefault(x => x.GameNo == game.GameNo);
            //                if (old == null)
            //                    _gameTimesRepository.AddGameTime(game);
            //                else if (old.GameDate == game.GameDate)
            //                    continue;
            //                _gameTimesRepository.UpdateGameTime(game);

            //            }
            //            catch (Exception)
            //            {
            //                k--;
            //            }
            //        }
            //    }
            //    k = 0;
            //}

            //// gametime away portland update

            //var gametimes = _db.GameTime.Where(x=>x.HomeTeam == Team.Error).ToList();
            //using (var driver = new ChromeDriver())
            //{
            //    foreach (var gametime in gametimes)
            //    {
            //        driver.Navigate().GoToUrl("https://www.nba.com/game/002210" + gametime.GameNo.ToString("0000"));
            //        string source = driver.PageSource;
            //        int index1 = source.IndexOf("NBA Game - ");
            //        source = source.Substring(index1 + 11);
            //        index1 = source.IndexOf(' ');
            //        gametime.AwayTeam = _helpers.GetTeamEnumByTeamMascotName(source.Substring(0, index1).ToUpper().Replace('İ', 'I'));
            //        source = source.Substring(index1 + 5);
            //        if (gametime.AwayTeam == Team.Error)
            //        {
            //            gametime.AwayTeam = Team.PortlandTrailBlazers;
            //            source = source.Substring(8);
            //        }
            //        index1 = source.IndexOf(',');
            //        gametime.HomeTeam = _helpers.GetTeamEnumByTeamMascotName(source.Substring(0, index1 - 1).ToUpper().Replace('İ', 'I'));
            //        int index2 = source.IndexOf("startDate");
            //        string date = source.Substring(index2 + 12, 10);
            //        string hour = source.Substring(index2 + 23, 8);
            //    }
            //}
            //_uw.Commit();

            ////GameTime20_21 Scraper

            //int i = 0;
            //int j = 1;
            //try
            //{
            //    int lastgame = _db.GameTime20_21.OrderByDescending(x => x.GameNo).First().GameNo;
            //    i = (lastgame + 1) / 100;
            //    j = (lastgame + 1) % 100;
            //}
            //catch (Exception)
            //{

            //}

            //for (; i < 11; i++)
            //{
            //    using (var driver = new ChromeDriver())
            //    {
            //        driver.Manage().Window.Maximize();
            //        driver.Navigate().GoToUrl("https://www.nba.com/schedule");
            //        Thread.Sleep(1000);
            //        driver.FindElementByXPath("/html/body/div[2]/div[3]/div/div/div[2]/div/div/button").Click();
            //        for (; j < 100; j++)
            //        {
            //            if (i * 100 + j == 1078)
            //                return;
            //            if (i * 100 + j == 0)
            //                continue;
            //            driver.Manage().Window.Maximize();
            //            driver.Navigate().GoToUrl("https://www.nba.com/game/002200" + (i * 100 + j).ToString("0000"));
            //            GameTime20_21 gameTime = new GameTime20_21();
            //            gameTime.GameNo = i * 100 + j;
            //            string source = driver.PageSource;
            //            int index1 = source.IndexOf("NBA Game - ");
            //            source = source.Substring(index1 + 11);
            //            index1 = source.IndexOf(' ');
            //            gameTime.AwayTeam = _helpers.GetTeamEnumByTeamMascotName(source.Substring(0, index1).ToUpper().Replace('İ', 'I'));
            //            source = source.Substring(index1 + 5);
            //            index1 = source.IndexOf(',');
            //            gameTime.HomeTeam = _helpers.GetTeamEnumByTeamMascotName(source.Substring(0, index1 - 1).ToUpper().Replace('İ', 'I'));
            //            int index2 = source.IndexOf("startDate");
            //            string date = source.Substring(index2 + 12, 10);
            //            string hour = source.Substring(index2 + 23, 8);
            //            gameTime.GameDate = DateTime.Parse(date + " " + hour);
            //            _gameTimesRepository.AddGameTime20_21(gameTime);
            //        }
            //    }
            //}

            ////GameTime19_20 Scraper

            //int i = 0;
            //int j = 1;
            //try
            //{
            //    int lastgame = _db.GameTime19_20.OrderByDescending(x => x.GameNo).First().GameNo;
            //    i = (lastgame + 1) / 100;
            //    j = (lastgame + 1) % 100;
            //}
            //catch (Exception)
            //{

            //}
            //for (; i < 10; i++)
            //{
            //    using (var driver = new ChromeDriver())
            //    {
            //        driver.Manage().Window.Maximize();
            //        driver.Navigate().GoToUrl("https://www.nba.com/schedule");
            //        Thread.Sleep(1000);
            //        driver.FindElementByXPath("/html/body/div[2]/div[3]/div/div/div[2]/div/div/button").Click();
            //        for (; j < 100; j++)
            //        {
            //            switch (i * 100 + j)
            //            {
            //                case 974:
            //                    return;
            //                case 0:
            //                case 707:
            //                case 972:
            //                    continue;
            //            }

            //            try
            //            {
            //                driver.Manage().Window.Maximize();
            //                driver.Navigate().GoToUrl("https://www.nba.com/game/002190" + (i * 100 + j).ToString("0000"));
            //                GameTime19_20 gameTime = new GameTime19_20();
            //                gameTime.GameNo = i * 100 + j;
            //                string source = driver.PageSource;
            //                int index1 = source.IndexOf("NBA Game - ");
            //                source = source.Substring(index1 + 11);
            //                index1 = source.IndexOf(' ');
            //                gameTime.AwayTeam = _helpers.GetTeamEnumByTeamMascotName(source.Substring(0, index1).ToUpper().Replace('İ', 'I'));
            //                source = source.Substring(index1 + 5);
            //                index1 = source.IndexOf(',');
            //                gameTime.HomeTeam = _helpers.GetTeamEnumByTeamMascotName(source.Substring(0, index1 - 1).ToUpper().Replace('İ', 'I'));
            //                int index2 = source.IndexOf("startDate");
            //                string date = source.Substring(index2 + 12, 10);
            //                string hour = source.Substring(index2 + 23, 8);
            //                gameTime.GameDate = DateTime.Parse(date + " " + hour);
            //                _gameTimesRepository.AddGameTime19_20(gameTime);
            //            }
            //            catch (Exception)
            //            {
            //                j--;
            //            }
            //        }
            //    }
            //}





            ////FullSeason20_21 Scraper

            //int k = 0;
            //int l = 1;
            //try
            //{
            //    int lastgame = _db.FullSeason20_21.OrderByDescending(x => x.GameNo).First().GameNo;
            //    k = (lastgame + 1) / 100;
            //    l = (lastgame + 1) % 100;
            //}
            //catch (Exception)
            //{

            //}

            //for (; k < 11; k++)
            //{
            //    using (var driver = new ChromeDriver())
            //    {
            //        driver.Manage().Window.Maximize();
            //        Thread.Sleep(3000);
            //        driver.Navigate().GoToUrl("https://www.nba.com/schedule");
            //        Thread.Sleep(3000);
            //        driver.FindElementById("onetrust-accept-btn-handler").Click();
            //        Thread.Sleep(3000);
            //        for (; l < 100; l++)
            //        {
            //            if (k * 100 + l == 1078)
            //                return;
            //            try
            //            {
            //                var game = _statScraper.GameScraper20_21(driver, 100 * k + l,
            //                    "https://www.nba.com/game/002200" + (k * 100 + l).ToString("0000"));
            //                _fullSeasonRepository.AddGame20_21(game);
            //            }
            //            catch (Exception)
            //            {
            //                l--;
            //            }
            //        }
            //    }

            //    l = 0;
            //}

            ////FullSeason19_20 Scraper

            //int i = 0;
            //int j = 1;
            //try
            //{
            //    int lastgame = _db.FullSeason19_20.OrderByDescending(x => x.GameNo).First().GameNo;
            //    i = (lastgame + 1) / 100;
            //    j = (lastgame + 1) % 100;
            //}
            //catch (Exception)
            //{

            //}

            //for (; i < 10; i++)
            //{
            //    using (var driver = new ChromeDriver())
            //    {
            //        driver.Manage().Window.Maximize();
            //        Thread.Sleep(3000);
            //        driver.Navigate().GoToUrl("https://www.nba.com/schedule");
            //        Thread.Sleep(3000);
            //        driver.FindElementById("onetrust-accept-btn-handler").Click();
            //        Thread.Sleep(3000);
            //        for (; j < 100; j++)
            //        {
            //            switch (i * 100 + j)
            //            {
            //                case 974:
            //                    return;
            //                case 0:
            //                case 707:
            //                case 972:
            //                    continue;
            //            }

            //            try
            //            {
            //                var game = _statScraper.GameScraper19_20(driver, 100 * i + j,
            //                    "https://www.nba.com/game/002190" + (i * 100 + j).ToString("0000"));
            //                _fullSeasonRepository.AddGame19_20(game);
            //                Thread.Sleep(3000);
            //            }
            //            catch (Exception)
            //            {
            //                j--;
            //            }
            //        }
            //    }

            //    j = 0;
            //}




            ////FullSeasonQuarters Scraper

            //int i = 0;
            //int j = 1;
            //try
            //{
            //    int lastgame = _db.FullSeasonQuarters.OrderByDescending(x => x.GameNo).First().GameNo;
            //    i = (lastgame + 1) / 25;
            //    j = (lastgame + 1) % 25;
            //}
            //catch (Exception)
            //{
            //    for (; i < 50; i++)
            //    {

            //    }
            //}

            ////FullSeasonQuarters20_21 Scraper

            //int i = 0;
            //int j = 1;
            //try
            //{
            //    int lastgame = _db.FullSeasonQuarters20_21.OrderByDescending(x => x.GameNo).First().GameNo;
            //    i = (lastgame + 1) / 25;
            //    j = (lastgame + 1) % 25;
            //}
            //catch (Exception)
            //{

            //}
            //for (; i < 44; i++)
            //{
            //    using (var driver = new ChromeDriver())
            //    {
            //        driver.Manage().Window.Maximize();
            //        Thread.Sleep(3000);
            //        driver.Navigate().GoToUrl("https://www.nba.com/schedule");
            //        Thread.Sleep(3000);
            //        driver.FindElementById("onetrust-accept-btn-handler").Click();
            //        Thread.Sleep(3000);
            //        for (; j < 25; j++)
            //        {
            //            if (i * 25 + j > 1077)
            //                continue;
            //            int TotalQuarters = 0;
            //            for (int k = 0; k < 1; k++)
            //            {
            //                try
            //                {
            //                    driver.Navigate().GoToUrl("https://www.nba.com/game/002200" +
            //                                              (i * 25 + j).ToString("0000") + "/box-score");
            //                    string period = driver.FindElementByXPath(
            //                        "/html/body/div[1]/div[2]/div[4]/section[1]/div/form/div[2]/label/div/select").Text;
            //                    switch (period.Length)
            //                    {
            //                        case 47:
            //                            TotalQuarters = 4;
            //                            break;
            //                        case 52:
            //                            TotalQuarters = 5;
            //                            break;
            //                        default:
            //                            TotalQuarters = (period.Length - 55) / 5 + 4;
            //                            break;
            //                    }
            //                }
            //                catch (Exception)
            //                {
            //                    Thread.Sleep(3000);
            //                    k--;
            //                }
            //            }
            //            for (int k = 1; k < TotalQuarters + 1; k++)
            //            {
            //                try
            //                {
            //                    driver.Navigate().GoToUrl("https://www.nba.com/game/002200" +
            //                                              (i * 25 + j).ToString("0000") + GetTimeSpan(k));
            //                    Thread.Sleep(3000);
            //                    FullSeasonQuarters20_21 quarter =
            //                        _statScraper.QuarterScraper20_21(driver, i * 25 + j, k);
            //                    _fullSeasonQuartersRepository.AddQuarter20_21(quarter);
            //                }
            //                catch (Exception)
            //                {
            //                    Thread.Sleep(3000);
            //                    k--;
            //                }
            //            }
            //        }
            //    }
            //    j = 0;
            //}

            ////FullSeasonQuarters19_20 Scraper

            //int l = 0;
            //int m = 1;
            //try
            //{
            //    int lastgame = _db.FullSeasonQuarters19_20.OrderByDescending(x => x.GameNo).First().GameNo;
            //    l = (lastgame + 1) / 25;
            //    m = (lastgame + 1) % 25;
            //}
            //catch (Exception)
            //{

            //}
            //for (; l < 39; l++)
            //{
            //    using (var driver = new ChromeDriver())
            //    {
            //        driver.Manage().Window.Maximize();
            //        Thread.Sleep(3000);
            //        driver.Navigate().GoToUrl("https://www.nba.com/schedule");
            //        Thread.Sleep(3000);
            //        driver.FindElementById("onetrust-accept-btn-handler").Click();
            //        Thread.Sleep(3000);
            //        for (; m < 25; m++)
            //        {
            //            if (l * 25 + m > 973)
            //                continue;
            //            switch (l * 25 + m)
            //            {
            //                case 0:
            //                case 707:
            //                case 972:
            //                    continue;
            //            }
            //            int TotalQuarters = 0;
            //            for (int k = 0; k < 1; k++)
            //            {
            //                try
            //                {
            //                    if (m == 0)
            //                    {
            //                        Thread.Sleep(3000);
            //                    }
            //                    driver.Navigate().GoToUrl("https://www.nba.com/game/002190" +
            //                                              (l * 25 + m).ToString("0000") + "/box-score");
            //                    string period = driver.FindElementByXPath(
            //                        "/html/body/div[1]/div[2]/div[4]/section[1]/div/form/div[2]/label/div/select").Text;
            //                    switch (period.Length)
            //                    {
            //                        case 47:
            //                            TotalQuarters = 4;
            //                            break;
            //                        case 52:
            //                            TotalQuarters = 5;
            //                            break;
            //                        default:
            //                            TotalQuarters = (period.Length - 55) / 5 + 4;
            //                            break;
            //                    }
            //                }
            //                catch (Exception)
            //                {
            //                    Thread.Sleep(3000);
            //                    k--;
            //                }
            //            }
            //            for (int n = 1; n < TotalQuarters + 1; n++)
            //            {
            //                try
            //                {
            //                    if (m == 0)
            //                    {
            //                        Thread.Sleep(3000);
            //                    }
            //                    driver.Navigate().GoToUrl("https://www.nba.com/game/002190" +
            //                                              (l * 25 + m).ToString("0000") + GetTimeSpan(n));
            //                    Thread.Sleep(3000);
            //                    FullSeasonQuarters19_20 quarter =
            //                        _statScraper.QuarterScraper19_20(driver, l * 25 + m, n);
            //                    _fullSeasonQuartersRepository.AddQuarter19_20(quarter);
            //                }
            //                catch (Exception)
            //                {
            //                    Thread.Sleep(3000);
            //                    n--;
            //                }
            //            }
            //        }
            //    }
            //    m = 0;
            //}



            //PlayerStatScraper

            ////PlayerStatScraper20_21

            //int i = 0;
            //int j = 1;
            //try
            //{
            //    int gameNo = _db.PlayerStats20_21.OrderByDescending(x => x.GameNo).First().GameNo;
            //    i = (gameNo + 1) / 100;
            //    j = (gameNo + 1) % 100;
            //}
            //catch (Exception)
            //{

            //}

            //for (; i < 11; i++)
            //{
            //    using (var driver = new ChromeDriver())
            //    {
            //        driver.Manage().Window.Maximize();
            //        Thread.Sleep(3000);
            //        driver.Navigate().GoToUrl("https://www.nba.com/schedule");
            //        Thread.Sleep(3000);
            //        driver.FindElementById("onetrust-accept-btn-handler").Click();
            //        Thread.Sleep(3000);
            //        for (; j < 100; j++)
            //        {
            //            if (i * 100 + j > 1077)
            //                continue;
            //            try
            //            {
            //                driver.Navigate().GoToUrl("https://www.nba.com/game/002200" +
            //                                          (i * 100 + j).ToString("0000" + "/box-score"));
            //                Thread.Sleep(3000);
            //                List<PlayerStats20_21> stats= _statScraper.PlayerStatScraper20_21(driver, i * 100 + j);
            //                foreach (var VARIABLE in stats)
            //                {
            //                    _db.PlayerStats20_21.Add(VARIABLE);
            //                }
            //                _uw.Commit();
            //            }
            //            catch (Exception)
            //            {
            //                j--;
            //            }
            //        }
            //    }
            //}

            ////PlayerStatScraper19_20

            //int k = 0;
            //int l = 1;
            //try
            //{
            //    int gameNo = _db.PlayerStats19_20.OrderByDescending(x => x.GameNo).First().GameNo;
            //    k = (gameNo + 1) / 100;
            //    l = (gameNo + 1) % 100;
            //}
            //catch (Exception)
            //{

            //}

            //for (; k < 11; k++)
            //{
            //    using (var driver = new ChromeDriver())
            //    {
            //        driver.Manage().Window.Maximize();
            //        Thread.Sleep(3000);
            //        driver.Navigate().GoToUrl("https://www.nba.com/schedule");
            //        Thread.Sleep(3000);
            //        driver.FindElementById("onetrust-accept-btn-handler").Click();
            //        Thread.Sleep(3000);
            //        for (; l < 100; l++)
            //        {
            //            if (k * 100 + l > 1077)
            //                continue;
            //            try
            //            {
            //                driver.Navigate().GoToUrl("https://www.nba.com/game/002190" +
            //                                          (k * 100 + l).ToString("0000" + "/box-score"));
            //                driver.Navigate().GoToUrl("https://www.nba.com/game/002190" +
            //                                          (k * 100 + l).ToString("0000" + "/box-score?range=0-28800"));
            //                Thread.Sleep(3000);
            //                List<PlayerStats19_20> stats = _statScraper.PlayerStatScraper19_20(driver, k * 100 + l);
            //                foreach (var VARIABLE in stats)
            //                {
            //                    _db.PlayerStats19_20.Add(VARIABLE);
            //                }
            //                _uw.Commit();
            //            }
            //            catch (Exception)
            //            {
            //                l--;
            //            }
            //        }
            //    }
            //}



            //PlayerstatQuartersScaper

            ////FullSeasonQuarters20_21 Scraper

            //int i = 0;
            //int j = 1;
            //try
            //{
            //    int lastgame = _db.FullSeasonQuarters20_21.OrderByDescending(x => x.GameNo).First().GameNo;
            //    i = (lastgame + 1) / 25;
            //    j = (lastgame + 1) % 25;
            //}
            //catch (Exception)
            //{

            //}
            //for (; i < 44; i++)
            //{
            //    using (var driver = new ChromeDriver())
            //    {
            //        driver.Manage().Window.Maximize();
            //        Thread.Sleep(3000);
            //        driver.Navigate().GoToUrl("https://www.nba.com/schedule");
            //        Thread.Sleep(3000);
            //        driver.FindElementById("onetrust-accept-btn-handler").Click();
            //        Thread.Sleep(3000);
            //        for (; j < 25; j++)
            //        {
            //            if (i * 25 + j > 1077)
            //                continue;
            //            int TotalQuarters = 0;
            //            for (int k = 0; k < 1; k++)
            //            {
            //                try
            //                {
            //                    driver.Navigate().GoToUrl("https://www.nba.com/game/002200" +
            //                                              (i * 25 + j).ToString("0000") + "/box-score");
            //                    string period = driver.FindElementByXPath(
            //                        "/html/body/div[1]/div[2]/div[4]/section[1]/div/form/div[2]/label/div/select").Text;
            //                    switch (period.Length)
            //                    {
            //                        case 47:
            //                            TotalQuarters = 4;
            //                            break;
            //                        case 52:
            //                            TotalQuarters = 5;
            //                            break;
            //                        default:
            //                            TotalQuarters = (period.Length - 55) / 5 + 4;
            //                            break;
            //                    }
            //                }
            //                catch (Exception)
            //                {
            //                    Thread.Sleep(3000);
            //                    k--;
            //                }
            //            }
            //            for (int k = 1; k < TotalQuarters + 1; k++)
            //            {
            //                try
            //                {
            //                    driver.Navigate().GoToUrl("https://www.nba.com/game/002200" +
            //                                              (i * 25 + j).ToString("0000") + GetTimeSpan(k));
            //                    Thread.Sleep(3000);
            //                    FullSeasonQuarters20_21 quarter =
            //                        _statScraper.QuarterScraper20_21(driver, i * 25 + j, k);
            //                    _fullSeasonQuartersRepository.AddQuarter20_21(quarter);
            //                }
            //                catch (Exception)
            //                {
            //                    Thread.Sleep(3000);
            //                    k--;
            //                }
            //            }
            //        }
            //    }
            //    j = 0;
            //}

            ////PlayerstatQuarters20_21 Scraper

            //int i = 0;
            //int j = 1;
            //try
            //{
            //    int lastgame = _db.PlayerStatsQuarter20_21.OrderByDescending(x => x.GameNo).First().GameNo;
            //    i = (lastgame + 1) / 25;
            //    j = (lastgame + 1) % 25;
            //}
            //catch (Exception)
            //{

            //}
            //for (; i < 44; i++)
            //{
            //    using (var driver = new ChromeDriver())
            //    {
            //        driver.Manage().Window.Maximize();
            //        Thread.Sleep(3000);
            //        driver.Navigate().GoToUrl("https://www.nba.com/schedule");
            //        Thread.Sleep(3000);
            //        driver.FindElementById("onetrust-accept-btn-handler").Click();
            //        Thread.Sleep(3000);
            //        for (; j < 25; j++)
            //        {
            //            if (i * 25 + j > 1077)
            //                continue;
            //            int TotalQuarters = 0;
            //            for (int k = 0; k < 1; k++)
            //            {
            //                try
            //                {
            //                    driver.Navigate().GoToUrl("https://www.nba.com/game/002200" +
            //                                              (i * 25 + j).ToString("0000") + "/box-score");
            //                    string period = driver.FindElementByXPath(
            //                        "/html/body/div[1]/div[2]/div[4]/section[1]/div/form/div[2]/label/div/select").Text;
            //                    switch (period.Length)
            //                    {
            //                        case 47:
            //                            TotalQuarters = 4;
            //                            break;
            //                        case 52:
            //                            TotalQuarters = 5;
            //                            break;
            //                        default:
            //                            TotalQuarters = (period.Length - 55) / 5 + 4;
            //                            break;
            //                    }
            //                }
            //                catch (Exception)
            //                {
            //                    Thread.Sleep(3000);
            //                    k--;
            //                }
            //            }
            //            for (int k = 1; k < TotalQuarters + 1; k++)
            //            {
            //                try
            //                {
            //                    driver.Navigate().GoToUrl("https://www.nba.com/game/002200" +
            //                                              (i * 25 + j).ToString("0000") + GetTimeSpan(k));
            //                    //Thread.Sleep(3000);
            //                    var quarterstats =
            //                        _statScraper.PlayerStatQuarterScraper20_21(driver, i * 25 + j, k);
            //                    foreach (var VARIABLE in quarterstats)
            //                    {
            //                        _playerStatQuarterRepository.AddPlayerStatsQuarter20_21(VARIABLE);
            //                    }
            //                }
            //                catch (Exception)
            //                {
            //                    Thread.Sleep(3000);
            //                    k--;
            //                }
            //            }
            //        }
            //    }
            //    j = 0;
            //}

            ////PlayerstatQuarters19_20 Scraper

            //int l = 0;
            //int m = 1;
            //try
            //{
            //    int lastgame = _db.PlayerStatsQuarter19_20.OrderByDescending(x => x.GameNo).First().GameNo;
            //    l = (lastgame + 1) / 25;
            //    m = (lastgame + 1) % 25;
            //}
            //catch (Exception)
            //{

            //}
            //for (; l < 39; l++)
            //{
            //    using (var driver = new ChromeDriver())
            //    {
            //        driver.Manage().Window.Maximize();
            //        Thread.Sleep(3000);
            //        driver.Navigate().GoToUrl("https://www.nba.com/schedule");
            //        Thread.Sleep(3000);
            //        driver.FindElementById("onetrust-accept-btn-handler").Click();
            //        Thread.Sleep(3000);
            //        for (; m < 25; m++)
            //        {
            //            if (l * 25 + m > 973)
            //                continue;
            //            switch (l * 25 + m)
            //            {
            //                case 0:
            //                case 707:
            //                case 972:
            //                    continue;
            //            }
            //            int TotalQuarters = 0;
            //            for (int k = 0; k < 1; k++)
            //            {
            //                try
            //                {
            //                    if (m == 0)
            //                    {
            //                        Thread.Sleep(3000);
            //                    }
            //                    driver.Navigate().GoToUrl("https://www.nba.com/game/002190" +
            //                                              (l * 25 + m).ToString("0000") + "/box-score");
            //                    string period = driver.FindElementByXPath(
            //                        "/html/body/div[1]/div[2]/div[4]/section[1]/div/form/div[2]/label/div/select").Text;
            //                    switch (period.Length)
            //                    {
            //                        case 47:
            //                            TotalQuarters = 4;
            //                            break;
            //                        case 52:
            //                            TotalQuarters = 5;
            //                            break;
            //                        default:
            //                            TotalQuarters = (period.Length - 55) / 5 + 4;
            //                            break;
            //                    }
            //                }
            //                catch (Exception)
            //                {
            //                    Thread.Sleep(3000);
            //                    k--;
            //                }
            //            }
            //            for (int n = 1; n < TotalQuarters + 1; n++)
            //            {
            //                try
            //                {
            //                    if (m == 0)
            //                    {
            //                        Thread.Sleep(3000);
            //                    }
            //                    driver.Navigate().GoToUrl("https://www.nba.com/game/002190" +
            //                                              (l * 25 + m).ToString("0000") + GetTimeSpan(n));
            //                    //Thread.Sleep(3000);
            //                    var quarterstats =
            //                        _statScraper.PlayerStatQuarterScraper19_20(driver, l * 25 + m, n);
            //                    foreach (var VARIABLE in quarterstats)
            //                    {
            //                        _playerStatQuarterRepository.AddPlayerStatsQuarter19_20(VARIABLE);
            //                    }
            //                }
            //                catch (Exception)
            //                {
            //                    Thread.Sleep(3000);
            //                    n--;
            //                }
            //            }
            //        }
            //    }
            //    m = 0;
            //}

            //var players = _db.Players.ToList();
            //foreach (var player in players)
            //{
            //    var playerstats = _db.PlayerStatsQuarter19_20.Where(x => x.Player == player).ToList();
            //    if (playerstats.Count == 0)
            //        continue;
            //    List<int> games = new List<int>();
            //    foreach (var stat in playerstats)
            //    {
            //        games.Add(stat.GameNo);
            //    }
            //    games = games.Distinct().ToList();
            //    foreach (int game in games)
            //    {
            //        List<PlayerStatsQuarter19_20> quarters = playerstats.Where(x => x.GameNo == game).ToList();
            //        PlayerStats19_20 stats = new PlayerStats19_20();
            //        stats.GameNo = game;
            //        stats.Player = player;
            //        foreach (var quarter in quarters)
            //        {
            //            stats.Team = quarter.Team;
            //            stats.Minutes += quarter.Minutes;
            //            stats.Seconds += quarter.Seconds;
            //            stats.PlayerPoints += quarter.PlayerPoints;
            //            stats.PlayerFGA += quarter.PlayerFGA;
            //            stats.PlayerFGM += quarter.PlayerFGM;
            //            stats.Player3PA += quarter.Player3PA;
            //            stats.Player3PM += quarter.Player3PM;
            //            stats.PlayerFTA += quarter.PlayerFTA;
            //            stats.PlayerFTM += quarter.PlayerFTM;
            //            stats.PlayerAssists += quarter.PlayerAssists;
            //            stats.PlayerDefensiveRebounds += quarter.PlayerDefensiveRebounds;
            //            stats.PlayerOffensiveRebounds += quarter.PlayerOffensiveRebounds;
            //            stats.PlayerBlocks += quarter.PlayerBlocks;
            //            stats.PlayerSteals += quarter.PlayerSteals;
            //            stats.PlayerTurnovers += quarter.PlayerTurnovers;
            //            stats.PlayerFouls += quarter.PlayerFouls;
            //            stats.PlayerPlusMinus += quarter.PlayerPlusMinus;
            //        }

            //        stats.Minutes += stats.Seconds / 60;
            //        stats.Seconds = stats.Seconds % 60;
            //        _db.PlayerStats19_20.Add(stats);
            //    }
            //}





            //////// playerstatsquarter team column fix method

            //////var players = _playerRepository.GetAllPlayers().Data;
            //////var playerStatsQuarter20 = _playerStatQuarterRepository.GetAllPlayerStatsQuarter20_21().Data;
            //////var gametimes20 = _gameTimesRepository.GetFullSeason20_21().Data;
            //////var fullSeason = _fullSeasonRepository.GetFullSeason20_21().Data.OrderBy(x=>x.GameDate.GameDate);
            //////foreach (var player in players)
            //////{
            //////    var stats = playerStatsQuarter20.Where(x => x.Player == player).ToList();
            //////    if (stats.Count == 0)
            //////        continue;
            //////    List<MyClass> teams = new List<MyClass>();
            //////    MyClass AtlantaHawks = new MyClass()
            //////    {
            //////        team = Team.AtlantaHawks,
            //////        count = 0
            //////    };
            //////    MyClass BostonCeltics = new MyClass()
            //////    {
            //////        team = Team.BostonCeltics,
            //////        count = 0
            //////    };
            //////    MyClass BrooklynNets = new MyClass()
            //////    {
            //////        team = Team.BrooklynNets,
            //////        count = 0
            //////    };
            //////    MyClass CharlotteHornets = new MyClass()
            //////    {
            //////        team = Team.CharlotteHornets,
            //////        count = 0
            //////    };
            //////    MyClass ChicagoBulls = new MyClass()
            //////    {
            //////        team = Team.ChicagoBulls,
            //////        count = 0
            //////    };
            //////    MyClass ClevelandCavaliers = new MyClass()
            //////    {
            //////        team = Team.ClevelandCavaliers,
            //////        count = 0
            //////    };
            //////    MyClass DallasMavericks = new MyClass()
            //////    {
            //////        team = Team.DallasMavericks,
            //////        count = 0
            //////    };
            //////    MyClass DenverNuggets = new MyClass()
            //////    {
            //////        team = Team.DenverNuggets,
            //////        count = 0
            //////    };
            //////    MyClass DetroitPistons = new MyClass()
            //////    {
            //////        team = Team.DetroitPistons,
            //////        count = 0
            //////    };
            //////    MyClass GoldenStateWarriors = new MyClass()
            //////    {
            //////        team = Team.GoldenStateWarriors,
            //////        count = 0
            //////    };
            //////    MyClass HoustonRockets = new MyClass()
            //////    {
            //////        team = Team.HoustonRockets,
            //////        count = 0
            //////    };
            //////    MyClass IndianaPacers = new MyClass()
            //////    {
            //////        team = Team.IndianaPacers,
            //////        count = 0
            //////    };
            //////    MyClass LosAngelesClippers = new MyClass()
            //////    {
            //////        team = Team.LosAngelesClippers,
            //////        count = 0
            //////    };
            //////    MyClass LosAngelesLakers = new MyClass()
            //////    {
            //////        team = Team.LosAngelesLakers,
            //////        count = 0
            //////    };
            //////    MyClass MemphisGrizzlies = new MyClass()
            //////    {
            //////        team = Team.MemphisGrizzlies,
            //////        count = 0
            //////    };
            //////    MyClass MiamiHeat = new MyClass()
            //////    {
            //////        team = Team.MiamiHeat,
            //////        count = 0
            //////    };
            //////    MyClass MilwaukeeBucks = new MyClass()
            //////    {
            //////        team = Team.MilwaukeeBucks,
            //////        count = 0
            //////    };
            //////    MyClass MinnesotaTimberwolves = new MyClass()
            //////    {
            //////        team = Team.MinnesotaTimberwolves,
            //////        count = 0
            //////    };
            //////    MyClass NewOrleansPelicans = new MyClass()
            //////    {
            //////        team = Team.NewOrleansPelicans,
            //////        count = 0
            //////    };
            //////    MyClass NewYorkKnicks = new MyClass()
            //////    {
            //////        team = Team.NewYorkKnicks,
            //////        count = 0
            //////    };
            //////    MyClass OklahomaCityThunder = new MyClass()
            //////    {
            //////        team = Team.OklahomaCityThunder,
            //////        count = 0
            //////    };
            //////    MyClass OrlandoMagic = new MyClass()
            //////    {
            //////        team = Team.OrlandoMagic,
            //////        count = 0
            //////    };
            //////    MyClass Philadelphia76ers = new MyClass()
            //////    {
            //////        team = Team.Philadelphia76ers,
            //////        count = 0
            //////    };
            //////    MyClass PhoenixSuns = new MyClass()
            //////    {
            //////        team = Team.PhoenixSuns,
            //////        count = 0
            //////    };
            //////    MyClass PortlandTrailBlazers = new MyClass()
            //////    {
            //////        team = Team.PortlandTrailBlazers,
            //////        count = 0
            //////    };
            //////    MyClass SacramentoKings = new MyClass()
            //////    {
            //////        team = Team.SacramentoKings,
            //////        count = 0
            //////    };
            //////    MyClass SanAntonioSpurs = new MyClass()
            //////    {
            //////        team = Team.SanAntonioSpurs,
            //////        count = 0
            //////    };
            //////    MyClass TorontoRaptors = new MyClass()
            //////    {
            //////        team = Team.TorontoRaptors,
            //////        count = 0
            //////    };
            //////    MyClass UtahJazz = new MyClass()
            //////    {
            //////        team = Team.UtahJazz,
            //////        count = 0
            //////    };
            //////    MyClass WashingtonWizards = new MyClass()
            //////    {
            //////        team = Team.WashingtonWizards,
            //////        count = 0
            //////    };
            //////    teams.Add(AtlantaHawks);
            //////    teams.Add(BostonCeltics);
            //////    teams.Add(BrooklynNets);
            //////    teams.Add(CharlotteHornets);
            //////    teams.Add(ChicagoBulls);
            //////    teams.Add(ClevelandCavaliers);
            //////    teams.Add(DallasMavericks);
            //////    teams.Add(DenverNuggets);
            //////    teams.Add(DetroitPistons);
            //////    teams.Add(GoldenStateWarriors);
            //////    teams.Add(HoustonRockets);
            //////    teams.Add(IndianaPacers);
            //////    teams.Add(LosAngelesClippers);
            //////    teams.Add(LosAngelesLakers);
            //////    teams.Add(MemphisGrizzlies);
            //////    teams.Add(MiamiHeat);
            //////    teams.Add(MilwaukeeBucks);
            //////    teams.Add(MinnesotaTimberwolves);
            //////    teams.Add(NewOrleansPelicans);
            //////    teams.Add(NewYorkKnicks);
            //////    teams.Add(OklahomaCityThunder);
            //////    teams.Add(OrlandoMagic);
            //////    teams.Add(Philadelphia76ers);
            //////    teams.Add(PhoenixSuns);
            //////    teams.Add(PortlandTrailBlazers);
            //////    teams.Add(SacramentoKings);
            //////    teams.Add(SanAntonioSpurs);
            //////    teams.Add(TorontoRaptors);
            //////    teams.Add(UtahJazz);
            //////    teams.Add(WashingtonWizards);
            //////    List<int> games = new List<int>();
            //////    List<GameTime20_21> gametimes = new List<GameTime20_21>();
            //////    foreach (var stat in stats)
            //////    {
            //////        gametimes.Add(_gameTimesRepository.GetGameTime20_21(stat.GameNo).Data);
            //////    }

            //////    gametimes = gametimes.OrderBy(x => x.GameDate).ToList();
            //////    foreach (var gametime in gametimes)
            //////    {
            //////        games.Add(gametime.GameNo);
            //////    }
            //////    games = games.Distinct().ToList();
            //////    foreach (var game in games)
            //////    {
            //////        Team home = fullSeason.First(x => x.GameNo == game).HomeTeam;
            //////        MyClass first = teams.First(x => x.team == home);
            //////        first.count++;
            //////        Team away = fullSeason.First(x => x.GameNo == game).AwayTeam;
            //////        first = teams.First(x => x.team == away);
            //////        first.count++;
            //////    }

            //////    teams = teams.OrderByDescending(x => x.count).ToList();
            //////    if (teams[0].count == games.Count)
            //////    {
            //////        foreach (var stat in stats)
            //////        {
            //////            stat.Team = teams.First().team;
            //////        }
            //////        _uw.Commit();
            //////    }
            //////    else
            //////    {
            //////        stats = playerStatsQuarter20.Where(x => x.Player == player).ToList();
            //////        stats = stats.Where(x => x.GameNo > 658 || x.GameNo == 434 || x.GameNo == 160).ToList();
            //////        foreach (var stat in stats)
            //////        {
            //////            stat.Team = teams.First(x=>x.team==Team.ChicagoBulls).team;
            //////        }
            //////        _uw.Commit();
            //////    }
            //////}


        }

        public class MyClass
        {
            public Team team { get; set; }
            public int count { get; set; }
        }

        static string GetTimeSpan(int quarterNo)
        {
            if (quarterNo > 4)
                return "/box-score?range=" + ((quarterNo - 5) * 3000 + 28801) + "-" + ((quarterNo - 4) * 3000 + 28800);
            return "/box-score?range=" + ((quarterNo - 1) * 7200 + 1) + "-" + quarterNo * 7200;
        }
        static string GetQuarter(int quarterNo)
        {
            if (quarterNo > 4)
                return "/box-score?period=OT" + (quarterNo - 4);
            return "/box-score?period=Q" + quarterNo;
        }
    }
}
