using WellsFargo.DataAccess.Abstractions;
using WellsFargo.Domain;

namespace WellsFargo.DataAccess
{
    public class PortfolioRepository : CsvFileReader<Portfolio>, IPortfolioRepository
    {
       
    }
}
