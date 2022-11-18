using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WellsFargo.Contracts.Models;

namespace WellsFargo.Core.Abstractions
{
    public interface IOrderManagementSystemService
    {
        List<OmsTypeResponseModel> CreateOmsTransactions(List<TransactionRequestModel> transactionRequestModels);
    }
}
