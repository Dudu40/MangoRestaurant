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

        public async Task<T> AddProductAsync<T>(ProductModel product)
        {
            return await SendRequest<T>(new RequestModel()
            {
                ApiType = Utils.ApiType.POST,
                Url = Utils.APIBase + "api/products/add" ,
                Data = product,
            });
        }

        public async Task<T> DeleteProductAsync<T>(int id)
        {
            return await SendRequest<T>(new RequestModel()
            {
                ApiType = Utils.ApiType.DELETE,
                Url = Utils.APIBase + "api/products/delete/" + id
            });
        }

        public async Task<T> GetProductByIdAsync<T>(int id)
        {
            return await SendRequest<T>(new RequestModel()
            {
                ApiType = Utils.ApiType.GET,
                Url = Utils.APIBase + "api/products/getById/"+ id
            });
        }

        public async Task<T> GetProductsAsync<T>()
        {
            return await SendRequest<T>(new RequestModel()
            {
                ApiType = Utils.ApiType.GET,
                Url = Utils.APIBase + "api/products/getAll"
            });
        }

        public async Task<T> UpdateProductAsync<T>(ProductModel product)
        {
            return await SendRequest<T>(new RequestModel()
            {
                ApiType = Utils.ApiType.PUT,
                Url = Utils.APIBase + "api/products/update",
                Data = product
            });
        }

        public async Task<T> AddToShoppingCartAsync<T>(CartDetailModel productDetail)
        {
            return await SendRequest<T>(new RequestModel()
            {
                ApiType = Utils.ApiType.POST,
                Url = Utils.APIBase + "api/products/addToShoppingCart",
                Data = productDetail
            }); ;
        }
    }
}
