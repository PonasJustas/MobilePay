using MobilePay.Services;
using NUnit.Framework;
using System.Collections;

namespace MobilePay.Tests
{
    public class DiscountServiceTests
    {
        private IDiscountService _discountService;

        [SetUp]
        public void Setup()
        {
            _discountService = new DiscountService();
            _discountService.AddOrUpdateDiscount("TELIA", 10);
            _discountService.AddOrUpdateDiscount("CIRCLE_K", 20);
        }

        [TestCaseSource(nameof(GetDisountPercentagesTestCases))]
        public int? TestGetDiscountPercentage(string merchantName)
        {
            return _discountService.GetDiscountPercentage(merchantName);
        }
        
        public static IEnumerable GetDisountPercentagesTestCases
        {
            get
            {
                yield return new TestCaseData("TELIA")
                    .Returns(10);
                yield return new TestCaseData("CIRCLE_K")
                    .Returns(20);
                yield return new TestCaseData("NETTO")
                    .Returns(null);
            }
        }
    }
}
