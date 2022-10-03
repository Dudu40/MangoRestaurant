using AutoMapper;
using Mango.Services.ProductAPI.Entities;
using Mango.Services.ProductAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.ProductAPI.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _db;
        private IMapper _mapper;

        public ProductRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<ProductModel> AddProduct(ProductModel productModel)
        {
            // map productDto to product
            Product product = _mapper.Map<Product>(productModel);
            // add the product to db
            _db.Products.Add(product);
            //save changes
            await _db.SaveChangesAsync();
            // return product Model 
            return _mapper.Map<ProductModel>(product);
        }
        public async Task<ProductModel> UpdateProduct(ProductModel productModel)
        {
            // map productDto to product
            Product product = _mapper.Map<Product>(productModel);
            // add the product to db
            _db.Products.Update(product);
            //save changes
            await _db.SaveChangesAsync();
            // return product Model 
            return _mapper.Map<ProductModel>(product);
        }


        public async Task<bool> DeleteProduct(int productId)
        {
            try
            {
                Product product = await _db.Products.Where(p => p.ProductId == productId).FirstOrDefaultAsync();
                if (product == null)
                {
                    return false;
                }
                _db.Products.Remove(product);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<ProductModel> GetProductById(int productId)
        {
            Product product = await _db.Products.Where(p => p.ProductId == productId).FirstOrDefaultAsync();
            return _mapper.Map<ProductModel>(product);
        }

        public async Task<IEnumerable<ProductModel>> GetProducts()
        {
            List<Product> productList = await _db.Products.ToListAsync();
            return _mapper.Map<List<ProductModel>>(productList);
        }
    }
}
