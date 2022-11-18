using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WellsFargo.Core.Abstractions;

namespace WellsFargo.Core.Strategy
{
    public class OmsDataExtractResolver : IOmsDataExtractResolver
    {
        private readonly IEnumerable<IOmsDataExtractStrategy> omsDataExtractStrategies;

        public OmsDataExtractResolver(IEnumerable<IOmsDataExtractStrategy> omsDataExtractStrategies)
        {
            this.omsDataExtractStrategies = omsDataExtractStrategies;
        }

        public IOmsDataExtractStrategy Resolve(string omsType)
        {
            return omsDataExtractStrategies.Single(s => s.CanMap(omsType));
        }
    }
}
