namespace Popasu.Domain.DTOs
{
    public class ClimateCreateDTO
    {
        public Guid WindRoseId { get; set; }
        public List<Guid> ParameterIDs { get; set; } = null!;
        public string ParametersInput { get; set; } = null!;
    }
}
