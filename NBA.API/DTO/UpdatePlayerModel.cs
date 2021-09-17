namespace NBA.API.DTO
{
using NBA.Models;
    public class UpdatePlayerModel
    {
        public string Name { get; set; }
        public Team Team { get; set; }
        public int Number { get; set; }
        public string Position { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public string Country { get; set; }
    }
}
