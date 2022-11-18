using WellsFargo.Contracts.Models;
using WellsFargo.Domain;

namespace WellsFargo.Core.Abstractions
{
    public interface IOmsDataExtractStrategyContext
    {
        OmsTypeResponseModel GetData(string omsType, List<TransactionRequestModel> transactions, List<Portfolio> portfolios, List<Security> securities);
    }
}
