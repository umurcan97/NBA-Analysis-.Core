namespace NBA.Services.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using NBA.Models;
    using NBA.Models.DataContext;
    using NBA.Models.Entities;
    public class Helpers : IHelpers
    {
        private readonly INBAContext _db;

        public Helpers(INBAContext db)
        {
            this._db = db;
        }
        public Team GetTeamEnumByTeamName(string TeamName)
        {
            switch (TeamName)
            {
                case "ATLANTAHAWKS": return Team.AtlantaHawks;
                case "BOSTONCELTICS": return Team.BostonCeltics;
                case "BROOKLYNNETS": return Team.BrooklynNets;
                case "CHARLOTTEHORNETS": return Team.CharlotteHornets;
                case "CHICAGOBULLS": return Team.ChicagoBulls;
                case "CLEVELANDCAVALIERS": return Team.ClevelandCavaliers;
                case "DALLASMAVERICKS": return Team.DallasMavericks;
                case "DENVERNUGGETS": return Team.DenverNuggets;
                case "DETROITPISTONS": return Team.DetroitPistons;
                case "GOLDENSTATEWARRIORS": return Team.GoldenStateWarriors;
                case "HOUSTONROCKETS": return Team.HoustonRockets;
                case "INDIANAPACERS": return Team.IndianaPacers;
                case "LACLIPPERS": return Team.LosAngelesClippers;
                case "LOSANGELESLAKERS": return Team.LosAngelesLakers;
                case "MEMPHISGRIZZLIES": return Team.MemphisGrizzlies;
                case "MIAMIHEAT": return Team.MiamiHeat;
                case "MILWAUKEEBUCKS": return Team.MilwaukeeBucks;
                case "MINNESOTATIMBERWOLVES": return Team.MinnesotaTimberwolves;
                case "NEWORLEANSPELICANS": return Team.NewOrleansPelicans;
                case "NEWYORKKNICKS": return Team.NewYorkKnicks;
                case "OKLAHOMACITYTHUNDER": return Team.OklahomaCityThunder;
                case "ORLANDOMAGIC": return Team.OrlandoMagic;
                case "PHILADELPHIA76ERS": return Team.Philadelphia76ers;
                case "PHOENIXSUNS": return Team.PhoenixSuns;
                case "PORTLANDTRAILBLAZERS": return Team.PortlandTrailBlazers;
                case "SACRAMENTOKINGS": return Team.SacramentoKings;
                case "SANANTONIOSPURS": return Team.SanAntonioSpurs;
                case "TORONTORAPTORS": return Team.TorontoRaptors;
                case "UTAHJAZZ": return Team.UtahJazz;
                case "WASHINGTONWIZARDS": return Team.WashingtonWizards;
                default: return Team.Error;
            }
        }
        public Team GetTeamEnumByTeamId(int TeamID)
        {
            switch (TeamID)
            {
                case 1: return Team.AtlantaHawks;
                case 2: return Team.BostonCeltics;
                case 3: return Team.BrooklynNets;
                case 4: return Team.CharlotteHornets;
                case 5: return Team.ChicagoBulls;
                case 6: return Team.ClevelandCavaliers;
                case 7: return Team.DallasMavericks;
                case 8: return Team.DenverNuggets;
                case 9: return Team.DetroitPistons;
                case 10: return Team.GoldenStateWarriors;
                case 11: return Team.HoustonRockets;
                case 12: return Team.IndianaPacers;
                case 13: return Team.LosAngelesClippers;
                case 14: return Team.LosAngelesLakers;
                case 15: return Team.MemphisGrizzlies;
                case 16: return Team.MiamiHeat;
                case 17: return Team.MilwaukeeBucks;
                case 18: return Team.MinnesotaTimberwolves;
                case 19: return Team.NewOrleansPelicans;
                case 20: return Team.NewYorkKnicks;
                case 21: return Team.OklahomaCityThunder;
                case 22: return Team.OrlandoMagic;
                case 23: return Team.Philadelphia76ers;
                case 24: return Team.PhoenixSuns;
                case 25: return Team.PortlandTrailBlazers;
                case 26: return Team.SacramentoKings;
                case 27: return Team.SanAntonioSpurs;
                case 28: return Team.TorontoRaptors;
                case 29: return Team.UtahJazz;
                case 30: return Team.WashingtonWizards;
                default: return Team.Error;
            }
        }
        public Team GetTeamEnumByTeamShortName(string TeamShortName)
        {
            switch (TeamShortName)
            {
                case "ATL": return Team.AtlantaHawks;
                case "BOS": return Team.BostonCeltics;
                case "BKN": return Team.BrooklynNets;
                case "CHA": return Team.CharlotteHornets;
                case "CHI": return Team.ChicagoBulls;
                case "CLE": return Team.ClevelandCavaliers;
                case "DAL": return Team.DallasMavericks;
                case "DEN": return Team.DenverNuggets;
                case "DET": return Team.DetroitPistons;
                case "GSW": return Team.GoldenStateWarriors;
                case "HOU": return Team.HoustonRockets;
                case "IND": return Team.IndianaPacers;
                case "LAC": return Team.LosAngelesClippers;
                case "LAL": return Team.LosAngelesLakers;
                case "MEM": return Team.MemphisGrizzlies;
                case "MIA": return Team.MiamiHeat;
                case "MIL": return Team.MilwaukeeBucks;
                case "MIN": return Team.MinnesotaTimberwolves;
                case "NOP": return Team.NewOrleansPelicans;
                case "NYK": return Team.NewYorkKnicks;
                case "OKC": return Team.OklahomaCityThunder;
                case "ORL": return Team.OrlandoMagic;
                case "PHI": return Team.Philadelphia76ers;
                case "PHX": return Team.PhoenixSuns;
                case "POR": return Team.PortlandTrailBlazers;
                case "SAC": return Team.SacramentoKings;
                case "SAS": return Team.SanAntonioSpurs;
                case "TOR": return Team.TorontoRaptors;
                case "UTA": return Team.UtahJazz;
                case "WAS": return Team.WashingtonWizards;
                default: return Team.Error;
            }
        }
        public Team GetTeamEnumByTeamMascotName(string TeamMascotName)
        {
            switch (TeamMascotName)
            {
                case "HAWKS": return Team.AtlantaHawks;
                case "CELTICS": return Team.BostonCeltics;
                case "NETS": return Team.BrooklynNets;
                case "HORNETS": return Team.CharlotteHornets;
                case "BULLS": return Team.ChicagoBulls;
                case "CAVALIERS": return Team.ClevelandCavaliers;
                case "MAVERICKS": return Team.DallasMavericks;
                case "NUGGETS": return Team.DenverNuggets;
                case "PISTONS": return Team.DetroitPistons;
                case "WARRIORS": return Team.GoldenStateWarriors;
                case "ROCKETS": return Team.HoustonRockets;
                case "PACERS": return Team.IndianaPacers;
                case "CLIPPERS": return Team.LosAngelesClippers;
                case "LAKERS": return Team.LosAngelesLakers;
                case "GRIZZLIES": return Team.MemphisGrizzlies;
                case "HEAT": return Team.MiamiHeat;
                case "BUCKS": return Team.MilwaukeeBucks;
                case "TIMBERWOLVES": return Team.MinnesotaTimberwolves;
                case "PELICANS": return Team.NewOrleansPelicans;
                case "KNICKS": return Team.NewYorkKnicks;
                case "THUNDER": return Team.OklahomaCityThunder;
                case "MAGIC": return Team.OrlandoMagic;
                case "76ERS": return Team.Philadelphia76ers;
                case "SUNS": return Team.PhoenixSuns;
                case "TRAIL BLAZERS": return Team.PortlandTrailBlazers;
                case "KINGS": return Team.SacramentoKings;
                case "SPURS": return Team.SanAntonioSpurs;
                case "RAPTORS": return Team.TorontoRaptors;
                case "JAZZ": return Team.UtahJazz;
                case "WIZARDS": return Team.WashingtonWizards;
                default: return Team.Error;
            }
        }
        public Players GetPlayerWithName(string name)
        {
            return this._db.Players.First(x => x.Name == name);
        }
        public bool DoesGameExist(int GameNo)
        {
            bool GameExists = true;
            FullSeason game = _db.FullSeason.FirstOrDefault(x => x.GameNo == GameNo);
            if (game == null)
                GameExists = false;
            return GameExists;
        }
        public bool DoesQuarterExist(int GameNo, int QuarterNo)
        {
            bool QuarterExists = true;
            FullSeasonQuarters game = _db.FullSeasonQuarters.FirstOrDefault(x => x.GameNo == GameNo && x.QuarterNo == QuarterNo);
            if (game == null)
                QuarterExists = false;
            return QuarterExists;
        }
        public bool DoesQuarterExist(int GameNo)
        {
            bool QuarterExists = true;
            FullSeasonQuarters game = _db.FullSeasonQuarters.FirstOrDefault(x => x.GameNo == GameNo);
            if (game == null)
                QuarterExists = false;
            return QuarterExists;
        }
        public bool DoesPlayerStatGameExist(int GameNo)
        {
            List<PlayerStats> stats = this._db.PlayerStats.Where(x => x.GameNo == GameNo).ToList();
            return stats.Count != 0;
        }
        public bool DoesGameExist20_21(int GameNo)
        {
            bool GameExists = true;
            FullSeason20_21 game = _db.FullSeason20_21.FirstOrDefault(x => x.GameNo == GameNo);
            if (game == null)
                GameExists = false;
            return GameExists;
        }
        public bool DoesQuarterExist20_21(int GameNo, int QuarterNo)
        {
            bool QuarterExists = true;
            FullSeasonQuarters20_21 game = _db.FullSeasonQuarters20_21.FirstOrDefault(x => x.GameNo == GameNo && x.QuarterNo == QuarterNo);
            if (game == null)
                QuarterExists = false;
            return QuarterExists;
        }
        public bool DoesQuarterExist20_21(int GameNo)
        {
            bool QuarterExists = true;
            FullSeasonQuarters20_21 game = _db.FullSeasonQuarters20_21.FirstOrDefault(x => x.GameNo == GameNo);
            if (game == null)
                QuarterExists = false;
            return QuarterExists;
        }
        public bool DoesPlayerStatGameExist20_21(int GameNo)
        {
            List<PlayerStats20_21> stats = this._db.PlayerStats20_21.Where(x => x.GameNo == GameNo).ToList();
            return stats.Count != 0;
        }
        public bool DoesGameExist19_20(int GameNo)
        {
            bool GameExists = true;
            FullSeason19_20 game = _db.FullSeason19_20.FirstOrDefault(x => x.GameNo == GameNo);
            if (game == null)
                GameExists = false;
            return GameExists;
        }
        public bool DoesQuarterExist19_20(int GameNo, int QuarterNo)
        {
            bool QuarterExists = true;
            FullSeasonQuarters19_20 game = _db.FullSeasonQuarters19_20.FirstOrDefault(x => x.GameNo == GameNo && x.QuarterNo == QuarterNo);
            if (game == null)
                QuarterExists = false;
            return QuarterExists;
        }
        public bool DoesQuarterExist19_20(int GameNo)
        {
            bool QuarterExists = true;
            FullSeasonQuarters19_20 game = _db.FullSeasonQuarters19_20.FirstOrDefault(x => x.GameNo == GameNo);
            if (game == null)
                QuarterExists = false;
            return QuarterExists;
        }
        public bool DoesPlayerStatGameExist19_20(int GameNo)
        {
            List<PlayerStats19_20> stats = this._db.PlayerStats19_20.Where(x => x.GameNo == GameNo).ToList();
            return stats.Count != 0;
        }
        public DateTime DateTimeConverter(string date)
        {
            int firstComma = date.IndexOf(',');
            date = date.Substring(firstComma + 2);
            int Month = date.IndexOf(' ');
            string month = date.Substring(0, Month);
            date = date.Substring(Month + 1);
            switch (month)
            {
                case "JANUARY":
                    Month = 1;
                    break;
                case "FEBRUARY":
                    Month = 2;
                    break;
                case "MARCH":
                    Month = 3;
                    break;
                case "APRIL":
                    Month = 4;
                    break;
                case "MAY":
                    Month = 5;
                    break;
                case "JUNE":
                    Month = 6;
                    break;
                case "JULY":
                    Month = 7;
                    break;
                case "AUGUST":
                    Month = 8;
                    break;
                case "SEPTEMBER":
                    Month = 9;
                    break;
                case "OCTOBER":
                    Month = 10;
                    break;
                case "NOVEMBER":
                    Month = 11;
                    break;
                case "DECEMBER":
                    Month = 12;
                    break;
            }

            int Day = 1;
            if (date[1] == '0' || date[1] == '1' || date[1] == '2' || date[1] == '3' || date[1] == '4' ||
                date[1] == '5' || date[1] == '6' || date[1] == '7' || date[1] == '8' || date[1] == '9')
                Day = 2;
            string day = date.Substring(0, Day);
            date = date.Substring(Day + 4);
            Day = int.Parse(day);
            int Year = int.Parse(date.Substring(0, 4));
            string hour = date.Substring(5);
            if (hour[1] == ':')
                hour = hour.Insert(0, "0");
            int Hour = int.Parse(hour.Substring(0, 2));
            int Minute = int.Parse(hour.Substring(3, 2));
            if (hour.Substring(6, 2) == "PM")
            {
                Hour += 12;
                if (Hour == 24)
                {
                    Hour = 12;
                }
            }

            return DateTime.Parse(Year + "-" + Month.ToString("00") + "-" + Day.ToString("00") + " " + Hour + ":" + Minute + ":" + "00");
        }
    }
}
