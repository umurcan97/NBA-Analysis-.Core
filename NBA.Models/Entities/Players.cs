namespace NBA.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Players
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public Team Team { get; set; }
        public int Number { get; set; }
        public string Position { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public string Country { get; set; }
        [NotMapped]
        public int line { get; set; }
    }
}
