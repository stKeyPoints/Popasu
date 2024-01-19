namespace Popasu.Domain.DTOs
{
    public class WindRoseCreateDTO
    {
        public List<Guid> ParameterIDs { get; set; } = null!;
        public string ParametersInput { get; set; } = string.Empty;
    }
}
