namespace Popasu.Domain.DTOs
{
    public class WindRoseUpdateDTO
    {
        public Guid Id { get; set; }
        public List<Guid> ParameterIDs { get; set; } = null!;
    }
}
