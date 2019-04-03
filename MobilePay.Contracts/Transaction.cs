using System;
using System.Linq;

namespace MobilePay.Contracts
{
    public class Transaction
    {
        public Transaction(DateTime date, decimal amount, Merchant merchant)
        {
            TransactionGuid = Guid.NewGuid();

            Date = date;
            Amount = amount;
            Merchant = merchant;

            Merchant.Transactions.Add(this);
        }

        public Guid TransactionGuid { get; }
        public DateTime Date { get; }
        public decimal Amount { get; }
        public Merchant Merchant { get; }

        public bool IsFirstTransactionOfTheMonth => Merchant.Transactions
            .FirstOrDefault(x => x.Date.Month == Date.Month && x.Date.Year == Date.Year)?.Equals(this) ?? false;

        public override bool Equals(object obj)
        {
            var transaction = (Transaction) obj;
            return TransactionGuid == transaction.TransactionGuid && Date == transaction.Date &&
                   Amount == transaction.Amount && Merchant.Equals(transaction.Merchant);
        }
        
        public override int GetHashCode()
        {
            return HashCode.Combine(TransactionGuid, Date, Amount, Merchant);
        }
    }
}
