namespace NBA.Models.Entities
{
    using System.ComponentModel.DataAnnotations;
    public class PlayerStatsQuarter
    {
        [Key]
        public int Id { get; set; }
        public int GameNo { get; set; }
        public int QuarterNo { get; set; }
        public Players Player { get; set; }
        public Team Team { get; set; }
        public int Minutes { get; set; }
        public int Seconds { get; set; }
        public double PlayerPoints { get; set; }
        public double PlayerAssists { get; set; }
        public double PlayerDefensiveRebounds { get; set; }
        public double PlayerOffensiveRebounds { get; set; }
        public double PlayerBlocks { get; set; }
        public double PlayerSteals { get; set; }
        public double PlayerTurnovers { get; set; }
        public double PlayerFGA { get; set; }
        public double PlayerFGM { get; set; }
        public double Player3PA { get; set; }
        public double Player3PM { get; set; }
        public double PlayerFTA { get; set; }
        public double PlayerFTM { get; set; }
        public double PlayerFouls { get; set; }
        public double PlayerPlusMinus { get; set; }
    }
}
