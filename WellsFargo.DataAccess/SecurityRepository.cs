using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WellsFargo.DataAccess.Abstractions;
using WellsFargo.Domain;

namespace WellsFargo.DataAccess
{
    public class SecurityRepository : CsvFileReader<Security>, ISecurityRepository
    {
       
    }
}
