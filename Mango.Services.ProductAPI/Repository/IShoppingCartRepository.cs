
using Mango.Services.API.Models;

namespace Mango.Services.API.Repository
{

    public interface IShoppingCartRepository
    {
        // Asynch List of productModel
        Task<List<CartDetailModel>> GetAllDetailsByIdUser(string idUser);

        Task<bool> RemoveItem(int id);

        Task<CartDetailModel> GetCartbyProductId(int id);

        Task<CartDetailModel> EditItem(CartDetailModel cartDetail);
        Task<CartHeaderModel> GetHeaderByIdUser(string idUser);
        Task<bool> ApplyCoupon(CartHeaderModel cartHeader);
        Task<bool> RemoveCoupon(string UserId);

    }
}
