using MobilePay.Contracts;
using MobilePay.Services;
using MobilePay.Tests.Mockers;
using NUnit.Framework;
using System;
using System.Collections;

namespace MobilePay.Tests
{
    public class FeeCalculationServiceTests
    {
        private IFeeCalculationService _feeCalculationService;

        [SetUp]
        public void Setup()
        {
            var mocker = new DiscountServiceMocker();
            _feeCalculationService = new FeeCalculationService(mocker.DiscountService);

            mocker.DiscountService.AddOrUpdateDiscount("TELIA", 10);
            mocker.DiscountService.AddOrUpdateDiscount("CIRCLE_K", 20);
        }

        [TestCaseSource(nameof(CalculateFeeTestCases))]
        public TransactionFee TestCalculateFee(Transaction transaction)
        {
            return _feeCalculationService.CalculateFee(transaction);
        }

        // Testing return of whole TransactionFee object and not only Fee property
        // because output file has to contain information from transaction and merchant objects.
        public static IEnumerable CalculateFeeTestCases
        {
            get
            {
                var circleKMerchant = new Merchant("CIRCLE_K");
                var teliaMerchant = new Merchant("TELIA");

                var transaction = new Transaction(new DateTime(2018, 9, 2), 120, circleKMerchant);
                yield return new TestCaseData(transaction)
                    .Returns(new TransactionFee(transaction, 29.96M));

                transaction = new Transaction(new DateTime(2018, 9, 4), 200, teliaMerchant);
                yield return new TestCaseData(transaction)
                    .Returns(new TransactionFee(transaction, 30.80M));

                transaction = new Transaction(new DateTime(2018, 10, 22), 300, circleKMerchant);
                yield return new TestCaseData(transaction)
                    .Returns(new TransactionFee(transaction, 31.40M));

                transaction = new Transaction(new DateTime(2018, 10, 29), 150, circleKMerchant);
                yield return new TestCaseData(transaction)
                    .Returns(new TransactionFee(transaction, 1.20M));

                //yield return new TestCaseData( 0, 0 )
                //    .Throws(typeof(DivideByZeroException))
                //    .SetName("DivideByZero")
                //    .SetDescription("An exception is expected");
            }
        }
    }
}