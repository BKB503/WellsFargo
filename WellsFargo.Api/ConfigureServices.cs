using WellsFargo.Contracts.Helpers;
using WellsFargo.Core.Abstractions;
using WellsFargo.Core.Services;
using WellsFargo.Core.Strategy;
using WellsFargo.DataAccess;
using WellsFargo.DataAccess.Abstractions;

namespace WellsFargo.Api
{
    public static class ConfigureServices
    {
        public static void AddHelperServices(this IServiceCollection services)
        {
            services.AddTransient<ICsvFileHelper, CsvFileHelper>();
        }

        public static void AdddDataAccessServices(this IServiceCollection services)
        {
            //services.AddTransient<ICsvFileReader, CsvFileReader>();
            services.AddTransient<IPortfolioRepository, PortfolioRepository>();
            services.AddTransient<ISecurityRepository, SecurityRepository>();
            services.AddTransient<IMetaDataContext, MetaDataContext>();
            services.AddTransient<ITemplateConfigurationProvider, TemplateConfigurationProvider>(c =>
            {
                var appPath = AppContext.BaseDirectory;
                return new TemplateConfigurationProvider(appPath);
            });
        }

        public static void AddCoreServices(this IServiceCollection services)
        {
            services.AddTransient<IOmsDataExtractResolver, OmsDataExtractResolver>();
            services.AddTransient<IOmsDataExtractStrategy, OmsAAADataExtract>();
            services.AddTransient<IOmsDataExtractStrategy, OmsBBBDataExtract>();
            services.AddTransient<IOmsDataExtractStrategy, OmsCCCDataExtract>();
            services.AddTransient<IOrderManagementSystemService, OrderManagementSystemService>();
            //services.AddTransient<IOmsDataExtractStrategyContext, OmsDataExtractStrategyContext>();
        }
    }
}
