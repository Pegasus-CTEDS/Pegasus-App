using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pegasus_App.Models
{
    public class Asset
    {
        public string? Name { get; set; }
        public string? Category { get; set; }
        public List<double>? PriceHistory { get; set; }
        public List<DateTime>? PriceDates { get; set; }
    }
}
