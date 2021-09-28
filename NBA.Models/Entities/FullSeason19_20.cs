namespace NBA.Models.Entities
{
    using System.ComponentModel.DataAnnotations;
    public class FullSeason19_20 : GameModel
    {
        [Key]
        public int Id { get; set; }
        public GameTime19_20 GameDate { get; set; }
        public int GameNo { get; set; }
        public Team HomeTeam { get; set; }
        public Team AwayTeam { get; set; }
    }
}
