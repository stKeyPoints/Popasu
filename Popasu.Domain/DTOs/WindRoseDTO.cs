namespace Popasu.Domain.DTOs
{
    public class WindRoseDTO
    {
        public Guid Id { get; set; }
        public List<string> Parameters { get; set; } = new List<string>();
    }
}
