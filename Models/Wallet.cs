using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pegasus_App.Models
{
    public static class Wallet
    {
        /// Uniquely indentifies the wallet in the database
        /// </summary>
        public static Guid ID { get; set; }

        /// <summary>
        /// name of the wallet, for personal use or reference
        /// </summary>
        public static string? Name { get; set; }

        /// <summary>
        /// sum of the amounts of all investment
        /// </summary>
        public static double Amount { get; set; }

        /// <summary>
        /// sum of the balances of all investment
        /// </summary>
        public static double Balance { get; set; }

        /// <summary>
        /// sum of the yields of all investment
        /// </summary>
        public static double Yield { get; set; }

        /// <summary>
        /// list of all the investments in the current wallet
        /// </summary>
        public static List<Investment>? Investments { get; set; }

    }
}
