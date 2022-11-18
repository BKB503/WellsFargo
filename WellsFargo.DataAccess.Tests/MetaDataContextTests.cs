using Moq;
using WellsFargo.DataAccess.Abstractions;
using WellsFargo.Domain;
using WellsFargo.TestUtils;

namespace WellsFargo.DataAccess.Tests
{
    public class MetaDataContextTests
    {
        private MetaDataContext subject;
        private List<Security> mockSecurities = SecurityFactory.BuildSecurities();
        private List<Portfolio> mockPortfolios = PortfolioFactory.BuildPortfolios();
        private Mock<IPortfolioRepository> portfolioRepository;
        private Mock<ISecurityRepository> securityRepository;
        private Mock<ITemplateConfigurationProvider> templateConfigurationProvider;
        private readonly string filePath = "filePath";

        [SetUp]
        public void Setup()
        {
            portfolioRepository = new Mock<IPortfolioRepository>();
            securityRepository = new Mock<ISecurityRepository>();
            templateConfigurationProvider = new Mock<ITemplateConfigurationProvider>();

            portfolioRepository.Setup(p => p.GetData(filePath)).Returns(mockPortfolios);
            securityRepository.Setup(p => p.GetData(filePath)).Returns(mockSecurities);
            templateConfigurationProvider.Setup(x => x.GetPortfolioFilePath).Returns(filePath);
            templateConfigurationProvider.Setup(x => x.GetSecurityFilePath).Returns(filePath);

            subject = new MetaDataContext(portfolioRepository.Object, securityRepository.Object, templateConfigurationProvider.Object);
        }

        [Test]
        public void ImplementsContract()
        {
            Assert.That(subject, Is.InstanceOf(typeof(IMetaDataContext)));
        }


        [Test]
        public void WhenGetSecurities_ThenMustCallSecurityRepository_GetData()
        {
            var result = subject.GetSecurities();

            securityRepository.Verify(x => x.GetData(filePath), Times.Once);
            templateConfigurationProvider.Verify(x => x.GetSecurityFilePath, Times.Once);
            Assert.That(result.Count, Is.EqualTo(mockSecurities.Count));

        }

        [Test]
        public void WhenGetPortfolios_ThenMustCallPortfolioRepository_GetData()
        {
            var result = subject.GetPortfolios();

            portfolioRepository.Verify(x => x.GetData(filePath), Times.Once);
            templateConfigurationProvider.Verify(x => x.GetPortfolioFilePath, Times.Once);  
            Assert.That(result.Count, Is.EqualTo(mockPortfolios.Count));

        }


    }
}