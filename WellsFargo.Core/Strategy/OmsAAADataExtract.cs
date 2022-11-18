using WellsFargo.Contracts.Models;
using WellsFargo.Core.Abstractions;
using WellsFargo.Domain;

namespace WellsFargo.Core.Strategy
{
    public class OmsAAADataExtract : IOmsDataExtractStrategy
    {
        private readonly string OmsType = "AAA";
        private readonly string Delimiter = ",";
        private readonly string FileType = ".aaa";
        private readonly bool IsHeadersRequired = true;

        public bool CanMap(string omsType)
        {
            return OmsType == omsType;
        }
        public OmsTypeResponseModel BuildOmsData(List<TransactionRequestModel> transactions, List<Portfolio> portfolios, List<Security> securities)
        {
            
            var omsAAAResponseModels = new List<OmsResponseModel>();
            foreach (var transaction in transactions)
            {
                var portfolio = portfolios.FirstOrDefault(x => x.PortfolioId == transaction.PortfolioId);
                var secuirty = securities.FirstOrDefault(x => x.SecurityId == transaction.SecurityId);
                var omsAAAResponseModel = new OmsAAAResponseModel
                {
                    ISIN = secuirty?.ISIN,
                    Nominal = transaction.Nominal,
                    PortfolioCode = portfolio?.PortfolioCode,
                    TransactionType = transaction.TransactionType,
                };

                omsAAAResponseModels.Add(omsAAAResponseModel);
            }

            var omsTypeResponseModel = new OmsTypeResponseModel
            {
                OmsType = OmsType,
                IsHeadersRequired = IsHeadersRequired,
                Delimiter = Delimiter,
                FileType = FileType,
                OmsData = omsAAAResponseModels

            };
            return omsTypeResponseModel;
        }

     
    }
}
