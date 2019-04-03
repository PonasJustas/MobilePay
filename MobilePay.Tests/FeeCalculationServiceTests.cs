using MobilePay.Contracts;
using MobilePay.Domain;
using NUnit.Framework;
using System;
using System.Collections;

namespace Tests
{
    public class FeeCalculationServiceTests
    {
        private IFeeCalculationService _feeCalculationService;

        [SetUp]
        public void Setup()
        {
            _feeCalculationService = new FeeCalculationService();
        }

        [TestCaseSource(nameof(TestCases))]
        public TransactionFee TestCalculateFee(Transaction transaction)
        {
            return _feeCalculationService.CalculateFee(transaction);
        }

        // Testing return of whole TransactionFee object and not only Fee property
        // because output file has to contain information from transaction and merchant objects.
        public static IEnumerable TestCases
        {
            get
            {
                yield return new TestCaseData(new Transaction(new DateTime(2018, 9, 2), 120, new Merchant("CIRCLE_K")))
                    .Returns(new TransactionFee(
                        new Transaction(new DateTime(2018, 9, 2), 120, new Merchant("CIRCLE_K")),
                        1.20M));
                yield return new TestCaseData(new Transaction(new DateTime(2018, 9, 4), 200, new Merchant("TELIA")))
                    .Returns(new TransactionFee(new Transaction(new DateTime(2018, 9, 4), 200, new Merchant("TELIA")),
                        2.00M));
                yield return new TestCaseData(new Transaction(new DateTime(2018, 10, 22), 300, new Merchant("CIRCLE_K")))
                    .Returns(new TransactionFee(new Transaction(new DateTime(2018, 10, 22), 300, new Merchant("CIRCLE_K")),
                        3.00M));
                yield return new TestCaseData(new Transaction(new DateTime(2018, 10, 29), 150, new Merchant("CIRCLE_K")))
                    .Returns(new TransactionFee(new Transaction(new DateTime(2018, 10, 29), 150, new Merchant("CIRCLE_K")),
                        1.50M));
                //yield return new TestCaseData( 0, 0 )
                //    .Throws(typeof(DivideByZeroException))
                //    .SetName("DivideByZero")
                //    .SetDescription("An exception is expected");
            }
        }
    }
}