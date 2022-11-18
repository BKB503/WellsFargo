using WellsFargo.DataAccess.Abstractions;
using WellsFargo.Domain;

namespace WellsFargo.DataAccess.Tests
{
    public class TemplateConfigurationProviderTests
    {
        private TemplateConfigurationProvider subject;
        private readonly string basePath = "basePath";

        [SetUp]
        public void Setup()
        {
            subject = new TemplateConfigurationProvider(basePath);
        }

        [Test]
        public void ImplementsContract()
        {
            Assert.That(subject, Is.InstanceOf(typeof(ITemplateConfigurationProvider)));
        }

        
        [Test]
        public void WhenGetPortfolioFilePath_ThenReturnFullFilePathOfFile()
        {   
            var result = subject.GetPortfolioFilePath;
            
            Assert.That(result, Is.EqualTo($"{basePath}/MetadataCsvFiles/portfolios.csv"));
            
        }

        [Test]
        public void WhenGetSecurityFilePath_ThenReturnFullFilePathOfFile()
        {
            var result = subject.GetSecurityFilePath;

            Assert.That(result, Is.EqualTo($"{basePath}/MetadataCsvFiles/securities.csv"));

        }


    }
}