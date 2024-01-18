namespace Popasu.Domain.DTOs
{
    public class ParameterDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Unit { get; set; } = string.Empty;
        public int Year { get; set; }
        public string ParameterValue { get; set; } = string.Empty;
    }
}
