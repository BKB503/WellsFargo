using WellsFargo.DataAccess.Abstractions;
using WellsFargo.Domain;

namespace WellsFargo.DataAccess.Tests
{
    public class SecurityRepositoryTests
    {
        private SecurityRepository subject;
        private List<Security> result;

        [SetUp]
        public void Setup()
        {
            subject = new SecurityRepository();
        }

        [Test]
        public void ImplementsContract()
        {
            Assert.That(subject, Is.InstanceOf(typeof(ISecurityRepository)));
        }

        [Test]
        public void WhenGetSecurities_ThenReturnDirectoryNotFoundException()
        {
            var filePath = $"{Directory.GetCurrentDirectory()}\\NotFilePath\\securities.csv";
            Assert.Throws<DirectoryNotFoundException>(() => subject.GetData(filePath));
        }

        [Test]
        public void WhenGetSecurities_ThenReturnSecurities()
        {
            var filePath = $"{Directory.GetCurrentDirectory()}\\CsvFiles\\securities.csv";
           
            result = subject.GetData(filePath);
           
            Assert.That(result.Count,Is.EqualTo(2));
            Assert.That(result.ElementAt(0).SecurityId,Is.EqualTo(1));
            Assert.That(result.ElementAt(0).Ticker,Is.EqualTo("s1"));
            Assert.That(result.ElementAt(0).CUSIP, Is.EqualTo("CUSIP0001"));
            Assert.That(result.ElementAt(0).ISIN, Is.EqualTo("ISIN11111111"));
            Assert.That(result.ElementAt(1).SecurityId, Is.EqualTo(2));
            Assert.That(result.ElementAt(1).Ticker, Is.EqualTo("s2"));
            Assert.That(result.ElementAt(1).CUSIP, Is.EqualTo("CUSIP0002"));
            Assert.That(result.ElementAt(1).ISIN, Is.EqualTo("ISIN22222222"));
        }

    
    }
}