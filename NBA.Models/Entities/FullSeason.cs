namespace NBA.Models.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class FullSeason : GameModel
    {
        [Key]
        public int Id { get; set; }
        public GameTime GameDate { get; set; }
        public int GameNo { get; set; }
        public Team HomeTeam { get; set; }
        public Team AwayTeam { get; set; }
        public bool OverTime { get; set; }
    }
}
