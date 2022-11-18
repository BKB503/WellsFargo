using WellsFargo.Contracts.Models;
using WellsFargo.Domain;

namespace WellsFargo.Core.Abstractions
{
    public interface IOmsDataExtractStrategy
    {
        bool CanMap(string omsType);
        OmsTypeResponseModel BuildOmsData(List<TransactionRequestModel> transactions, List<Portfolio> portfolios, List<Security> securities);
    }
}
