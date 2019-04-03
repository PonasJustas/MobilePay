using System.Collections.Generic;

namespace MobilePay.Contracts
{
    public class Merchant
    {
        public Merchant(string name)
        {
            Name = name;
            Transactions = new List<Transaction>();
        }

        public string Name { get; }

        public List<Transaction> Transactions { get; }
        
        public override bool Equals(object obj)
        {
            var merchant = (Merchant) obj;
            return Name == merchant.Name;
        }
        
        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}