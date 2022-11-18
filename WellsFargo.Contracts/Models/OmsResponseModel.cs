using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WellsFargo.Contracts.Models
{
    public class OmsResponseModel
    {
        public decimal Nominal { get; set; }
        public string TransactionType { get; set; }
        public string PortfolioCode { get; set; }
    }
}
