using System.Transactions;
using WellsFargo.Contracts.Models;
using WellsFargo.Core.Abstractions;
using WellsFargo.Core.Strategy;
using WellsFargo.DataAccess.Abstractions;
using WellsFargo.Domain;
using WellsFargo.TestUtils;

namespace WellsFargo.Core.Tests.Strategy
{
    public class OmsAAADataExtractTests
    {
        private OmsAAADataExtract subject;
        private readonly string omsType = "AAA";
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

            subject = new OmsAAADataExtract();
        }

        [Test]
        public void ImplementsContractOfTypeIOmsDataExtract()
        {
            Assert.That(subject, Is.InstanceOf(typeof(IOmsDataExtractStrategy)));
        }

        [Test]
        public void CanMap_ForOmsTypeAAA_ShouldReturnTrue()
        {
            Assert.That(subject.CanMap(omsType), Is.True);
        }

        [Test]
        public void CanMap_ForOmsTypeWrongType_ShouldReturnFalse()
        {
            Assert.That(subject.CanMap("dummy"), Is.False);
        }

        [Test]
        public void GivenOmsAAATransactionsWithEmptyInputs_WhenBuildOmsData_ShouldReturnOmsTypeResponseModel()
        {
            result = subject.BuildOmsData(new List<TransactionRequestModel>(), new List<Portfolio>(), securities);

            Assert.That(result.OmsData.Count, Is.EqualTo(0));


        }

        [Test]
        public void GivenOmsAAATransactionsWithEmptyPortfolios_WhenBuildOmsData_ShouldReturnOmsTypeResponseModel()
        {
            result = subject.BuildOmsData(transactions, new List<Portfolio>(), securities);

            
            for (int i = 0; i < result.OmsData.Count; i++)
            {
                Assert.That(result.OmsData.ElementAt(i).PortfolioCode, Is.EqualTo(null));
            }
            
        }

        [Test]
        public void GivenOmsAAATransactionsWithEmptySecurities_WhenBuildOmsData_ShouldReturnOmsTypeResponseModel()
        {
            result = subject.BuildOmsData(transactions, portfolios, new List<Security>());

           
            for (int i = 0; i < result.OmsData.Count; i++)
            {
                var omsDataAAA = (OmsAAAResponseModel) result.OmsData.ElementAt(i);
                Assert.That(omsDataAAA.ISIN, Is.EqualTo(null));
            }


        }

        [Test]
        public void GivenOmsAAATransactions_WhenBuildOmsData_ShouldReturnOmsTypeResponseModel()
        {
            result = subject.BuildOmsData(transactions,portfolios, securities);

            Assert.That(result.OmsType, Is.EqualTo(omsType));
            Assert.That(result.IsHeadersRequired, Is.EqualTo(true));
            Assert.That(result.FileType, Is.EqualTo(".aaa"));
            Assert.That(result.Delimiter, Is.EqualTo(","));
            Assert.That(result.OmsData.Count, Is.EqualTo(10));

            for (int i = 0; i < result.OmsData.Count; i++)
            {
                var omsDataAAA = (OmsAAAResponseModel)result.OmsData.ElementAt(i);
                var secuirty = securities?.FirstOrDefault(x => x.ISIN == omsDataAAA.ISIN);
                var portfoilo = portfolios?.FirstOrDefault(x => x.PortfolioCode == omsDataAAA.PortfolioCode);
                var transaction = transactions.FirstOrDefault(x => x.SecurityId == secuirty.SecurityId && x.PortfolioId == portfoilo.PortfolioId);

                Assert.That(omsDataAAA.ISIN, Is.EqualTo(secuirty?.ISIN));
                Assert.That(omsDataAAA.PortfolioCode, Is.EqualTo(portfoilo?.PortfolioCode));
                Assert.That(omsDataAAA.Nominal, Is.EqualTo(transaction.Nominal));
                Assert.That(omsDataAAA.TransactionType, Is.EqualTo(transaction.TransactionType));
            }
        }


    }
}