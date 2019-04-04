using System;
using System.Collections.Generic;

namespace MobilePay.Services
{
    public class DiscountService : IDiscountService
    {
        // obviously these constants should not be here. It would be better to read them from a file or some other source...
        private Dictionary<string, int> _merchantDiscountPercentages = new Dictionary<string, int>();

        public void AddOrUpdateDiscount(string merchantName, int discountPercentage)
        {
            if (discountPercentage < 0)
            {
                throw new Exception("discount cannot be less than zero.");
            }

            if (discountPercentage == 0)
            {
                if (_merchantDiscountPercentages.ContainsKey(merchantName))
                {
                    _merchantDiscountPercentages.Remove(merchantName);
                }
            }
            else
            {
                _merchantDiscountPercentages[merchantName] = discountPercentage;
            }
        }

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
 