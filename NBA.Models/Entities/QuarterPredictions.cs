namespace NBA.Models.Entities
{
    using System.ComponentModel.DataAnnotations;
    public class QuarterPredictions : QuarterModel
    {
        [Key]
        public int Id { get; set; }
        public bool FirstGame { get; set; }
        public int GameNo { get; set; }
        public int QuarterNo { get; set; }
        public Team HomeTeam { get; set; }
        public Team AwayTeam { get; set; }
    }
}
