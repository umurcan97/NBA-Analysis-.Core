namespace NBA.Services.Calculations
{
    using NBA.Models;
    using NBA.Models.Entities;
    using System.Collections.Generic;

    public interface IMathOperations
    {
        List<FullSeason> ArrangeStats(List<FullSeason> Stats, Team team);
        List<FullSeasonQuarters> ArrangeStats(List<FullSeasonQuarters> Stats, Team team);
        List<FullSeason19_20> ArrangeStats(List<FullSeason19_20> Stats, Team team);
        List<FullSeasonQuarters19_20> ArrangeStats(List<FullSeasonQuarters19_20> Stats, Team team);
        GameModel FullSeasonToGameModel(FullSeason Stats);
        QuarterModel GameModeltoQuarterModel(GameModel Stats);
        void GameModelAddition(GameModel stat1, FullSeason stat2);
        void GameModelAdditionSwitch(GameModel stat1, GameModel stat2);
        void GameModelSwitch(GameModel stat1);
        void GameModelAddition(GameModel stat1, GameModel stat2);
        FullSeason GameModelSubtraction(FullSeason stat1, GameModel stat2);
        FullSeason20_21 GameModelSubtraction(FullSeason20_21 stat1, GameModel stat2);
        FullSeason19_20 GameModelSubtraction(FullSeason19_20 stat1, GameModel stat2);
        void GameModelMultiplication(GameModel stat1, GameModel stat2);
        void GameModelMultiplication(FullSeason stat1, FullSeason stat2);
        void GameModelSquareRoot(GameModel stat);
        void GameModelDivision(GameModel stats, double divider);
        void GameModelDivision(GameModel stat1, GameModel stat2);
        QuarterModel QuarterModelAddition(QuarterModel stat1, QuarterModel stat2);
        QuarterModel QuarterModelAddition(QuarterModel stat1, FullSeasonQuarters stat2);
        void QuarterModelAdditionSwitch(QuarterModel stat1, QuarterModel stat2);
        void QuarterModelSwitch(QuarterModel stat1);
        void QuarterModelAdditionSwitch(FullSeasonQuarters stat1, FullSeasonQuarters stat2);
        FullSeasonQuarters QuarterModelSubtraction(FullSeasonQuarters stat1, QuarterModel stat2);
        FullSeasonQuarters20_21 QuarterModelSubtraction(FullSeasonQuarters20_21 stat1, QuarterModel stat2);
        FullSeasonQuarters19_20 QuarterModelSubtraction(FullSeasonQuarters19_20 stat1, QuarterModel stat2);
        void QuarterModelMultiplication(QuarterModel stat1, QuarterModel stat2);
        void QuarterModelMultiplication(QuarterModel stat1, FullSeasonQuarters stat2);
        void QuarterModelSquareRoot(QuarterModel stat);
        void QuarterModelDivision(QuarterModel stats, double divider);
        void QuarterModelDivision(QuarterModel stat1, QuarterModel stat2);
        GameModel AverageCalculator(List<FullSeason> stats, Team team);
        GameModel DeviationCalculator(List<FullSeason> stats, Team team);
        QuarterModel AverageCalculator(List<FullSeasonQuarters> stats, Team team);
        QuarterModel DeviationCalculator(List<FullSeasonQuarters> stats, Team team);
        GameModel AverageCalculator(List<FullSeason20_21> stats, Team team);
        GameModel DeviationCalculator(List<FullSeason20_21> stats, Team team);
        QuarterModel AverageCalculator(List<FullSeasonQuarters20_21> stats, Team team);
        QuarterModel DeviationCalculator(List<FullSeasonQuarters20_21> stats, Team team);
        GameModel AverageCalculator(List<FullSeason19_20> stats, Team team);
        GameModel DeviationCalculator(List<FullSeason19_20> stats, Team team);
        QuarterModel AverageCalculator(List<FullSeasonQuarters19_20> stats, Team team);
        QuarterModel DeviationCalculator(List<FullSeasonQuarters19_20> stats, Team team);
    }
}
