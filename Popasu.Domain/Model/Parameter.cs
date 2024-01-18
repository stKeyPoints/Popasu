using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Popasu.Domain.Model
{
    public class Parameter
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Unit { get; set; } = string.Empty;
        public ParameterValue ParameterValue { get; set; } = null!;
        public int Year { get; set; }
    }
}
