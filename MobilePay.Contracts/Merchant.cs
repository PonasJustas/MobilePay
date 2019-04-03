namespace MobilePay.Contracts
{
    public class Merchant
    {
        public Merchant(string name)
        {
            Name = name;
        }

        public string Name { get; }
        
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