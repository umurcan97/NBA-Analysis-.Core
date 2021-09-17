namespace NBA.Models.Entities
{
    using System.ComponentModel.DataAnnotations;
    public class FullSeasonQuarters : QuarterModel
    {
        [Key]
        public int ID { get; set; }
        public int GameNo { get; set; }
        public int QuarterNo { get; set; }
        public Team HomeTeam { get; set; }
        public Team AwayTeam { get; set; }
    }
}
