using WellsFargo.DataAccess.Abstractions;
using WellsFargo.Domain;

namespace WellsFargo.DataAccess.Tests
{
    public class PortfolioRepositoryTests
    {
        private PortfolioRepository subject;
        private List<Portfolio> result;

        [SetUp]
        public void Setup()
        {
            subject = new PortfolioRepository();
        }

        [Test]
        public void ImplementsContract()
        {
            Assert.That(subject, Is.InstanceOf(typeof(IPortfolioRepository)));
        }

        [Test]
        public void WhenGetPortfolios_ThenReturnDirectoryNotFoundException()
        {
            var filePath = $"{Directory.GetCurrentDirectory()}\\CsvFilesd\\portfolios.csv";
            Assert.Throws<DirectoryNotFoundException>(() => subject.GetData(filePath));
        }

        [Test]
        public void WhenGetDataPortfolios_ThenReturnPortfolios()
        {
            var filePath = $"{Directory.GetCurrentDirectory()}\\CsvFiles\\portfolios.csv";
           
            result = subject.GetData(filePath);
           
            Assert.That(result.Count,Is.EqualTo(2));
            Assert.That(result.ElementAt(0).PortfolioId,Is.EqualTo(1));
            Assert.That(result.ElementAt(0).PortfolioCode,Is.EqualTo("p1"));
            Assert.That(result.ElementAt(1).PortfolioId, Is.EqualTo(2));
            Assert.That(result.ElementAt(1).PortfolioCode, Is.EqualTo("p2"));
        }

    
    }
}