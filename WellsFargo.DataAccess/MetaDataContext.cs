using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WellsFargo.DataAccess.Abstractions;
using WellsFargo.Domain;

namespace WellsFargo.DataAccess
{
    public class MetaDataContext : IMetaDataContext
    {
        private readonly IPortfolioRepository portfolioRepository;
        private readonly ISecurityRepository securityRepository;
        private readonly ITemplateConfigurationProvider templateConfigurationProvider;
        public MetaDataContext(IPortfolioRepository portfolioRepository, ISecurityRepository securityRepository, ITemplateConfigurationProvider templateConfigurationProvider)
        {
            this.portfolioRepository= portfolioRepository;
            this.securityRepository= securityRepository;
            this.templateConfigurationProvider = templateConfigurationProvider;
        }
       
        public List<Portfolio> GetPortfolios()
        {
            return portfolioRepository.GetData(templateConfigurationProvider.GetPortfolioFilePath).ToList();
        }

        public List<Security> GetSecurities()
        {
            return securityRepository.GetData(templateConfigurationProvider.GetSecurityFilePath).ToList();
        }
    }
}
