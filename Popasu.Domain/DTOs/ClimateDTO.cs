namespace Popasu.Domain.DTOs
{
    public class ClimateDTO
    {
        public Guid Id { get; set; }
        public WindRoseDTO WindRose { get; set; } = new WindRoseDTO();
        public List<string> Parameters { get; set; } = new List<string>();
    }
}
