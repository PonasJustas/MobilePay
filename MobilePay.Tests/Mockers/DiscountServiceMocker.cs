using MobilePay.Services;
using Moq;
using System.Collections.Generic;

namespace MobilePay.Tests.Mockers
{
    public class DiscountServiceMocker
    {
        private Dictionary<string, int> _merchantDiscountPercentages = new Dictionary<string, int>();

        public DiscountServiceMocker()
        {
            MockDiscountService = new Mock<IDiscountService>();
            MockDiscountService.Setup(x => x.AddOrUpdateDiscount(It.IsAny<string>(), It.IsAny<int>()))
                .Callback((string merchantName, int discountPercentage) =>
                    _merchantDiscountPercentages[merchantName] = discountPercentage);

            MockDiscountService.Setup(x => x.GetDiscountPercentage(It.IsAny<string>()))
                .Returns((string merchantName) =>
                {
                    if (_merchantDiscountPercentages.ContainsKey(merchantName))
                    {
                        return _merchantDiscountPercentages[merchantName];
                    }

                    return null;
                });
        }

        public Mock<IDiscountService> MockDiscountService { get; }

        public IDiscountService DiscountService => MockDiscountService.Object;
    }
}
