using Moq;
using WellsFargo.Core.Abstractions;
using WellsFargo.Core.Strategy;

namespace WellsFargo.Core.Tests.Strategy
{
    public class OmsDataExtractResolverTests
    {
        private Mock<IOmsDataExtractStrategy> omsTypeAAA;
        private Mock<IOmsDataExtractStrategy> someOtherStrategy;

        [SetUp]
        public void SetUp()
        {
            omsTypeAAA = new Mock<IOmsDataExtractStrategy>();
            someOtherStrategy = new Mock<IOmsDataExtractStrategy>();
        }


        [Test]
        public void Resolve_ForAOmsType_ShouldReturnTheCorrespondingStrategy()
        {
            omsTypeAAA.Setup(a => a.CanMap("AAA")).Returns(true);

            var dataExtractStrategy = new List<IOmsDataExtractStrategy>
            {
                omsTypeAAA.Object,
                someOtherStrategy.Object,
            };

            var dataExtractResolver = new OmsDataExtractResolver(dataExtractStrategy);

            var actualDataExtractStrategy = dataExtractResolver.Resolve("AAA");

            omsTypeAAA.Verify(a => a.CanMap("AAA"), Times.Once);
            someOtherStrategy.Verify(a => a.CanMap("AAA"), Times.Once);
            Assert.That(actualDataExtractStrategy, Is.EqualTo(omsTypeAAA.Object));
        }
    }
}
