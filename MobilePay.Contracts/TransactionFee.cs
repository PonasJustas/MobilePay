using System;

namespace MobilePay.Contracts
{
    public class TransactionFee
    {
        public TransactionFee(Transaction transaction, decimal fee)
        {
            Transaction = transaction;
            Fee = fee;
        }

        public Transaction Transaction { get;  }
        public decimal Fee { get; }

        public override bool Equals(object obj)
        {
            var transactionFee = (TransactionFee) obj;
            return Fee == transactionFee.Fee && Transaction.Equals(transactionFee.Transaction);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Transaction, Fee);
        }

        public override string ToString()
        {
            return $"{Transaction.Date:yyyy-MM-dd} {Transaction.Merchant.Name} {Fee:0.00}";
        }
    }
}
