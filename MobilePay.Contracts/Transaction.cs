using System;

namespace MobilePay.Contracts
{
    public class Transaction
    {
        public Transaction(DateTime date, decimal amount, Merchant merchant)
        {
            Date = date;
            Amount = amount;
            Merchant = merchant;
        }

        public DateTime Date { get; }
        public decimal Amount { get; }
        public Merchant Merchant { get; }

        public override bool Equals(object obj)
        {
            var transactionFee = (Transaction) obj;
            return Date == transactionFee.Date && Amount == transactionFee.Amount && Merchant.Equals(transactionFee.Merchant);
        }
        
        public override int GetHashCode()
        {
            return HashCode.Combine(Date, Amount, Merchant);
        }
    }
}
