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
    public class OmsBBBDataExtract : IOmsDataExtractStrategy
    {
        private readonly string OmsType = "BBB";
        private readonly string Delimiter = "|";
        private readonly string FileType = ".bbb";
        private readonly bool IsHeadersRequired = true;

        public bool CanMap(string omsType)
        {
            return OmsType == omsType;
        }
        public OmsTypeResponseModel BuildOmsData(List<TransactionRequestModel> transactions, List<Portfolio> portfolios, List<Security> securities)
        {
            
            var omsBBBResponseModels = new List<OmsResponseModel>();
            foreach (var transaction in transactions)
            {
                var portfolio = portfolios.FirstOrDefault(x => x.PortfolioId == transaction.PortfolioId);
                var secuirty = securities.FirstOrDefault(x => x.SecurityId == transaction.SecurityId);
                var omsBBBResponseModel = new OmsBBBResponseModel
                {
                    Cusip = secuirty?.CUSIP,
                    Nominal = transaction.Nominal,
                    PortfolioCode = portfolio?.PortfolioCode,
                    TransactionType = transaction.TransactionType,
                };

                omsBBBResponseModels.Add(omsBBBResponseModel);
            }

            var omsTypeResponseModel = new OmsTypeResponseModel
            {
                OmsType = OmsType,
                IsHeadersRequired = IsHeadersRequired,
                Delimiter = Delimiter,
                FileType = FileType,
                OmsData = omsBBBResponseModels

            };
            return omsTypeResponseModel;
        }

     
    }
}
