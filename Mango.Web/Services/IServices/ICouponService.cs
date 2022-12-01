namespace Mango.Web.Services.IServices
{
    public interface ICouponService
    {
        Task<T> GetCouponByCodeAsync<T>(string Code);
    }
}
