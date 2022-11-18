using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WellsFargo.DataAccess.Abstractions
{
    public interface ITemplateConfigurationProvider
    {
        string GetPortfolioFilePath { get; }
        string GetSecurityFilePath { get; }
    }
}
