
using Mango.Web.Areas.Identity.Data;
using Mango.Web.Model;
using Mango.Web.Models;
using Mango.Web.Services;
using Mango.Web.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


namespace Mango.Web.Controllers
{
    [Authorize]
    public class ShoppingCartController : Controller
    {
        private IShoppingCartService _shoppingCartService;
        private ICouponService _couponService;
        private UserManager<ApplicationUser> _userManager;
        public ShoppingCartController(IShoppingCartService shoppingCartService, ICouponService couponService,UserManager<ApplicationUser> userManager)
        {
            _shoppingCartService = shoppingCartService;
            _couponService = couponService;
            _userManager = userManager;
        }
        public async Task<IActionResult> ShoppingCartIndex()
        {
            return View(await LoadIndexDetails());
        }

        public async Task<IActionResult> RemoveItem(int ProductId)
        {
            var response = await _shoppingCartService.RemoveItemAsync<ResponseModel>(ProductId);
          
            return RedirectToAction(nameof(ShoppingCartIndex));
            
            
        }

        public async Task<IActionResult> ShoppingCartEdit(int ProductId)
        {
            var response = await _shoppingCartService.GetCartbyProductIdAsync<ResponseModel>(ProductId);
            if (response != null && response.IsSuccess)
            {

                return View(JsonConvert.DeserializeObject<CartDetailModel>(Convert.ToString(response.Result)));
            } 
            return View();

        }

        public async Task<IActionResult> EditItem(int Quantity, int ProductId)
        {
            var response1 = await _shoppingCartService.GetCartbyProductIdAsync<ResponseModel>(ProductId);
            CartDetailModel cartDetail = JsonConvert.DeserializeObject<CartDetailModel>(Convert.ToString(response1.Result));
            cartDetail.Quantity=Quantity;
            var response2 = await _shoppingCartService.EditItemAsync<ResponseModel>(cartDetail);
            return RedirectToAction(nameof(ShoppingCartIndex));
          
        }

        public IActionResult CheckoutIndex()
        {
            return View();
        }

        public async Task<ShoppingCartModel> LoadIndexDetails()
        {
            ShoppingCartModel shoppingCart = new ShoppingCartModel();
            List<CartDetailModel> cartDetails = new List<CartDetailModel>();
            CartHeaderModel cartHeaderObj = new CartHeaderModel();
            double totalPrice = 0.0;
            double couponAmount = 0.0;
            var idUser = _userManager.GetUserId(User);
            var response = await _shoppingCartService.GetAllDetailsByIdUserAsync<ResponseModel>(idUser);
            if (response != null && response.IsSuccess)
            {
                cartDetails = JsonConvert.DeserializeObject<List<CartDetailModel>>(Convert.ToString(response.Result));
                foreach (var cartDetail in cartDetails)
                {

                    totalPrice += cartDetail.Quantity * cartDetail.Product.Price;
                };

                var cartHeader = await _shoppingCartService.GetHeaderByIdUserAsync<ResponseModel>(idUser);
                if(cartHeader != null)
                {
                    cartHeaderObj = JsonConvert.DeserializeObject<CartHeaderModel>(Convert.ToString(cartHeader.Result));
                    var coupon = await _couponService.GetCouponByCodeAsync<ResponseModel>(cartHeaderObj.CouponCode);
                    if(coupon!=null && coupon.IsSuccess)
                    {
                        CouponModel couponObj = JsonConvert.DeserializeObject<CouponModel>(Convert.ToString(coupon.Result));
                        couponAmount = couponObj.Amount;
                    }
                }

                totalPrice -= couponAmount;
                
            }
            
            shoppingCart = new ShoppingCartModel()
            {
                cartHeader = cartHeaderObj,
                cartDetails = cartDetails,
                TotalPrice = totalPrice,
                CouponAmount = couponAmount
            };
            return shoppingCart;
        }


        public async Task<IActionResult> ApplyCoupon(ShoppingCartModel shoppingCart)
        {
            var response = await _shoppingCartService.ApplyCouponAsync<ResponseModel>(shoppingCart.cartHeader);
            if (response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(ShoppingCartIndex));
            }
            return View();

        }

        [HttpPost]
        [ActionName("RemoveCoupon")]
        public async Task<IActionResult> RemoveCoupon(ShoppingCartModel shoppingCart)
        {
            var response = await _shoppingCartService.RemoveCouponAsync<ResponseModel>(shoppingCart.cartHeader.UserId);
            if (response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(ShoppingCartIndex));
            }
            return View();

        }
    }
}

