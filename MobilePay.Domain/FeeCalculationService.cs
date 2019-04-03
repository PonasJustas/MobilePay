using MobilePay.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MobilePay.Domain
{
    public class FeeCalculationService : IFeeCalculationService
    {
        private const int FEE_PERCENTAGE = 1;
        private const decimal INVOICE_FEE = 29;

        private readonly IDiscountService _discountService;

        public FeeCalculationService(IDiscountService discountService)
        {
            _discountService = discountService;
        }

        public List<TransactionFee> CalculateFees(List<Transaction> transactions)
        {
            return transactions.Select(CalculateFee).ToList();
        }

        public TransactionFee CalculateFee(Transaction transaction)
        {
            var discount = _discountService.GetDiscountPercentage(transaction.Merchant.Name);

            var fee = transaction.Amount / 100 * FEE_PERCENTAGE;

            if (discount.HasValue)
            {
                fee -= fee / 100 * discount.Value;
            }

            if (fee > 0 && transaction.IsFirstTransactionOfTheMonth)
            {
                fee += INVOICE_FEE;
            }

            return new TransactionFee(transaction, Math.Round(fee, 2));
        }
    }
}
