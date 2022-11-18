using WellsFargo.DataAccess.Abstractions;

namespace WellsFargo.DataAccess
{
    public class TemplateConfigurationProvider : ITemplateConfigurationProvider
    {
        private string AppDirectoryPath { get; }
        public TemplateConfigurationProvider(string appDirectoryPath)
        {
            AppDirectoryPath= appDirectoryPath;
        }

        public string GetPortfolioFilePath => $"{AppDirectoryPath}/MetadataCsvFiles/portfolios.csv";

        public string GetSecurityFilePath => $"{AppDirectoryPath}/MetadataCsvFiles/securities.csv";
    }
}
