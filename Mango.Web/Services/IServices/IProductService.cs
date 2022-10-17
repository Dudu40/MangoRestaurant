
using Mango.Web.Model;

namespace Mango.Web.Services.IServices
{
    public interface IProductService
    {
        // all methods send a request by the methode sendRequest
        Task<T> GetProductsAsynch<T>();
        Task<T> GetProductByIdAsynch<T>(int id);
        Task<T> AddProductAsynch<T>(ProductModel product);
        Task<T> UpdateProductAsynch<T>(ProductModel product);
        Task<T> DeleteProductAsynch<T>(int id);
    }
}
