namespace Popasu.Domain.DTOs
{
    public class ParameterValueDTO
    {
        public Guid Id { get; set; }
        public string Value { get; set; } = string.Empty;
        public int Date { get; set; }
        public int Month { get; set; }
    }
}
