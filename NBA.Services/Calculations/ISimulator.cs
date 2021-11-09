namespace NBA.Services.Calculations
{
    using NBA.Models;
    using NBA.Models.Entities;
    public interface ISimulator
    {
        int DeviationApplication(double stat, double statdeviation, double stataverage);
        double PaceofPlayCalculator(QuarterModel stats);

        QuarterPredictions QuarterCalculator(QuarterModel Stats, QuarterModel HomeAverages, QuarterModel AwayAverages,
            QuarterModel HomeDeviation, QuarterModel AwayDeviation);

        QuarterPredictions QuarterSimulator(Team HomeTeam, Team AwayTeam, int QuarterNo, int GameNo);

        GamePredictions GameCalculator(GameModel Stats, GameModel HomeAverages, GameModel AwayAverages,
            GameModel HomeDeviation, GameModel AwayDeviation);

        GamePredictions FullMatchSimulator(Team HomeTeam, Team AwayTeam, int GameNo);
    }
}
