using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using WellsFargo.Contracts.Models;
using WellsFargo.Core.Abstractions;
using WellsFargo.Core.Services;
using WellsFargo.DataAccess;
using WellsFargo.DataAccess.Abstractions;
using WellsFargo.Domain;
using WellsFargo.TestUtils;

namespace WellsFargo.Core.Tests.Service
{
    public class OrderManagementSystemServiceTests
    {
        private string omsType = "AAA";
        private OrderManagementSystemService subject;
        private Mock<IMetaDataContext> metaDataContext;
        private Mock<IOmsDataExtractResolver> omsDataExtractResolver;
        private Mock<IOmsDataExtractStrategy> omsDataExtractStrategy;
        private List<Security> securities = SecurityFactory.BuildSecurities();
        private List<Portfolio> portfolios = PortfolioFactory.BuildPortfolios();
        private List<TransactionRequestModel> transactions= TransactionFactory.BuildTransactionRequestModel("AAA");



        [SetUp]
        public void Setup()
        {
            metaDataContext = new Mock<IMetaDataContext>();
            omsDataExtractResolver = new Mock<IOmsDataExtractResolver>();
            omsDataExtractStrategy= new Mock<IOmsDataExtractStrategy>();

            omsDataExtractStrategy.Setup( x=> x.CanMap(omsType)).Returns(true);
            omsDataExtractStrategy.Setup( x=> x.BuildOmsData(transactions,portfolios,securities)).Returns(BuildActualResult());

            omsDataExtractResolver.Setup(x => x.Resolve(omsType)).Returns(omsDataExtractStrategy.Object);

            metaDataContext.Setup(x => x.GetSecurities()).Returns(securities);
            metaDataContext.Setup(x => x.GetPortfolios()).Returns(portfolios);

            subject = new OrderManagementSystemService(metaDataContext.Object, omsDataExtractResolver.Object);
        }

        [Test]
        public void GivenEmptyTransactions_WhenCreateOmsTransactions_ThenMustCallMetaDataContextCalls()
        {
            var result = subject.CreateOmsTransactions(new List<Contracts.Models.TransactionRequestModel>());

            metaDataContext.Verify(x => x.GetSecurities(), Times.Never);
            metaDataContext.Verify(x => x.GetPortfolios(), Times.Never);
        }

        [Test]
        public void GivenTransactions_WhenCreateOmsTransactions_ThenMustCallMetaDataContextCalls()
        {
            var result = subject.CreateOmsTransactions(transactions);

            metaDataContext.Verify(x => x.GetSecurities(), Times.Once);
            metaDataContext.Verify(x => x.GetPortfolios(), Times.Once);

        }

        [Test]
        public void GivenTransactions_WhenCreateOmsTransactions_ThenMustCallResolverAndStrategy_ReturnOmsAAATypeData()
        {
            var result = subject.CreateOmsTransactions(transactions);
            var expectedResult = result.ElementAt(0);
            var actualResult = BuildActualResult();

            omsDataExtractResolver.Verify(x => x.Resolve(omsType), Times.Once);
            omsDataExtractStrategy.Verify(x => x.BuildOmsData(transactions, portfolios, securities), Times.Once);
            Assert.That(actualResult.Delimiter, Is.EqualTo(expectedResult.Delimiter));
            Assert.That(actualResult.IsHeadersRequired, Is.EqualTo(expectedResult.IsHeadersRequired));

        }

        private OmsTypeResponseModel BuildActualResult()
        {
            var omsAAAResponseModel = new OmsAAAResponseModel
            {
                ISIN = securities.ElementAt(0).ISIN,
                Nominal = transactions.ElementAt(0).Nominal,
                PortfolioCode = portfolios.ElementAt(0).PortfolioCode,
                TransactionType = transactions.ElementAt(0).TransactionType,
            };

            var omsData = new List<OmsResponseModel>() { omsAAAResponseModel };
            return new OmsTypeResponseModel
            {
                Delimiter = ",",
                FileType = ".aaa",
                IsHeadersRequired = true,
                OmsType = omsType,
                OmsData = omsData
            };
        }
    }
}
