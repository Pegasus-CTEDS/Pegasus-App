using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pegasus_App.Models
{
    public class Investment
    {
        /// <summary>
        /// uniquely indentifies the investment in the database
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// comercial use name of the investment
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// amount of money in the investment
        /// </summary>
        public double Amount { get; set; }

        /// <summary>
        /// the actual balance of the investment
        /// </summary>
        public double Balance { get; set; }

        /// <summary>
        /// the actual yield of the investment, in a predeterminated period
        /// </summary>
        public double Yield { get; set; }

        public Investment(Guid id, string? name, double amount, double balance, double yield)
        {
            ID = id;
            Name = name;
            Amount = amount;
            Balance = balance;
            Yield = yield;
        }
    }
}
