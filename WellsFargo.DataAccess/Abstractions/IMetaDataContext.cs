using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WellsFargo.Domain;

namespace WellsFargo.DataAccess.Abstractions
{
    public interface IMetaDataContext
    {
        List<Portfolio> GetPortfolios();
        List<Security> GetSecurities();


    }
}
