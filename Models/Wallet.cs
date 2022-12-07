using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pegasus_App.Models
{
    public static class Wallet
    {
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

        public static void GetWallet()
        {
            
        }

    }
}
