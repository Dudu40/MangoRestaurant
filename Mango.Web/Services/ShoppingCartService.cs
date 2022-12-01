using Mango.Web;
using Mango.Web.Model;
using Mango.Web.Models;
using Mango.Web.Services.IServices;
using System.Reflection.PortableExecutable;

namespace Mango.Web.Services
{
    public class ShoppingCartService : BaseService, IShoppingCartService
    {
        public ShoppingCartService(IHttpClientFactory httpClient) : base(httpClient)
        {
        }

        public async Task<T> GetAllDetailsByIdUserAsync<T>(string idUser)
        {
            return await SendRequest<T>(new RequestModel()
            {
                ApiType = Utils.ApiType.GET,
                Url = Utils.APIBase + "api/shoppingCart/getAllDetails/"+idUser
            });
        }

        public async Task<T> RemoveItemAsync<T>(int id)
        {
            return await SendRequest<T>(new RequestModel()
            {
                ApiType = Utils.ApiType.DELETE,
                Url = Utils.APIBase + "api/shoppingCart/removeItem/"+ id
            });
        }

        public async Task<T> GetCartbyProductIdAsync<T>(int id)
        {
            return await SendRequest<T>(new RequestModel()
            {
                ApiType = Utils.ApiType.GET,
                Url = Utils.APIBase + "api/shoppingCart/getCartbyProductId/" + id
            });
        }
        public async Task<T> EditItemAsync<T>(CartDetailModel cartDetail)
        {
            return await SendRequest<T>(new RequestModel()
            {
                ApiType = Utils.ApiType.PUT,
                Url = Utils.APIBase + "api/shoppingCart/editItem",
                Data = cartDetail
            });
        }

        public async Task<T> GetHeaderByIdUserAsync<T>(string idUser)
        {
            return await SendRequest<T>(new RequestModel()
            {
                ApiType = Utils.ApiType.GET,
                Url = Utils.APIBase + "api/shoppingCart/getHeaderByIdUser/" + idUser
            });
        }

        public async Task<T> ApplyCouponAsync<T>(CartHeaderModel CartHeader)
        {
            return await SendRequest<T>(new RequestModel()
            {
                ApiType = Utils.ApiType.POST,
                Url = Utils.APIBase + "api/shoppingCart/applyCoupon",
                Data= CartHeader
            });
        }

        public async Task<T> RemoveCouponAsync<T>(string UserId)
        {
            return await SendRequest<T>(new RequestModel()
            {
                ApiType = Utils.ApiType.POST,
                Url = Utils.APIBase + "api/shoppingCart/removeCoupon",
                Data = UserId
            });
        }

    }
}
