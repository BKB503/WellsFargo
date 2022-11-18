using WellsFargo.Contracts.Models;

namespace WellsFargo.TestUtils
{
    public static class TransactionFactory
    {
        public static List<TransactionRequestModel> BuildTransactionRequestModel(string omsType)
        {
            var transactions = new List<TransactionRequestModel>();
            for (int i = 1; i <= 10; i++)
            {
                var transaction = new TransactionRequestModel
                {
                    OMS = omsType,
                    Nominal = i,
                    PortfolioId = i,
                    SecurityId = i,
                    TransactionType = "Buy"

                };
                transactions.Add(transaction);
            }
            return transactions;
        }
    }
}