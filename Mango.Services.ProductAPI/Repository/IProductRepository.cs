using Mango.Services.ProductAPI.Model;

namespace Mango.Services.ProductAPI.Repository
{

    public interface IProductRepository
    {
        // Asynch List of productModel
        Task<IEnumerable<ProductModel>> GetProducts();
        Task<ProductModel> GetProductById(int productId);
        Task<ProductModel> AddProduct(ProductModel productModel);
        Task<ProductModel> UpdateProduct(ProductModel productModel);
        Task<bool> DeleteProduct(int productId);
    }
}
