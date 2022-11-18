using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WellsFargo.Contracts.Models;

namespace WellsFargo.Contracts.Helpers
{
    public interface ICsvFileHelper
    {
        List<TransactionRequestModel> ReadAndValidate(byte[] fileData);

        byte[] ExportToCsvData(OmsTypeResponseModel omsTypeResponseModel);
    }
}
