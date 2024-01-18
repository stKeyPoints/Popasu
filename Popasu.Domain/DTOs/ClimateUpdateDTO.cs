namespace Popasu.Domain.DTOs
{
    public class ClimateUpdateDTO
    {
        public Guid Id { get; set; }
        public Guid WindRoseId { get; set; }
        public List<Guid> ParameterIDs { get; set; } = null!;
    }
}
