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
        public string? ComercialName { get; set; }
        public double Price { get; set; }
        public double Variation { get; set; }
        public double Invested { get; set; }
        public string? Category { get; set; }

        public List<double>? PriceHistory { get; set; }
        public List<DateTime>? PriceDates { get; set; }

        //asset.Name, asset.ComercialName, asset.Price, asset.Invested, asset.Variation

        public Asset(string name, string comercial_name, double price, double invested, double variation)
        {
            Name = name;
            ComercialName = comercial_name;
            Price = price;
            Invested = invested;
            Variation = variation;
        }
    }
}
