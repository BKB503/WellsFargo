using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WellsFargo.Domain
{
    public class Security
    {
        public int SecurityId { get; set; }
        public string ISIN { get; set; }
        public string Ticker { get; set; }
        public string CUSIP { get; set; }
    }
}
