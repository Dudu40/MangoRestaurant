
using Mango.Services.API.Models;

namespace Mango.Services.API.Repository
{

    public interface IProductRepository
    {
        // Asynch List of productModel
        Task<ProductModel> AddProduct(ProductModel productModel);
        Task<ProductModel> UpdateProduct(ProductModel productModel);
        Task<bool> DeleteProduct(int productId);
        Task<ProductModel> GetProductById(int productId);
        Task<IEnumerable<ProductModel>> GetProducts();
        Task<CartDetailModel> AddToShoppingCart(CartDetailModel cartDetail);




    }
}
