
using Mango.Web.Model;
using Mango.Web.Models;

namespace Mango.Web.Services.IServices
{
    public interface IProductService
    {
        // all methods send a request by the methode sendRequest
        Task<T> GetProductsAsync<T>();
        Task<T> GetProductByIdAsync<T>(int id);
        Task<T> AddProductAsync<T>(ProductModel product);
        Task<T> UpdateProductAsync<T>(ProductModel product);
        Task<T> DeleteProductAsync<T>(int id);

        Task<T> AddToShoppingCartAsync<T>(CartDetailModel product);

    }
}
