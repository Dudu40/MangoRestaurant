using Mango.Web.Areas.Identity.Data;
using Mango.Web.Model;
using Mango.Web.Models;
using Mango.Web.Models.ViewModel;
using Mango.Web.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Mango.Web.Controllers
{
    public class ProductController : Controller
    {
        private IProductService _productService;
        private UserManager<ApplicationUser> _userManager;
        public ProductController(IProductService productService, UserManager<ApplicationUser> userManager)
        {
            _productService = productService;
            _userManager = userManager;
        }

        [Authorize(Roles = Utils.UserRoles.Admin)]
        public async Task<IActionResult> ProductIndex()
        {
            List<ProductModel> list = new();
            var response = await _productService.GetProductsAsync<ResponseModel>();
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<ProductModel>>(Convert.ToString(response.Result));
            }
            return View(list);
        }

        [Authorize(Roles = Utils.UserRoles.Admin)]
        public IActionResult ProductCreate()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Utils.UserRoles.Admin)]
        public async Task<IActionResult> ProductCreate(ProductModel product)
        {
            var response = await _productService.AddProductAsync<ResponseModel>(product);
            if (response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(ProductIndex));
            }
            return View();
        }

        [Authorize(Roles = Utils.UserRoles.Admin)]
        public async Task<IActionResult> ProductEdit(int id)
        {
            var response = await _productService.GetProductByIdAsync<ResponseModel>(id);
            if (response != null && response.IsSuccess)
            {
                ProductModel product = JsonConvert.DeserializeObject<ProductModel>(Convert.ToString(response.Result));
                return View(product);
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Utils.UserRoles.Admin)]
        public async Task<IActionResult> ProductEdit(ProductModel product)
        {
            var response = await _productService.UpdateProductAsync<ResponseModel>(product);
            if (response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(ProductIndex));
            }
            return View(product);
        }

        [Authorize(Roles = Utils.UserRoles.Admin)]
        public async Task<IActionResult> ProductDelete(int id)
        {
            var response = await _productService.GetProductByIdAsync<ResponseModel>(id);
            if (response != null && response.IsSuccess)
            {
                ProductModel product = JsonConvert.DeserializeObject<ProductModel>(Convert.ToString(response.Result));
                return View(product);
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Utils.UserRoles.Admin)]
        public async Task<IActionResult> ProductDelete(ProductModel product)
        {
            var response = await _productService.DeleteProductAsync<ResponseModel>(product.ProductId);
            if (response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(ProductIndex));
            }
            return View(product);
        }

        public async Task<IActionResult> ProductDetail(int ProductId)
        {
            var response = await _productService.GetProductByIdAsync<ResponseModel>(ProductId);
            if (response.IsSuccess && response!=null)
            {
                ProductModel product = JsonConvert.DeserializeObject<ProductModel>(response.Result.ToString());
                var productViewModel = new CartDetailModel
                {
                    Product = product,
                    Quantity = 1
                };
                return View(productViewModel);
            }
            // show details
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> AddToShoppingCart(int Quantity, int ProductId)
        {
            // add product to shoppingCard
            var response1 = await _productService.GetProductByIdAsync<ResponseModel>(ProductId);
            if(response1.IsSuccess && response1 != null)
            {
                var product = JsonConvert.DeserializeObject<ProductModel>(response1.Result.ToString());
                var cartDetail = new CartDetailModel()
                {
                    Product = product,
                    Quantity = Quantity,
                    UserId = _userManager.GetUserId(User)
                };
            
                var response2 = await _productService.AddToShoppingCartAsync<ResponseModel>(cartDetail);
            }
            return RedirectToAction("ShoppingCartIndex", "ShoppingCart");
        }
    }
}
