using MobilePay.Domain;
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
        }

        [TestCaseSource(nameof(TestCases))]
        public int? TestCalculateFee(string merchantName)
        {
            return _discountService.GetDiscountPercentage(merchantName);
        }
        
        public static IEnumerable TestCases
        {
            get
            {
                yield return new TestCaseData("TELIA")
                    .Returns(10);
                yield return new TestCaseData("CIRCLE_K")
                    .Returns(null);
                yield return new TestCaseData("NETTO")
                    .Returns(null);
            }
        }
    }
}
