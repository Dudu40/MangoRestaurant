
using Mango.Services.API.Entities;
using Mango.Services.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.API.Repository
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly ApplicationDbContext _db;
        private IProductRepository _productRepository;
        public ShoppingCartRepository(ApplicationDbContext db, IProductRepository productRepository)
        {
            _db = db;
            _productRepository = productRepository;
        }

        public async Task<List<CartDetailModel>> GetAllDetailsByIdUser(string idUser)
        {
            List<CartDetailModel> modelList = new List<CartDetailModel>();
            CartHeader cartHeader = await _db.CartHeaders.Where(c => c.UserId == idUser).FirstOrDefaultAsync(); 
            List<CartDetail> cartDetails = await _db.CartDetails.Where(c=>c.HeaderId==cartHeader.Id).ToListAsync();

            foreach (CartDetail cartDetail in cartDetails)
            {
                modelList.Add(
                    new CartDetailModel()
                    {
                        Product = await _productRepository.GetProductById(cartDetail.ProductId),
                        Quantity = cartDetail.Quantity,
                    }
                );
            }
            return modelList;
        }

        public async Task<CartDetailModel> GetCartbyProductId(int id)
        {
            CartDetail cartDetail = await _db.CartDetails.Where(c => c.ProductId == id).FirstOrDefaultAsync();
            CartDetailModel cartDetailModel = new CartDetailModel();
            cartDetailModel.Quantity = 1;
            cartDetailModel.Product = await _productRepository.GetProductById(cartDetail.ProductId);
            if (cartDetail != null)
            {
                cartDetailModel.Quantity=cartDetail.Quantity;
            }
            return cartDetailModel;
        }

        public async Task<bool> RemoveItem(int id)
        {
            CartDetail cartDetail = await _db.CartDetails.Where(c=>c.ProductId==id).FirstOrDefaultAsync();
            if (cartDetail == null)
            {
                return false;
            }
            try
            {
                _db.CartDetails.Remove(cartDetail);
                await _db.SaveChangesAsync();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
        public async Task<CartDetailModel> EditItem(CartDetailModel model)
        {
            CartDetail cartDetail = await _db.CartDetails.Where(c => c.ProductId == model.Product.ProductId).FirstOrDefaultAsync();
            cartDetail.Quantity=model.Quantity;
            _db.CartDetails.Update(cartDetail);
            await _db.SaveChangesAsync();
           return model;
            
        }

        public async Task<CartHeaderModel> GetHeaderByIdUser(string idUser)
        {
            CartHeader cartHeader = await _db.CartHeaders.Where(c => c.UserId == idUser).FirstOrDefaultAsync();
            CartHeaderModel cartHeaderModel = new CartHeaderModel()
            {
                Id = cartHeader.Id,
                UserId = cartHeader.UserId,
                CouponCode = cartHeader.CouponCode
            };
            return cartHeaderModel; 
        }

        public async Task<bool> ApplyCoupon(CartHeaderModel cartHeaderModel)
        {
            CartHeader cartHeader = await _db.CartHeaders.Where(c => c.UserId == cartHeaderModel.UserId).FirstOrDefaultAsync();
            if (cartHeader == null)
            {
                return false;
            }
            try
            {
                cartHeader.CouponCode = cartHeaderModel.CouponCode;
                _db.CartHeaders.Update(cartHeader);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<bool> RemoveCoupon(string UserId)
        {
            CartHeader cartHeader = await _db.CartHeaders.Where(c => c.UserId == UserId).FirstOrDefaultAsync();
            if (cartHeader == null)
            {
                return false;
            }
            try
            {
                cartHeader.CouponCode = "";
                _db.CartHeaders.Update(cartHeader);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
