using Mango.Services.API.Models;

namespace Mango.Services.API.Repository
{
    public interface ICouponRepository
    {
        Task <CouponModel> GetCouponByCode(string CouponCode);
    }
}
