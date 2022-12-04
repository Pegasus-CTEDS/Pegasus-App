using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pegasus_App.Models
{
    public class User
    {
        public static Guid ID { get; set; }
        public static string? Name { get; set; }
        public static string? Email { get; set; }
        public static void GetUser(string name)
        {
            Name = name;
        }
    }
}
