using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class CurrencyToConvert
    {
        public string Key { get; set; }
        public string InputCurrency { get; set; }
        public string OutputCurrency { get; set; }
        public decimal Amount { get; set; }

    }
}
