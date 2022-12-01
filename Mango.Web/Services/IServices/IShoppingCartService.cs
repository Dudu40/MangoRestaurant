
using Mango.Web.Models;

namespace Mango.Web.Services.IServices
{
    public interface IShoppingCartService
    {
        Task<T> GetAllDetailsByIdUserAsync<T>(string idUser);

        Task<T> RemoveItemAsync<T>(int id);

        Task<T> GetCartbyProductIdAsync<T>(int id);

        Task<T> EditItemAsync<T>(CartDetailModel cartDetail);

        Task<T> GetHeaderByIdUserAsync<T>(string idUser);

        Task<T> ApplyCouponAsync<T>(CartHeaderModel CartHeader);

        Task<T> RemoveCouponAsync<T>(string UserId);
    }
}