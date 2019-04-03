using System.Collections.Generic;

namespace MobilePay.Domain
{
    public class DiscountService : IDiscountService
    {
        // obviously these constants should not be here. It would be better to read them from a file or some other source...
        private Dictionary<string, int> _merchantDiscountPercentages = new Dictionary<string, int>
        {
            { "TELIA", 10 }
        };

        public int? GetDiscountPercentage(string merchantName)
        {
            if (_merchantDiscountPercentages.ContainsKey(merchantName))
            {
                return _merchantDiscountPercentages[merchantName];
            }

            return null;
        }
    }
}
