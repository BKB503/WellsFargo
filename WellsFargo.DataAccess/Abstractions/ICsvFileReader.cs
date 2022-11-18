using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WellsFargo.DataAccess.Abstractions
{
    public interface ICsvFileReader<T> where T : class
    {
        List<T> GetData(string filePath);
    }
}
