
using Mango.Services.API.Models;
using Mango.Services.API.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.API.Controllers
{
    [Route("api/coupons")]
    public class CouponAPIController : Controller
    {
        protected ResponseModel _response;
        private ICouponRepository _couponRepository;

        public CouponAPIController(ICouponRepository couponRepository)
        {
            _response = new ResponseModel();
            _couponRepository = couponRepository;
        }

        [HttpGet("getCouponByCode/{code}")]
        public async Task<object> getCouponByCode(string code)
        {
            try
            {
                CouponModel coupon = await _couponRepository.GetCouponByCode(code);
                _response.Result = coupon;

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };

            }
            return _response;
        }
    }
}
