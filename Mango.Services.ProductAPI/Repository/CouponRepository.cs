using AutoMapper;
using Mango.Services.API;
using Mango.Services.API.Entities;
using Mango.Services.API.Models;
using Mango.Services.API.Repository;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.API.Repository
{
    public class CouponRepository : ICouponRepository
    {
        private readonly ApplicationDbContext _db;
        private IMapper _mapper;
        public CouponRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public async Task<CouponModel> GetCouponByCode(string CouponCode)
        {
            Coupon coupon = await _db.Coupons.Where(c => c.Code == CouponCode).FirstOrDefaultAsync();
            return _mapper.Map<CouponModel>(coupon);
        }
    }
}
