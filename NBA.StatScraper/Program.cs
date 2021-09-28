using System.Collections.Generic;
using System.Linq;
using NBA.Services.Repositories;

namespace NBA.StatScraper
{
    using NBA.Models;
    using NBA.Models.DataContext;
    using NBA.Services.Abstraction.Repositories;
    using NBA.Services.Interfaces;
    using System;
    using OpenQA.Selenium.Chrome;
    using NBA.Services.Helpers;
    using System.Threading;
    using NBA.Models.Entities;
    using OpenQA.Selenium.Firefox;
    public class Program
    {
        static void Main(string[] args)
        {
            INBAContext _db = new NBAContext();
            IUnitOfWork _uw = new UnitOfWork(_db);
            IPlayerRepository playerRepository = new PlayerRepository(_db, _uw);
            IHelpers _helpers = new Helpers(_db);
            IStatScraper _statScraper = new StatScraper(_helpers);
            IGameTimesRepository _gameTimesRepository = new GameTimesRepository(_db, _uw);
            IPlayerRepository _playerRepository = new PlayerRepository(_db, _uw);



            ////PlayerInfoScraper
            //using (var driver = new FirefoxDriver())
            //{
            //    driver.Manage().Window.Maximize();
            //    List<Players> players = _statScraper.PlayerInfoScraper(driver, 1);
            //    _playerRepository.AddPlayerList(players);
            //}

            ////FullGame Date Scraper
            //int i = 0;
            //int k = 1;
            //for (; i < 14; i++)
            //{
            //    using (var driver = new FirefoxDriver())
            //    {
            //        driver.Manage().Window.Maximize();
            //        driver.Navigate().GoToUrl("https://www.nba.com/schedule");
            //        Thread.Sleep(1000);
            //        driver.FindElementByXPath("/html/body/div[2]/div[3]/div/div/div[2]/div/div/button").Click();
            //        for (; k < 100; k++)
            //        {
            //            if (_helpers.DoesGameExist(i * 100 + k) && i * 100 + k < 1212)
            //                continue;
            //            try
            //            {
            //                GameTime game = _statScraper.DateScraper(driver, i * 100 + k,
            //                    "https://www.nba.com/game/002210" + (i * 100 + k).ToString("0000"));
            //                string teams = driver
            //                    .FindElementByXPath("/html/body/div[1]/div[2]/section/div[1]/div[5]/div/div[2]/h1")
            //                    .Text;
            //                string away = teams.Substring(0, teams.IndexOf('@') - 1).ToUpper().Replace('İ', 'I');
            //                string home = teams.Substring(teams.IndexOf('@') + 2).ToUpper().Replace('İ', 'I');
            //                game.AwayTeam = _helpers.GetTeamEnumByTeamMascotName(away);
            //                game.HomeTeam = _helpers.GetTeamEnumByTeamMascotName(home);
            //                _gameTimesRepository.AddGameTime(game);
            //            }
            //            catch (Exception)
            //            {
            //                k--;
            //            }
            //        }
            //    }
            //    k = 0;
            //}

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
            //    using (var driver = new FirefoxDriver())
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
            //    using (var driver = new FirefoxDriver())
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


        }
    }
}
