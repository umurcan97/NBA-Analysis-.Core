namespace NBA.Services.Calculations
{
    using NBA.Models;
    using NBA.Models.Entities;
    using NBA.Services.Abstraction;
    using NBA.Services.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Simulator : ISimulator
    {
        private readonly IMathOperations _mathOperations;
        private readonly IGameTimesRepository _gameTimesRepository;
        private readonly IFullSeasonRepository _fullSeasonRepository;
        private readonly IFullSeasonQuartersRepository _fullSeasonQuartersRepository;

        public Simulator(IMathOperations mathOperations,
            IGameTimesRepository gameTimesRepository,
            IFullSeasonRepository fullSeasonRepository,
            IFullSeasonQuartersRepository fullSeasonQuartersRepository)
        {
            this._mathOperations = mathOperations;
            this._gameTimesRepository = gameTimesRepository;
            this._fullSeasonRepository = fullSeasonRepository;
            this._fullSeasonQuartersRepository = fullSeasonQuartersRepository;
        }
        public int DeviationApplication(double stat, double statdeviation, double stataverage)
        {
            if (stat < stataverage && stataverage - stat >= statdeviation)
                return Convert.ToInt16(Math.Round(stat) == Math.Floor(stat) ? Math.Floor(stat) - 1 : Math.Floor(stat));
            if (stat < stataverage && stataverage - stat < statdeviation)
                return Convert.ToInt16(Math.Round(stat) > stataverage ? Math.Floor(stat) : Math.Round(stat));
            if (stat > stataverage && stat - stataverage < statdeviation)
                return Convert.ToInt16(Math.Round(stat) < stataverage ? Math.Ceiling(stat) : Math.Round(stat));
            return Convert.ToInt16(Math.Round(stat) == Math.Ceiling(stat) ? Math.Ceiling(stat) + 1 : Math.Ceiling(stat));
        }

        public double PaceofPlayCalculator(QuarterModel Stats)
        {
            return Stats.HomeFGA + Stats.AwayFGA + Stats.HomeFTA / 2.5 + Stats.AwayFTA / 2.5 + Stats.HomeTurnovers +
                Stats.AwayTurnovers - Stats.HomeOffensiveRebounds - Stats.AwayOffensiveRebounds;
        }

        public QuarterPredictions QuarterCalculator(QuarterModel Stats, QuarterModel HomeAverages, QuarterModel AwayAverages, QuarterModel HomeDeviation, QuarterModel AwayDeviation)
        {
            double paceofplay = PaceofPlayCalculator(Stats);
            double home2PP = (Stats.HomeFGM - Stats.Home3PM) / (Stats.HomeFGA - Stats.Home3PA);
            double away2PP = (Stats.AwayFGM - Stats.Away3PM) / (Stats.AwayFGA - Stats.Away3PA);
            double home3PP = Stats.Home3PM / Stats.Home3PA;
            double away3PP = Stats.Away3PM / Stats.Away3PA;
            double homeFTP = Stats.HomeFTM / Stats.HomeFTA;
            double awayFTP = Stats.AwayFTM / Stats.AwayFTA;
            int Home3PA = DeviationApplication(Stats.Home3PA, HomeDeviation.Home3PA, HomeAverages.Home3PA);
            int HomeTurnovers = DeviationApplication(Stats.HomeTurnovers, HomeDeviation.HomeTurnovers, HomeAverages.HomeTurnovers);
            int HomeOffensiveRebounds = DeviationApplication(Stats.HomeOffensiveRebounds, HomeDeviation.HomeOffensiveRebounds, HomeAverages.HomeOffensiveRebounds);
            int HomeFTA = DeviationApplication(Stats.HomeFTA, HomeDeviation.HomeFTA, HomeAverages.HomeFTA);
            int Home2PA = Convert.ToInt16(Math.Round(paceofplay / 2 - HomeFTA / 2.5 - HomeTurnovers - Home3PA + HomeOffensiveRebounds));
            int Home2PM = DeviationApplication(Home2PA * home2PP, HomeDeviation.HomeFGM - HomeDeviation.Home3PM, HomeAverages.HomeFGM - HomeAverages.Home3PM);
            int Away3PA = DeviationApplication(Stats.Away3PA, AwayDeviation.Away3PA, AwayAverages.Away3PA);
            int AwayTurnovers = DeviationApplication(Stats.AwayTurnovers, AwayDeviation.AwayTurnovers, AwayAverages.AwayTurnovers);
            int AwayOffensiveRebounds = DeviationApplication(Stats.AwayOffensiveRebounds, AwayDeviation.AwayOffensiveRebounds, AwayAverages.AwayOffensiveRebounds);
            int AwayFTA = DeviationApplication(Stats.AwayFTA, AwayDeviation.AwayFTA, AwayAverages.AwayFTA);
            int Away2PA = Convert.ToInt16(Math.Round(paceofplay / 2 - AwayFTA / 2.5 - AwayTurnovers - Away3PA + AwayOffensiveRebounds));
            int Away2PM = DeviationApplication(Away2PA * away2PP, AwayDeviation.AwayFGM - AwayDeviation.Away3PM, AwayAverages.AwayFGM - AwayAverages.Away3PM);
            QuarterPredictions quarter = new QuarterPredictions
            {
                HomeFGA = Home3PA + Home2PA,
                Home3PA = Home3PA,
                Home3PM = DeviationApplication(Home3PA * home3PP, HomeDeviation.Home3PA, HomeAverages.Home3PM),
                HomeFTA = HomeFTA,
                HomeFTM = DeviationApplication(HomeFTA * homeFTP, HomeDeviation.HomeFTM, HomeAverages.HomeFTM),
                HomeAssists = DeviationApplication(Stats.HomeAssists, HomeDeviation.HomeAssists, HomeAverages.HomeAssists),
                HomeTurnovers = HomeTurnovers,
                HomeOffensiveRebounds = HomeOffensiveRebounds,
                HomeDefensiveRebounds = DeviationApplication(Stats.HomeDefensiveRebounds, HomeDeviation.HomeDefensiveRebounds, HomeAverages.HomeDefensiveRebounds),
                HomeBlocks = DeviationApplication(Stats.HomeBlocks, HomeDeviation.HomeBlocks, HomeAverages.HomeBlocks),
                HomeSteals = DeviationApplication(Stats.HomeSteals, HomeDeviation.HomeSteals, HomeAverages.HomeSteals),
                AwayFGA = Away3PA + Away2PA,
                Away3PA = Away3PA,
                Away3PM = DeviationApplication(Away3PA * away3PP, AwayDeviation.Away3PA, AwayAverages.Away3PM),
                AwayFTA = AwayFTA,
                AwayFTM = DeviationApplication(AwayFTA * awayFTP, AwayDeviation.AwayFTM, AwayAverages.AwayFTM),
                AwayAssists = DeviationApplication(Stats.AwayAssists, AwayDeviation.AwayAssists, AwayAverages.AwayAssists),
                AwayTurnovers = AwayTurnovers,
                AwayOffensiveRebounds = AwayOffensiveRebounds,
                AwayDefensiveRebounds = DeviationApplication(Stats.AwayDefensiveRebounds, AwayDeviation.AwayDefensiveRebounds, AwayAverages.AwayDefensiveRebounds),
                AwayBlocks = DeviationApplication(Stats.AwayBlocks, AwayDeviation.AwayBlocks, AwayAverages.AwayBlocks),
                AwaySteals = DeviationApplication(Stats.AwaySteals, AwayDeviation.AwaySteals, AwayAverages.AwaySteals)
            };
            quarter.HomeFGM = quarter.Home3PM + Home2PM;
            quarter.HomePoints = quarter.Home3PM * 3 + Home2PM * 2 + quarter.HomeFTM;
            quarter.AwayFGM = quarter.Away3PM + Away2PM;
            quarter.AwayPoints = quarter.Away3PM * 3 + Away2PM * 2 + quarter.AwayFTM;
            return quarter;
        }
        
        public QuarterPredictions QuarterSimulator(Team HomeTeam, Team AwayTeam, int QuarterNo, int GameNo)
        {
            //FullSeason Lists
            DateTime dt = new DateTime(2022, 2, 20);
            var homeGT = (ServiceResult<List<GameTime>>)_gameTimesRepository.GetFullSeasonForTeamPlayed(HomeTeam);
            homeGT.Data = homeGT.Data.Where(q => q.GameDate.Date > dt).OrderByDescending(x => x.GameDate).ToList();
            var awayGT = (ServiceResult<List<GameTime>>)_gameTimesRepository.GetFullSeasonForTeamPlayed(AwayTeam);
            awayGT.Data = awayGT.Data.Where(q => q.GameDate.Date > dt).OrderByDescending(x => x.GameDate).ToList();
            List<FullSeasonQuarters> homeFullSeasonQuarters = new();
            List<FullSeasonQuarters> awayFullSeasonQuarters = new();
            foreach (var game in homeGT.Data)
            {
                try
                {
                    var quarter = (ServiceResult<FullSeasonQuarters>)_fullSeasonQuartersRepository.GetQuartersWithGameNoAndQuarterNo(game.GameNo, QuarterNo);
                    homeFullSeasonQuarters.Add(quarter.Data);
                }
                catch (Exception)
                {

                }
            }

            foreach (var game in awayGT.Data)
            {
                try
                {
                    var quarter = (ServiceResult<FullSeasonQuarters>)_fullSeasonQuartersRepository.GetQuartersWithGameNoAndQuarterNo(game.GameNo, QuarterNo);
                    awayFullSeasonQuarters.Add(quarter.Data);
                }
                catch (Exception)
                {

                }
            }

            //Standard Deviations
            QuarterModel TotalDeviation = new();
            QuarterModel HomeDeviation = _mathOperations.DeviationCalculator(homeFullSeasonQuarters, HomeTeam);
            _mathOperations.QuarterModelAddition(TotalDeviation, HomeDeviation);
            QuarterModel AwayDeviation = _mathOperations.DeviationCalculator(awayFullSeasonQuarters, AwayTeam);
            _mathOperations.QuarterModelSwitch(AwayDeviation);
            _mathOperations.QuarterModelAddition(TotalDeviation, AwayDeviation);
            //Season Averages with Coefficients

            QuarterModel HomeQuarters = new();
            QuarterModel AwayQuarters = new();
            List<Team> homePlayedAgainst = new();
            foreach (var game in homeFullSeasonQuarters)
            {
                if (HomeTeam == game.HomeTeam && !homePlayedAgainst.Contains(game.AwayTeam))
                {
                    homePlayedAgainst.Add(game.AwayTeam);
                }
                else if (HomeTeam == game.AwayTeam && !homePlayedAgainst.Contains(game.HomeTeam))
                {
                    homePlayedAgainst.Add(game.HomeTeam);
                }
            }
            List<Team> awayPlayedAgainst = new();
            foreach (var game in awayFullSeasonQuarters)
            {
                if (AwayTeam == game.HomeTeam && !awayPlayedAgainst.Contains(game.AwayTeam))
                {
                    awayPlayedAgainst.Add(game.AwayTeam);
                }
                else if (AwayTeam == game.AwayTeam && !awayPlayedAgainst.Contains(game.HomeTeam))
                {
                    awayPlayedAgainst.Add(game.HomeTeam);
                }
            }
            List<Team> commonTeams = new();
            foreach (var team1 in homePlayedAgainst)
            {
                foreach (var team2 in awayPlayedAgainst)
                {
                    if (team1 == team2)
                    {
                        commonTeams.Add(team1);
                    }
                }
            }
            List<FullSeasonQuarters> homePlayedCommon = new();
            List<FullSeasonQuarters> awayPlayedCommon = new();
            foreach (var team in commonTeams)
            {
                homePlayedCommon.Add(homeFullSeasonQuarters.FirstOrDefault(x => x.HomeTeam == team || x.AwayTeam == team));
                awayPlayedCommon.Add(awayFullSeasonQuarters.FirstOrDefault(x => x.HomeTeam == team || x.AwayTeam == team));
            }
            bool isCommonNull = commonTeams.Count == 0;
            bool isFirstGame = homeFullSeasonQuarters.FirstOrDefault(x => x.HomeTeam == HomeTeam && x.AwayTeam == AwayTeam ||
                                                                x.HomeTeam == AwayTeam && x.AwayTeam == HomeTeam) == null;

            if (isFirstGame && isCommonNull)
            {
                QuarterModel homelast5gamesQuarters = _mathOperations.AverageCalculator(homeFullSeasonQuarters.Take(5).ToList(), HomeTeam);
                _mathOperations.QuarterModelDivision(homelast5gamesQuarters, 2);
                QuarterModel homeathomegamesQuarters = _mathOperations.AverageCalculator(homeFullSeasonQuarters.Where(x => x.HomeTeam == HomeTeam).ToList(), HomeTeam);
                _mathOperations.QuarterModelDivision(homeathomegamesQuarters, 2);
                QuarterModel awaylast5gamesQuarters = _mathOperations.AverageCalculator(awayFullSeasonQuarters.Take(5).ToList(), AwayTeam);
                _mathOperations.QuarterModelDivision(awaylast5gamesQuarters, 2);
                QuarterModel awayatawaygamesQuarters = _mathOperations.AverageCalculator(awayFullSeasonQuarters.Where(x => x.AwayTeam == AwayTeam).ToList(), AwayTeam);
                _mathOperations.QuarterModelDivision(awayatawaygamesQuarters, 2);

                _mathOperations.QuarterModelAddition(HomeQuarters, homelast5gamesQuarters);
                _mathOperations.QuarterModelAddition(HomeQuarters, homeathomegamesQuarters);
                _mathOperations.QuarterModelAddition(AwayQuarters, awaylast5gamesQuarters);
                _mathOperations.QuarterModelAddition(AwayQuarters, awayatawaygamesQuarters);
                _mathOperations.QuarterModelSwitch(AwayQuarters);
            }

            else if (isFirstGame && !isCommonNull)
            {
                QuarterModel homelast5gamesQuarters = _mathOperations.AverageCalculator(homeFullSeasonQuarters.Take(5).ToList(), HomeTeam);
                _mathOperations.QuarterModelDivision(homelast5gamesQuarters, 3.166);
                QuarterModel homeathomegamesQuarters = _mathOperations.AverageCalculator(homeFullSeasonQuarters.Where(x => x.HomeTeam == HomeTeam).ToList(), HomeTeam);
                _mathOperations.QuarterModelDivision(homeathomegamesQuarters, 2.375);
                QuarterModel homecommon = _mathOperations.AverageCalculator(homePlayedCommon, HomeTeam);
                _mathOperations.QuarterModelDivision(homecommon, 3.8);
                QuarterModel awaylast5gamesQuarters = _mathOperations.AverageCalculator(awayFullSeasonQuarters.Take(5).ToList(), AwayTeam);
                _mathOperations.QuarterModelDivision(awaylast5gamesQuarters, 3.166);
                QuarterModel awayatawaygamesQuarters = _mathOperations.AverageCalculator(awayFullSeasonQuarters.Where(x => x.AwayTeam == AwayTeam).ToList(), AwayTeam);
                _mathOperations.QuarterModelDivision(awayatawaygamesQuarters, 2.375);
                QuarterModel awaycommon = _mathOperations.AverageCalculator(awayPlayedCommon, AwayTeam);
                _mathOperations.QuarterModelDivision(awaycommon, 3.8);

                _mathOperations.QuarterModelAddition(HomeQuarters, homelast5gamesQuarters);
                _mathOperations.QuarterModelAddition(HomeQuarters, homeathomegamesQuarters);
                _mathOperations.QuarterModelAddition(HomeQuarters, homecommon);
                _mathOperations.QuarterModelAddition(AwayQuarters, awaylast5gamesQuarters);
                _mathOperations.QuarterModelAddition(AwayQuarters, awayatawaygamesQuarters);
                _mathOperations.QuarterModelAddition(AwayQuarters, awaycommon);
                _mathOperations.QuarterModelSwitch(AwayQuarters);
            }

            else if (!isFirstGame && isCommonNull)
            {
                QuarterModel homelast5gamesQuarters = _mathOperations.AverageCalculator(homeFullSeasonQuarters.Take(5).ToList(), HomeTeam);
                _mathOperations.QuarterModelDivision(homelast5gamesQuarters, 2.25);
                QuarterModel homeathomegamesQuarters = _mathOperations.AverageCalculator(homeFullSeasonQuarters.Where(x => x.HomeTeam == HomeTeam).ToList(), HomeTeam);
                _mathOperations.QuarterModelDivision(homeathomegamesQuarters, 2.25);
                QuarterModel awaylast5gamesQuarters = _mathOperations.AverageCalculator(awayFullSeasonQuarters.Take(5).ToList(), AwayTeam);
                _mathOperations.QuarterModelDivision(awaylast5gamesQuarters, 2.25);
                QuarterModel awayatawaygamesQuarters = _mathOperations.AverageCalculator(awayFullSeasonQuarters.Where(x => x.AwayTeam == AwayTeam).ToList(), AwayTeam);
                _mathOperations.QuarterModelDivision(awayatawaygamesQuarters, 2.25);
                QuarterModel seasonmatchupsQuarters = _mathOperations.AverageCalculator(homeFullSeasonQuarters.Where(x => (x.HomeTeam == HomeTeam && x.AwayTeam == AwayTeam) || (x.HomeTeam == AwayTeam && x.AwayTeam == HomeTeam)).ToList(), HomeTeam);
                _mathOperations.QuarterModelDivision(seasonmatchupsQuarters, 9);

                _mathOperations.QuarterModelAddition(HomeQuarters, homelast5gamesQuarters);
                _mathOperations.QuarterModelAddition(HomeQuarters, homeathomegamesQuarters);
                _mathOperations.QuarterModelAddition(HomeQuarters, seasonmatchupsQuarters);
                _mathOperations.QuarterModelAddition(AwayQuarters, awaylast5gamesQuarters);
                _mathOperations.QuarterModelAddition(AwayQuarters, awayatawaygamesQuarters);
                _mathOperations.QuarterModelAdditionSwitch(AwayQuarters, seasonmatchupsQuarters);
                _mathOperations.QuarterModelSwitch(AwayQuarters);
            }

            else
            {
                QuarterModel homelast5gamesQuarters = _mathOperations.AverageCalculator(homeFullSeasonQuarters.Take(5).ToList(), HomeTeam);
                _mathOperations.QuarterModelDivision(homelast5gamesQuarters, 3);
                QuarterModel homeathomegamesQuarters = _mathOperations.AverageCalculator(homeFullSeasonQuarters.Where(x => x.HomeTeam == HomeTeam).ToList(), HomeTeam);
                _mathOperations.QuarterModelDivision(homeathomegamesQuarters, 3);
                QuarterModel homecommon = _mathOperations.AverageCalculator(homePlayedCommon, HomeTeam);
                _mathOperations.QuarterModelDivision(homecommon, 4);
                QuarterModel awaylast5gamesQuarters = _mathOperations.AverageCalculator(awayFullSeasonQuarters.Take(5).ToList(), AwayTeam);
                _mathOperations.QuarterModelDivision(awaylast5gamesQuarters, 3);
                QuarterModel awayatawaygamesQuarters = _mathOperations.AverageCalculator(awayFullSeasonQuarters.Where(x => x.AwayTeam == AwayTeam).ToList(), AwayTeam);
                _mathOperations.QuarterModelDivision(awayatawaygamesQuarters, 3);
                QuarterModel awaycommon = _mathOperations.AverageCalculator(awayPlayedCommon, AwayTeam);
                _mathOperations.QuarterModelDivision(awaycommon, 4);
                QuarterModel seasonmatchupsQuarters = _mathOperations.AverageCalculator(homeFullSeasonQuarters.Where(x => (x.HomeTeam == HomeTeam && x.AwayTeam == AwayTeam) || (x.HomeTeam == AwayTeam && x.AwayTeam == HomeTeam)).ToList(), HomeTeam);
                _mathOperations.QuarterModelDivision(seasonmatchupsQuarters, 12);

                _mathOperations.QuarterModelAddition(HomeQuarters, homelast5gamesQuarters);
                _mathOperations.QuarterModelAddition(HomeQuarters, homeathomegamesQuarters);
                _mathOperations.QuarterModelAddition(HomeQuarters, homecommon);
                _mathOperations.QuarterModelAddition(HomeQuarters, seasonmatchupsQuarters);
                _mathOperations.QuarterModelAddition(AwayQuarters, awaylast5gamesQuarters);
                _mathOperations.QuarterModelAddition(AwayQuarters, awayatawaygamesQuarters);
                _mathOperations.QuarterModelAddition(AwayQuarters, awaycommon);
                _mathOperations.QuarterModelAdditionSwitch(AwayQuarters, seasonmatchupsQuarters);
                _mathOperations.QuarterModelSwitch(AwayQuarters);
            }

            _mathOperations.QuarterModelMultiplication(HomeQuarters, AwayDeviation);
            _mathOperations.QuarterModelMultiplication(AwayQuarters, HomeDeviation);
            _mathOperations.QuarterModelAddition(HomeQuarters, AwayQuarters);
            _mathOperations.QuarterModelDivision(HomeQuarters, TotalDeviation);
            QuarterModel homeSeasonAverages = _mathOperations.AverageCalculator(homeFullSeasonQuarters, HomeTeam);
            QuarterModel awaySeasonAverages = _mathOperations.AverageCalculator(awayFullSeasonQuarters, AwayTeam);
            _mathOperations.QuarterModelSwitch(awaySeasonAverages);
            QuarterPredictions Quarter = QuarterCalculator(HomeQuarters, homeSeasonAverages, awaySeasonAverages, HomeDeviation, AwayDeviation);
            Quarter.HomeTeam = HomeTeam;
            Quarter.AwayTeam = AwayTeam;
            Quarter.QuarterNo = QuarterNo;
            Quarter.GameNo = GameNo;
            Quarter.FirstGame = isFirstGame;
            return Quarter;
        }

        public GamePredictions GameCalculator(GameModel Stats, GameModel HomeAverages, GameModel AwayAverages, GameModel HomeDeviation, GameModel AwayDeviation)
        {
            double paceofplay = PaceofPlayCalculator(Stats);
            double home2PP = (Stats.HomeFGM - Stats.Home3PM) / (Stats.HomeFGA - Stats.Home3PA);
            double away2PP = (Stats.AwayFGM - Stats.Away3PM) / (Stats.AwayFGA - Stats.Away3PA);
            double home3PP = Stats.Home3PM / Stats.Home3PA;
            double away3PP = Stats.Away3PM / Stats.Away3PA;
            double homeFTP = Stats.HomeFTM / Stats.HomeFTA;
            double awayFTP = Stats.AwayFTM / Stats.AwayFTA;
            int Home3PA = DeviationApplication(Stats.Home3PA, HomeDeviation.Home3PA, HomeAverages.Home3PA);
            int HomeTurnovers = DeviationApplication(Stats.HomeTurnovers, HomeDeviation.HomeTurnovers, HomeAverages.HomeTurnovers);
            int HomeOffensiveRebounds = DeviationApplication(Stats.HomeOffensiveRebounds, HomeDeviation.HomeOffensiveRebounds, HomeAverages.HomeOffensiveRebounds);
            int HomeFTA = DeviationApplication(Stats.HomeFTA, HomeDeviation.HomeFTA, HomeAverages.HomeFTA);
            int Home2PA = Convert.ToInt16(Math.Round(paceofplay / 2 - HomeFTA / 2.5 - HomeTurnovers - Home3PA + HomeOffensiveRebounds));
            int Home2PM = DeviationApplication(Home2PA * home2PP, HomeDeviation.HomeFGM - HomeDeviation.Home3PM, HomeAverages.HomeFGM - HomeAverages.Home3PM);
            int Away3PA = DeviationApplication(Stats.Away3PA, AwayDeviation.Away3PA, AwayAverages.Away3PA);
            int AwayTurnovers = DeviationApplication(Stats.AwayTurnovers, AwayDeviation.AwayTurnovers, AwayAverages.AwayTurnovers);
            int AwayOffensiveRebounds = DeviationApplication(Stats.AwayOffensiveRebounds, AwayDeviation.AwayOffensiveRebounds, AwayAverages.AwayOffensiveRebounds);
            int AwayFTA = DeviationApplication(Stats.AwayFTA, AwayDeviation.AwayFTA, AwayAverages.AwayFTA);
            int Away2PA = Convert.ToInt16(Math.Round(paceofplay / 2 - AwayFTA / 2.5 - AwayTurnovers - Away3PA + AwayOffensiveRebounds));
            int Away2PM = DeviationApplication(Away2PA * away2PP, AwayDeviation.AwayFGM - AwayDeviation.Away3PM, AwayAverages.AwayFGM - AwayAverages.Away3PM);
            GamePredictions game = new GamePredictions
            {
                HomeFGA = Home3PA + Home2PA,
                Home3PA = Home3PA,
                Home3PM = DeviationApplication(Home3PA * home3PP, HomeDeviation.Home3PA, HomeAverages.Home3PM),
                HomeFTA = HomeFTA,
                HomeFTM = DeviationApplication(HomeFTA * homeFTP, HomeDeviation.HomeFTM, HomeAverages.HomeFTM),
                HomeAssists = DeviationApplication(Stats.HomeAssists, HomeDeviation.HomeAssists, HomeAverages.HomeAssists),
                HomeTurnovers = HomeTurnovers,
                HomeOffensiveRebounds = HomeOffensiveRebounds,
                HomeDefensiveRebounds = DeviationApplication(Stats.HomeDefensiveRebounds, HomeDeviation.HomeDefensiveRebounds, HomeAverages.HomeDefensiveRebounds),
                HomeBlocks = DeviationApplication(Stats.HomeBlocks, HomeDeviation.HomeBlocks, HomeAverages.HomeBlocks),
                HomeSteals = DeviationApplication(Stats.HomeSteals, HomeDeviation.HomeSteals, HomeAverages.HomeSteals),
                AwayFGA = Away3PA + Away2PA,
                Away3PA = Away3PA,
                Away3PM = DeviationApplication(Away3PA * away3PP, AwayDeviation.Away3PA, AwayAverages.Away3PM),
                AwayFTA = AwayFTA,
                AwayFTM = DeviationApplication(AwayFTA * awayFTP, AwayDeviation.AwayFTM, AwayAverages.AwayFTM),
                AwayAssists = DeviationApplication(Stats.AwayAssists, AwayDeviation.AwayAssists, AwayAverages.AwayAssists),
                AwayTurnovers = AwayTurnovers,
                AwayOffensiveRebounds = AwayOffensiveRebounds,
                AwayDefensiveRebounds = DeviationApplication(Stats.AwayDefensiveRebounds, AwayDeviation.AwayDefensiveRebounds, AwayAverages.AwayDefensiveRebounds),
                AwayBlocks = DeviationApplication(Stats.AwayBlocks, AwayDeviation.AwayBlocks, AwayAverages.AwayBlocks),
                AwaySteals = DeviationApplication(Stats.AwaySteals, AwayDeviation.AwaySteals, AwayAverages.AwaySteals)
            };
            game.HomeFGM = game.Home3PM + Home2PM;
            game.HomePoints = game.Home3PM * 3 + Home2PM * 2 + game.HomeFTM;
            game.AwayFGM = game.Away3PM + Away2PM;
            game.AwayPoints = game.Away3PM * 3 + Away2PM * 2 + game.AwayFTM;
            return game;
        }

        public GamePredictions FullMatchSimulator(Team HomeTeam, Team AwayTeam, int GameNo)
        {
            //FullSeason Lists
            var result = (ServiceResult<List<FullSeason>>)_fullSeasonRepository.GetFullSeasonForTeam(HomeTeam);
            List<FullSeason> homeFullSeason = result.Data.Where(x => x.GameDate.GameDate > new DateTime(2022, 2, 20)).ToList();
            result = (ServiceResult<List<FullSeason>>)_fullSeasonRepository.GetFullSeasonForTeam(AwayTeam);
            List<FullSeason> awayFullSeason = result.Data.Where(x => x.GameDate.GameDate > new DateTime(2022, 2, 20)).ToList();

            //Standard Deviations
            GameModel TotalDeviation = new();
            GameModel HomeDeviation = _mathOperations.DeviationCalculator(homeFullSeason, HomeTeam);
            _mathOperations.GameModelAddition(TotalDeviation, HomeDeviation);
            GameModel AwayDeviation = _mathOperations.DeviationCalculator(awayFullSeason, AwayTeam);
            _mathOperations.GameModelSwitch(AwayDeviation);
            _mathOperations.GameModelAddition(TotalDeviation, AwayDeviation);
            //Season Averages with Coefficients

            GameModel HomeGames = new();
            GameModel AwayGames = new();

            List<Team> homePlayedAgainst = new();
            foreach (var game in homeFullSeason)
            {
                if (HomeTeam == game.HomeTeam && !homePlayedAgainst.Contains(game.AwayTeam))
                {
                    homePlayedAgainst.Add(game.AwayTeam);
                }
                else if (HomeTeam == game.AwayTeam && !homePlayedAgainst.Contains(game.HomeTeam))
                {
                    homePlayedAgainst.Add(game.HomeTeam);
                }
            }
            List<Team> awayPlayedAgainst = new();
            foreach (var game in awayFullSeason)
            {
                if (AwayTeam == game.HomeTeam && !awayPlayedAgainst.Contains(game.AwayTeam))
                {
                    awayPlayedAgainst.Add(game.AwayTeam);
                }
                else if (AwayTeam == game.AwayTeam && !awayPlayedAgainst.Contains(game.HomeTeam))
                {
                    awayPlayedAgainst.Add(game.HomeTeam);
                }
            }
            List<Team> commonTeams = new();
            foreach (var team1 in homePlayedAgainst)
            {
                foreach (var team2 in awayPlayedAgainst)
                {
                    if (team1 == team2)
                    {
                        commonTeams.Add(team1);
                    }
                }
            }
            List<FullSeason> homePlayedCommon = new();
            List<FullSeason> awayPlayedCommon = new();
            foreach (var team in commonTeams)
            {
                homePlayedCommon.Add(homeFullSeason.FirstOrDefault(x => x.HomeTeam == team || x.AwayTeam == team));
                awayPlayedCommon.Add(awayFullSeason.FirstOrDefault(x => x.HomeTeam == team || x.AwayTeam == team));
            }

            bool isCommonNull = commonTeams.Count == 0;
            bool isFirstGame = homeFullSeason.FirstOrDefault(x => x.HomeTeam == HomeTeam && x.AwayTeam == AwayTeam ||
                                                                x.HomeTeam == AwayTeam && x.AwayTeam == HomeTeam) == null;

            if ( isFirstGame && isCommonNull)
            {
                GameModel homelast5games = _mathOperations.AverageCalculator(homeFullSeason.OrderByDescending(x => x.GameDate.GameDate).Take(5).ToList(), HomeTeam);
                _mathOperations.GameModelDivision(homelast5games, 2.5);
                GameModel homeathomegames = _mathOperations.AverageCalculator(homeFullSeason.Where(x => x.HomeTeam == HomeTeam).ToList(), HomeTeam);
                _mathOperations.GameModelDivision(homeathomegames, 1.667);
                GameModel awaylast5games = _mathOperations.AverageCalculator(awayFullSeason.OrderByDescending(x => x.GameDate.GameDate).Take(5).ToList(), AwayTeam);
                _mathOperations.GameModelDivision(awaylast5games, 2.5);
                GameModel awayatawaygames = _mathOperations.AverageCalculator(awayFullSeason.Where(x => x.AwayTeam == AwayTeam).ToList(), AwayTeam);
                _mathOperations.GameModelDivision(awayatawaygames, 1.667);

                _mathOperations.GameModelAddition(HomeGames, homelast5games);
                _mathOperations.GameModelAddition(HomeGames, homeathomegames);
                _mathOperations.GameModelAddition(AwayGames, awaylast5games);
                _mathOperations.GameModelAddition(AwayGames, awayatawaygames);
                _mathOperations.GameModelSwitch(AwayGames);
            }

            else if ( isFirstGame && !isCommonNull)
            {
                GameModel homelast5games = _mathOperations.AverageCalculator(homeFullSeason.OrderByDescending(x => x.GameDate.GameDate).Take(5).ToList(), HomeTeam);
                _mathOperations.GameModelDivision(homelast5games, 4.5);
                GameModel homeathomegames = _mathOperations.AverageCalculator(homeFullSeason.Where(x => x.HomeTeam == HomeTeam).ToList(), HomeTeam);
                _mathOperations.GameModelDivision(homeathomegames, 2.25);
                GameModel homeCommon = _mathOperations.AverageCalculator(homePlayedCommon, HomeTeam);
                _mathOperations.GameModelDivision(homeCommon, 3);
                GameModel awaylast5games = _mathOperations.AverageCalculator(awayFullSeason.OrderByDescending(x => x.GameDate.GameDate).Take(5).ToList(), AwayTeam);
                _mathOperations.GameModelDivision(awaylast5games, 4.5);
                GameModel awayatawaygames = _mathOperations.AverageCalculator(awayFullSeason.Where(x => x.AwayTeam == AwayTeam).ToList(), AwayTeam);
                _mathOperations.GameModelDivision(awayatawaygames, 2.25);
                GameModel awayCommon = _mathOperations.AverageCalculator(awayPlayedCommon, AwayTeam);
                _mathOperations.GameModelDivision(awayCommon, 3);

                _mathOperations.GameModelAddition(HomeGames, homelast5games);
                _mathOperations.GameModelAddition(HomeGames, homeathomegames);
                _mathOperations.GameModelAddition(HomeGames, homeCommon);
                _mathOperations.GameModelAddition(AwayGames, awaylast5games);
                _mathOperations.GameModelAddition(AwayGames, awayatawaygames);
                _mathOperations.GameModelAddition(AwayGames, awayCommon);
                _mathOperations.GameModelSwitch(AwayGames);
            }

            else if ( !isFirstGame && isCommonNull)
            {
                GameModel homelast5games = _mathOperations.AverageCalculator(homeFullSeason.OrderByDescending(x => x.GameDate.GameDate).Take(5).ToList(), HomeTeam);
                _mathOperations.GameModelDivision(homelast5games, 3.5);
                GameModel homeathomegames = _mathOperations.AverageCalculator(homeFullSeason.Where(x => x.HomeTeam == HomeTeam).ToList(), HomeTeam);
                _mathOperations.GameModelDivision(homeathomegames, 1.75);
                GameModel awaylast5games = _mathOperations.AverageCalculator(awayFullSeason.OrderByDescending(x => x.GameDate.GameDate).Take(5).ToList(), AwayTeam);
                _mathOperations.GameModelDivision(awaylast5games, 3.5);
                GameModel awayatawaygames = _mathOperations.AverageCalculator(awayFullSeason.Where(x => x.AwayTeam == AwayTeam).ToList(), AwayTeam);
                _mathOperations.GameModelDivision(awayatawaygames, 1.75);
                GameModel seasonmatchups = _mathOperations.AverageCalculator(homeFullSeason.Where(x => (x.HomeTeam == HomeTeam && x.AwayTeam == AwayTeam) || (x.HomeTeam == AwayTeam && x.AwayTeam == HomeTeam)).ToList(), HomeTeam);
                _mathOperations.GameModelDivision(seasonmatchups, 7);

                _mathOperations.GameModelAddition(HomeGames, homelast5games);
                _mathOperations.GameModelAddition(HomeGames, homeathomegames);
                _mathOperations.GameModelAddition(HomeGames, seasonmatchups);
                _mathOperations.GameModelAddition(AwayGames, awaylast5games);
                _mathOperations.GameModelAddition(AwayGames, awayatawaygames);
                _mathOperations.GameModelAdditionSwitch(AwayGames, seasonmatchups);
                _mathOperations.GameModelSwitch(AwayGames);
            }

            else
            {
                GameModel homelast5games = _mathOperations.AverageCalculator(homeFullSeason.OrderByDescending(x => x.GameDate.GameDate).Take(5).ToList(), HomeTeam);
                _mathOperations.GameModelDivision(homelast5games, 5.25);
                GameModel homeathomegames = _mathOperations.AverageCalculator(homeFullSeason.Where(x => x.HomeTeam == HomeTeam).ToList(), HomeTeam);
                _mathOperations.GameModelDivision(homeathomegames, 2.625);
                GameModel homeCommon = _mathOperations.AverageCalculator(homePlayedCommon, HomeTeam);
                _mathOperations.GameModelDivision(homeCommon, 3);
                GameModel awaylast5games = _mathOperations.AverageCalculator(awayFullSeason.OrderByDescending(x => x.GameDate.GameDate).Take(5).ToList(), AwayTeam);
                _mathOperations.GameModelDivision(awaylast5games, 5.25);
                GameModel awayatawaygames = _mathOperations.AverageCalculator(awayFullSeason.Where(x => x.AwayTeam == AwayTeam).ToList(), AwayTeam);
                _mathOperations.GameModelDivision(awayatawaygames, 2.625);
                GameModel awayCommon = _mathOperations.AverageCalculator(awayPlayedCommon, AwayTeam);
                _mathOperations.GameModelDivision(awayCommon, 3);
                GameModel seasonmatchups = _mathOperations.AverageCalculator(homeFullSeason.Where(x => (x.HomeTeam == HomeTeam && x.AwayTeam == AwayTeam) || (x.HomeTeam == AwayTeam && x.AwayTeam == HomeTeam)).ToList(), HomeTeam);
                _mathOperations.GameModelDivision(seasonmatchups, 10.5);

                _mathOperations.GameModelAddition(HomeGames, homelast5games);
                _mathOperations.GameModelAddition(HomeGames, homeathomegames);
                _mathOperations.GameModelAddition(HomeGames, homeCommon);
                _mathOperations.GameModelAddition(HomeGames, seasonmatchups);
                _mathOperations.GameModelAddition(AwayGames, awaylast5games);
                _mathOperations.GameModelAddition(AwayGames, awayatawaygames);
                _mathOperations.GameModelAddition(AwayGames, awayCommon);
                _mathOperations.GameModelAdditionSwitch(AwayGames, seasonmatchups);
                _mathOperations.GameModelSwitch(AwayGames);
            }

            _mathOperations.GameModelMultiplication(HomeGames, AwayDeviation);
            _mathOperations.GameModelMultiplication(AwayGames, HomeDeviation);
            _mathOperations.GameModelAddition(HomeGames, AwayGames);
            _mathOperations.GameModelDivision(HomeGames, TotalDeviation);
            GameModel homeSeasonAverages = _mathOperations.AverageCalculator(homeFullSeason, HomeTeam);
            GameModel awaySeasonAverages = _mathOperations.AverageCalculator(awayFullSeason, AwayTeam);
            _mathOperations.GameModelSwitch(awaySeasonAverages);
            GamePredictions Game = GameCalculator(HomeGames, homeSeasonAverages, awaySeasonAverages, HomeDeviation, AwayDeviation);
            Game.HomeTeam = HomeTeam;
            Game.AwayTeam = AwayTeam;
            Game.GameNo = GameNo;
            Game.FirstGame = isFirstGame;
            return Game;
        }
    }
}
