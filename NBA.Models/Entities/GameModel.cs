namespace NBA.Models.Entities
{
    public class GameModel : QuarterModel
    {
        public double HomeTotalRebounds { get; set; }
        public double HomePITP { get; set; }
        public double HomeFastBreakPoints { get; set; }
        public double HomeBenchPoints { get; set; }
        public double HomePointsofTO { get; set; }
        public double AwayTotalRebounds { get; set; }
        public double AwayPITP { get; set; }
        public double AwayFastBreakPoints { get; set; }
        public double AwayBenchPoints { get; set; }
        public double AwayPointsofTO { get; set; }
    }
}
