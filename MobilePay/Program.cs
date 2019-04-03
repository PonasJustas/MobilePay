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
        static void Main(string[] args)
        {
            var transactionsInput = File.ReadLines("transactions.txt");

            var transactions = transactionsInput.Where(x => !string.IsNullOrWhiteSpace(x)).Select(ParseTransaction).ToList();

            var feeCalculationService = new FeeCalculationService();
            var transactionFees = feeCalculationService.CalculateFees(transactions);

            foreach (var transactionFee in transactionFees)
            {
                Console.WriteLine(transactionFee.ToString());
            }

            Console.ReadKey();

            //File.WriteAllText("transactionFees.txt",
            //    transactionFees.Select(x => x.ToString()).Aggregate((i, j) => $"{i}{Environment.NewLine}{j}"));
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
