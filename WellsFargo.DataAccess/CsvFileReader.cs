using CsvHelper;
using System.Globalization;
using WellsFargo.DataAccess.Abstractions;

namespace WellsFargo.DataAccess
{
    public abstract class CsvFileReader<T>: ICsvFileReader<T> where T : class
    {
        public List<T> GetData(string filePath)
        {
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                //csv.Context.RegisterClassMap<T>();
                var records = csv.GetRecords<T>().ToList();
                return records;
            }
        }
    }
}
