namespace NBA.Models.Entities
{
    using System.ComponentModel.DataAnnotations;
    public class FullSeason20_21 : GameModel
    {
        [Key]
        public int Id { get; set; }
        public GameTime20_21 GameDate { get; set; }
        public int GameNo { get; set; }
        public Team HomeTeam { get; set; }
        public Team AwayTeam { get; set; }
        public bool OverTime { get; set; }
    }
}
