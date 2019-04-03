using MobilePay.Contracts;
using MobilePay.Domain;
using System;
using System.Globalization;
using System.IO;
using System.Linq;

namespace MobilePay
{
    class Program
    {
        private static IFeeCalculationService _feeCalculationService = new FeeCalculationService(new DiscountService());

        static void Main(string[] args)
        {

            var transactionsInput = File.ReadLines("transactions.txt");
            var transactions = transactionsInput.Where(x => !string.IsNullOrWhiteSpace(x)).Select(ParseTransaction).ToList();
            
            var transactionFees = _feeCalculationService.CalculateFees(transactions);
            foreach (var transactionFee in transactionFees)
            {
                Console.WriteLine(transactionFee.ToString());
            }

            Console.ReadKey();
        }

        private static Transaction ParseTransaction(string transaction)
        {
            var transactionDetails = transaction.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            return new Transaction(
                DateTime.ParseExact(transactionDetails[0], "yyyy-MM-dd", CultureInfo.InvariantCulture),
                decimal.Parse(transactionDetails[2], NumberStyles.AllowDecimalPoint),
                new Merchant(transactionDetails[1]));
        }
    }
}
