using AutoMapper;
using Mango.Services.API.Entities;
using Mango.Services.API.Models;

namespace Mango.Services.API
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<ProductModel, Product>().ReverseMap();
                config.CreateMap<CouponModel, Coupon>().ReverseMap();

            });
            
            return mappingConfig;
        }
    }
}
