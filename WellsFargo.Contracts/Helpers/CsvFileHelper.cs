using CsvHelper;
using System.Globalization;
using WellsFargo.Contracts.Exceptions;
using WellsFargo.Contracts.Models;
using System.Linq.Expressions;
using CsvHelper.Configuration;
using System.Reflection;

namespace WellsFargo.Contracts.Helpers
{
    public class CsvFileHelper : ICsvFileHelper
    {
        public List<TransactionRequestModel> ReadAndValidate(byte[] fileData)
        {
            try
            {
                var transactions = new List<TransactionRequestModel>();
                using (var ms = new MemoryStream(fileData))
                {
                    using (var reader = new StreamReader(ms, true))
                    using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                    {
                        transactions = csv.GetRecords<TransactionRequestModel>().ToList();

                    }
                }

                return transactions;
            }
            catch (Exception ex)
            {

                throw new InvalidFileException("Invalid File, Please upload a csv file with the columns names : SecurityId, PortfolioId, Nominal, OMS, TransactionType");
            }

        }

        public byte[] ExportToCsvData(OmsTypeResponseModel omsTypeResponseModel)
        {
            var csvConfiguration = new CsvConfiguration(CultureInfo.InvariantCulture);
            csvConfiguration.Delimiter = omsTypeResponseModel.Delimiter;

            using (var memoryStream = new MemoryStream())
            {
                using (var streamWriter = new StreamWriter(memoryStream))
                using (var csvWriter = new CsvWriter(streamWriter, csvConfiguration))
                {
                    CreateCsvData(omsTypeResponseModel, csvWriter);

                } // StreamWriter gets flushed here.

                return memoryStream.ToArray();
            }
        }

        private static void CreateCsvData(OmsTypeResponseModel omsTypeResponseModel, CsvWriter csvWriter)
        {
            var isHeaderCreated = false;

            foreach (var omsData in omsTypeResponseModel.OmsData)
            {
                var properties = omsData.GetType().GetProperties();

                if (omsTypeResponseModel.IsHeadersRequired && !isHeaderCreated)
                {
                    CreateCsvHeader(csvWriter, properties);
                    csvWriter.NextRecord();
                    isHeaderCreated = true;
                }

                CreateCsvRow(csvWriter, omsData, properties);
                csvWriter.NextRecord();
            }
        }

        private static void CreateCsvHeader(CsvWriter csvWriter, PropertyInfo[] properties)
        {
            foreach (var property in properties)
            {
                csvWriter.WriteField(property.Name);
            }
        }

        private static void CreateCsvRow(CsvWriter csvWriter, OmsResponseModel omsData, PropertyInfo[] properties)
        {
            foreach (var prop in properties)
            {
                var value = prop.GetValue(omsData, null);
                csvWriter.WriteField(value);
            }
        }
    }
}
