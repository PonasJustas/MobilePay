namespace MobilePay.Domain
{
    public interface IDiscountService
    {
        int? GetDiscountPercentage(string merchantName);
    }
}