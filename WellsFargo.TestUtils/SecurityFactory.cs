using System.Drawing;
using WellsFargo.Domain;

namespace WellsFargo.TestUtils
{
    public static class SecurityFactory
    {
        public static List<Security> BuildSecurities()
        {
            var securities = new List<Security>();
            for (int i = 1; i <= 10; i++)
            {
                var security = new Security
                {
                    SecurityId = i,
                    CUSIP= $"CUSIP{i}",
                    ISIN =$"ISIN{i}",
                    Ticker = $"Ticker{i}",
                                  
                };
                securities.Add(security);
            }
            return securities;
        }
    }
}