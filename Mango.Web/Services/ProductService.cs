using Mango.Web;
using Mango.Web.Model;
using Mango.Web.Models;
using Mango.Web.Services.IServices;

namespace Mango.Web.Services
{
    public class ProductService : BaseService, IProductService
    {
        public ProductService(IHttpClientFactory httpClient) : base(httpClient)
        {
        }

        public async Task<T> AddProductAsynch<T>(ProductModel product)
        {
            return await SendRequest<T>(new RequestModel()
            {
                ApiType = Utils.ApiType.POST,
                Url = Utils.ProductAPIBase + "api/products" ,
                Data = product,
            });
        }

        public async Task<T> DeleteProductAsynch<T>(int id)
        {
            return await SendRequest<T>(new RequestModel()
            {
                ApiType = Utils.ApiType.DELETE,
                Url = Utils.ProductAPIBase + "api/products/" + id
            });
        }

        public async Task<T> GetProductByIdAsynch<T>(int id)
        {
            return await SendRequest<T>(new RequestModel()
            {
                ApiType = Utils.ApiType.GET,
                Url = Utils.ProductAPIBase + "api/products/"+ id
            });
        }

        public async Task<T> GetProductsAsynch<T>()
        {
            return await SendRequest<T>(new RequestModel()
            {
                ApiType = Utils.ApiType.GET,
                Url = Utils.ProductAPIBase + "api/products"
            });
        }

        public async Task<T> UpdateProductAsynch<T>(ProductModel product)
        {
            return await SendRequest<T>(new RequestModel()
            {
                ApiType = Utils.ApiType.PUT,
                Url = Utils.ProductAPIBase + "api/products",
                Data = product
            });
        }
    }
}
