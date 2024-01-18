using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Popasu.Domain.Model
{
    public class Climate
    {
        public Guid Id { get; set; }
        public WindRose WindRose { get; set; } = null!;
        public List<Parameter> Parameters { get; set; } = new List<Parameter>();
    }
}
