namespace Popasu.Domain.DTOs
{
    public class WindRoseUpdateDTO
    {
        public Guid Id { get; set; }
        public List<string> ParametersIDs { get; set; } = new List<string>();
    }
}
