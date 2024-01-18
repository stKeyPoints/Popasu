using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Popasu.Domain.Model
{
    public class ParameterValue
    {
        public Guid Id { get; set; }
        public string Value { get; set; } = string.Empty;
        public int Date { get; set; }
        public int Month { get; set; }
    }
}
