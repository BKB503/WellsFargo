using WellsFargo.Domain;

namespace WellsFargo.DataAccess.Abstractions
{
    public interface IPortfolioRepository : ICsvFileReader<Portfolio>
    {

    }
}