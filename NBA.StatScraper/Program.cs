namespace NBA.StatScraper
{
    using NBA.Models;
    using NBA.Models.DataContext;
    using NBA.Services.Abstraction.Interfaces;
    using NBA.Services.Abstraction.Repositories;
    using System;
    public class Program { 
        static void Main(string[] args)
        {
            INBAContext db= new NBAContext();
            IUnitOfWork uw = new UnitOfWork(db);
            IPlayerRepository playerRepository = new PlayerRepository(db, uw);
        }
    }
}
