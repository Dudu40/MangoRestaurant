using Mango.Web.Services.IServices;
using Mango.Web.Models;

namespace Mango.Web.Services
{
    public class CouponService : BaseService, ICouponService
    {
        public CouponService(IHttpClientFactory httpClient) : base(httpClient)
        {
        }

        public async Task<T> GetCouponByCodeAsync<T>(string Code)
        {       
            return await SendRequest<T>(new RequestModel()
            {
                ApiType = Utils.ApiType.GET,
                Url = Utils.APIBase + "api/coupons/getCouponByCode/"+Code
            });
        }
        


    }
}
