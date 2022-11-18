using WellsFargo.Contracts.Models;
using WellsFargo.Core.Abstractions;
using WellsFargo.DataAccess.Abstractions;

namespace WellsFargo.Core.Services
{
    public class OrderManagementSystemService : IOrderManagementSystemService
    {
        private readonly IMetaDataContext metaDataContext;
        private readonly IOmsDataExtractResolver omsDataExtractResolver;
        public OrderManagementSystemService(IMetaDataContext metaDataContext, IOmsDataExtractResolver omsDataExtractResolver)
        {
            this.metaDataContext   = metaDataContext;
            this.omsDataExtractResolver = omsDataExtractResolver;
        }
        public List<OmsTypeResponseModel> CreateOmsTransactions(List<TransactionRequestModel> transactionRequestModels)
        {
            var omsTypeResponseModelList = new List<OmsTypeResponseModel>();
          
            if (transactionRequestModels.Any())
            {
                var portfolios = metaDataContext.GetPortfolios();
                var securities = metaDataContext.GetSecurities();
                var distinctOmsTypeGroups = transactionRequestModels.GroupBy(x => x.OMS, (key, group) => new { OMSType = key, Transactions = group.ToList() });

                foreach (var omsGroupType in distinctOmsTypeGroups)
                {
                    var dataExtractStrategy = omsDataExtractResolver.Resolve(omsGroupType.OMSType);
                    var omsTypeResponseModel = dataExtractStrategy.BuildOmsData(omsGroupType.Transactions, portfolios, securities);
                    //var omsTypeResponseModel = omsDataExtractStrategyContext.GetData(omsGroupType.OMS, omsGroupType.Items, portfolios, securities);
                    omsTypeResponseModelList.Add(omsTypeResponseModel);
                }

                return omsTypeResponseModelList;
            }
            return new List<OmsTypeResponseModel>();
        }
    }
}
