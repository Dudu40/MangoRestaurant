using AutoMapper;
using Mango.Services.ProductAPI.Entities;
using Mango.Services.ProductAPI.Model;

namespace Mango.Services.ProductAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<ProductModel, Product>();
                config.CreateMap<Product, ProductModel>();
            });
            
            return mappingConfig;
        }
    }
}
