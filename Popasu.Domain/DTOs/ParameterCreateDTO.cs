﻿namespace Popasu.Domain.DTOs
{
    public class ParameterCreateDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Unit { get; set; } = string.Empty;
        public int Year { get; set; }
        public Guid ParameterValueId { get; set; }
    }
}
