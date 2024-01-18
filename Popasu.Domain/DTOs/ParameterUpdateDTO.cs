namespace Popasu.Domain.DTOs
{
    public class ParameterUpdateDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Unit { get; set; } = string.Empty;
        public int Year { get; set; }
        public string ParameterValueId { get; set; } = null!;
    }
}
