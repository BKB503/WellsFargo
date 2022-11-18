using WellsFargo.Contracts.Models;
using WellsFargo.Core.Abstractions;
using WellsFargo.Core.Strategy;
using WellsFargo.Domain;
using WellsFargo.TestUtils;

namespace WellsFargo.Core.Tests.Strategy
{
    public class OmsCCCDataExtractTests
    {
        private OmsCCCDataExtract subject;
        private readonly string omsType = "CCC";
        private List<TransactionRequestModel> transactions;
        private List<Portfolio> portfolios;
        private List<Security> securities;
        private OmsTypeResponseModel result;

        [SetUp]
        public void Setup()
        {
            transactions = TransactionFactory.BuildTransactionRequestModel(omsType);
            portfolios = PortfolioFactory.BuildPortfolios();
            securities= SecurityFactory.BuildSecurities();

            subject = new OmsCCCDataExtract();
        }

        [Test]
        public void ImplementsContractOfTypeIOmsDataExtract()
        {
            Assert.That(subject, Is.InstanceOf(typeof(IOmsDataExtractStrategy)));
        }

        [Test]
        public void CanMap_ForOmsTypeCCC_ShouldReturnTrue()
        {
            Assert.That(subject.CanMap(omsType), Is.True);
        }

        [Test]
        public void CanMap_ForOmsTypeWrongType_ShouldReturnFalse()
        {
            Assert.That(subject.CanMap("dummy"), Is.False);
        }

        [Test]
        public void GivenOmsBBBTransactionsWithEmptyInputs_WhenBuildOmsData_ShouldReturnOmsTypeResponseModel()
        {
            result = subject.BuildOmsData(new List<TransactionRequestModel>(), new List<Portfolio>(), securities);

            Assert.That(result.OmsData.Count, Is.EqualTo(0));


        }

        [Test]
        public void GivenOmsBBBTransactionsWithEmptyPortfolios_WhenBuildOmsData_ShouldReturnOmsTypeResponseModel()
        {
            result = subject.BuildOmsData(transactions, new List<Portfolio>(), securities);

            
            for (int i = 0; i < result.OmsData.Count; i++)
            {
                Assert.That(result.OmsData.ElementAt(i).PortfolioCode, Is.EqualTo(null));
            }
            
        }

        [Test]
        public void GivenOmsBBBTransactionsWithEmptySecurities_WhenBuildOmsData_ShouldReturnOmsTypeResponseModel()
        {
            result = subject.BuildOmsData(transactions, portfolios, new List<Security>());

           
            for (int i = 0; i < result.OmsData.Count; i++)
            {
                var omsDataCCC = (OmsCCCResponseModel) result.OmsData.ElementAt(i);
                Assert.That(omsDataCCC.Ticker, Is.EqualTo(null));
            }


        }

        [Test]
        public void GivenOmsBBBTransactions_WhenBuildOmsData_ShouldReturnOmsTypeResponseModel()
        {
            result = subject.BuildOmsData(transactions,portfolios, securities);

            Assert.That(result.OmsType, Is.EqualTo(omsType));
            Assert.That(result.IsHeadersRequired, Is.EqualTo(false));
            Assert.That(result.FileType, Is.EqualTo(".ccc"));
            Assert.That(result.Delimiter, Is.EqualTo(","));
            Assert.That(result.OmsData.Count, Is.EqualTo(10));

            for (int i = 0; i < result.OmsData.Count; i++)
            {
                var omsDataCCC = (OmsCCCResponseModel)result.OmsData.ElementAt(i);
                var secuirty = securities?.FirstOrDefault(x => x.Ticker == omsDataCCC.Ticker);
                var portfoilo = portfolios?.FirstOrDefault(x => x.PortfolioCode == omsDataCCC.PortfolioCode);
                var transaction = transactions.FirstOrDefault(x => x.SecurityId == secuirty.SecurityId && x.PortfolioId == portfoilo.PortfolioId);

                Assert.That(omsDataCCC.Ticker, Is.EqualTo(secuirty?.Ticker));
                Assert.That(omsDataCCC.PortfolioCode, Is.EqualTo(portfoilo?.PortfolioCode));
                Assert.That(omsDataCCC.Nominal, Is.EqualTo(transaction.Nominal));
                Assert.That(omsDataCCC.TransactionType, Is.EqualTo(transaction.TransactionType));
            }
        }


    }
}