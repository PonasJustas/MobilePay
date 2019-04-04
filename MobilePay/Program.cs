using MobilePay.Contracts;
using MobilePay.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace MobilePay
{
    class Program
    {
        private static IDiscountService _discountService;
        private static IFeeCalculationService _feeCalculationService;
        private static List<Merchant> _merchants = new List<Merchant>();

        static void Main(string[] args)
        {
            _discountService = new DiscountService();
            _feeCalculationService = new FeeCalculationService(_discountService);

            _discountService.AddOrUpdateDiscount("TELIA", 10);
            _discountService.AddOrUpdateDiscount("CIRCLE_K", 20);

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

            var merchant = _merchants.FirstOrDefault(x => x.Name == transactionDetails[1]);

            if (merchant == null)
            {
                merchant = new Merchant(transactionDetails[1]);
                _merchants.Add(merchant);
            }
               
            return new Transaction(
                DateTime.ParseExact(transactionDetails[0], "yyyy-MM-dd", CultureInfo.InvariantCulture),
                decimal.Parse(transactionDetails[2], NumberStyles.AllowDecimalPoint),
                merchant);
        }
    }
}
