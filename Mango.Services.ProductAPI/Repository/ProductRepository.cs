using AutoMapper;
using Mango.Services.API.Entities;
using Mango.Services.API.Models;
using Mango.Web.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.API.Repository
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

        public async Task<CartDetailModel> AddToShoppingCart(CartDetailModel cartDetailModel)
        {
            // map productDetailsViewModel to product
            Product product = _mapper.Map<Product>(cartDetailModel.Product);

            // get header of shoppingcart user with userId
            CartHeader cartHeader = await _db.CartHeaders.Where(c => c.UserId == cartDetailModel.UserId).FirstOrDefaultAsync();
            // if headerId ==null -> create a new header with this userId
            if (cartHeader == null)
            {
                cartHeader = new CartHeader()
                {
                    UserId = cartDetailModel.UserId,
                    CouponCode = null,
                };
                // add header to the db
                _db.CartHeaders.Add(cartHeader);
                await _db.SaveChangesAsync();
            }
            //if the cartDetail doesn't exist -> Create new cartDetail (ProductId,quantity,HeaderId) for this headerId
            CartDetail cartDetail = await _db.CartDetails.Where(c =>c.ProductId==product.ProductId).FirstOrDefaultAsync();
            if(cartDetail == null)
            {
                cartDetail = new CartDetail()
                {
                    HeaderId = cartHeader.Id,
                    ProductId = product.ProductId,
                    Quantity = cartDetailModel.Quantity

                };
                _db.CartDetails.Add(cartDetail);
                await _db.SaveChangesAsync();
            }
            else
            // else change only the quantity
            {          
                cartDetail.Quantity += cartDetailModel.Quantity;       
                _db.CartDetails.Update(cartDetail);
                await _db.SaveChangesAsync();
            }
           
            return _mapper.Map<CartDetailModel>(cartDetail);
        }
    }
}
