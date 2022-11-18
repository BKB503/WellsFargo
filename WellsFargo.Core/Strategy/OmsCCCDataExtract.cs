using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WellsFargo.Contracts.Models;
using WellsFargo.Core.Abstractions;
using WellsFargo.Domain;

namespace WellsFargo.Core.Strategy
{
    public class OmsCCCDataExtract : IOmsDataExtractStrategy
    {
        private readonly string OmsType = "CCC";
        private readonly string Delimiter = ",";
        private readonly string FileType = ".ccc";
        private readonly bool IsHeadersRequired = false;

        public bool CanMap(string omsType)
        {
            return OmsType == omsType;
        }
        public OmsTypeResponseModel BuildOmsData(List<TransactionRequestModel> transactions, List<Portfolio> portfolios, List<Security> securities)
        {
          
            var omsCCCResponseModels = new List<OmsResponseModel>();
            foreach (var transaction in transactions)
            {
                var portfolio = portfolios.FirstOrDefault(x => x.PortfolioId == transaction.PortfolioId);
                var secuirty = securities.FirstOrDefault(x => x.SecurityId == transaction.SecurityId);
                var omsCCCResponseModel = new OmsCCCResponseModel
                {
                    Ticker = secuirty?.Ticker,
                    Nominal = transaction.Nominal,
                    PortfolioCode = portfolio?.PortfolioCode,
                    TransactionType = transaction.TransactionType,
                };

                omsCCCResponseModels.Add(omsCCCResponseModel);
            }

            var omsTypeResponseModel = new OmsTypeResponseModel
            {
                OmsType = OmsType,
                IsHeadersRequired = IsHeadersRequired,
                Delimiter = Delimiter,
                FileType = FileType,
                OmsData = omsCCCResponseModels

            };
            return omsTypeResponseModel;
        }

     
    }
}
