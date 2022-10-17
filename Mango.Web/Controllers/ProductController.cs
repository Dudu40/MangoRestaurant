using Mango.Web.Model;
using Mango.Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Mango.Web.Controllers
{
    public class ProductController : Controller
    {
        private IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        public async Task<IActionResult> ProductIndex()
        {
            List<ProductModel> list = new();
            var response = await _productService.GetProductsAsynch<ResponseModel>();
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<ProductModel>>(Convert.ToString(response.Result));
            }
            return View(list);
        }
        public IActionResult ProductCreate()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductCreate(ProductModel product)
        {
            var response = await _productService.AddProductAsynch<ResponseModel>(product);
            if (response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(ProductIndex));
            }
            return View();
        }

        public async Task<IActionResult> ProductEdit(int id)
        {
            var response = await _productService.GetProductByIdAsynch<ResponseModel>(id);
            if (response != null && response.IsSuccess)
            {
                ProductModel product = JsonConvert.DeserializeObject<ProductModel>(Convert.ToString(response.Result));
                return View(product);
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductEdit(ProductModel product)
        {
            var response = await _productService.UpdateProductAsynch<ResponseModel>(product);
            if (response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(ProductIndex));
            }
            return View(product);
        }

        public async Task<IActionResult> ProductDelete(int id)
        {
            var response = await _productService.GetProductByIdAsynch<ResponseModel>(id);
            if (response != null && response.IsSuccess)
            {
                ProductModel product = JsonConvert.DeserializeObject<ProductModel>(Convert.ToString(response.Result));
                return View(product);
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductDelete(ProductModel product)
        {
            var response = await _productService.DeleteProductAsynch<ResponseModel>(product.ProductId);
            if (response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(ProductIndex));
            }
            return View(product);
        }
    }
}
