namespace MobilePay.Services
{
    public interface IDiscountService
    {
        void AddOrUpdateDiscount(string merchantName, int discountPercentage);
        int? GetDiscountPercentage(string merchantName);
    }
}