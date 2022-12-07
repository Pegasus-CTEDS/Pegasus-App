using Pegasus_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Pegasus_App.Pages
{
    /// <summary>
    /// Interação lógica para Wallet.xam
    /// </summary>
    public partial class Wallet : Page
    {
        public Wallet()
        {
            InitializeComponent();
            LoadInvestments();
        }
        public void LoadInvestments()
        {
            Models.Wallet.GetWallet();
            List<string> nomes = new List<string>();
            nomes.Add("fsfs");
            nomes.Add("fsfrrts");
            nomes.Add("fsfttts");
            nomes.Add("jhj");
            foreach (var inv in nomes)
            {
                TextBlock tb = new TextBlock();
                tb.Text = inv;
                tb.FontSize = 24;
            }
        }
    }
}
