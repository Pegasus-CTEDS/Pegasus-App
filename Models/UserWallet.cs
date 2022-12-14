using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pegasus_App.Models
{
    public static class UserWallet
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
            UserAssets = new List<Asset>();
            UserAssets.Add(new Asset("ASAI3", "ASSAÍ", 18.63, 2013.21, -1.27));
            UserAssets.Add(new Asset("VALE3", "VALE ON EJ NM", 86.16, 2013.21, 0.36));
            UserAssets.Add(new Asset("PETR4", "PETROBRAS PN N2", 23.32, 2013.21, -2.46));
            UserAssets.Add(new Asset("AMBEV3", "AMBV S/ A ON", 15.28, 2013.21, 0.26));
            UserAssets.Add(new Asset("BBAS3", "BRASIL ON EJ NM", 31.86, 2013.21, -4.88));
            ComputeGlobalMetrics();
        }
        public static void ComputeGlobalMetrics()
        {
            Amount = 0;
            Balance = 0;
            Yield = 0;
            foreach(var asset in UserAssets)
            {
                Amount += asset.Invested;
                Balance += 0;
                Yield += 0;
            }
        }
        /// <summary>
        /// list of all the assets owned by the user
        /// </summary>
        public static List<Asset>? UserAssets { get; set; }

    }
}
