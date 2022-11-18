using System.Drawing;
using WellsFargo.Domain;

namespace WellsFargo.TestUtils
{
    public static class PortfolioFactory
    {
        public static List<Portfolio> BuildPortfolios()
        {
            var portfolios = new List<Portfolio>();
            for (int i = 1; i <= 10; i++)
            {
                var portfolio = new Portfolio
                {
                   PortfolioId= i,
                   PortfolioCode = $"PortfolioCode{i}",                    
                };
                portfolios.Add(portfolio);
            }
            return portfolios;
        }
    }
}