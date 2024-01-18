using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Popasu.Domain.Model
{
    public class WindRose
    {
        public Guid Id { get; set; }
        public List<Parameter> Parameters { get; set; } = null!;
    }
}
