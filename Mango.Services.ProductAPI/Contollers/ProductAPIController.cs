using Mango.Services.ProductAPI.Model;
using Mango.Services.ProductAPI.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client.Utils.Windows;

namespace Mango.Services.ProductAPI.Controllers
{
    [Route("api/products")]
    public class ProductAPIController : Controller
    {
        protected ResponseModel _response;
        private IProductRepository _productRepository;

        public ProductAPIController(IProductRepository productRepository )
        {
            _response = new ResponseModel();
            _productRepository = productRepository;
        }

        [HttpGet]
        [Authorize]
        public async Task<object> GetProducts()
        {
            try
            {
                IEnumerable<ProductModel> products = await _productRepository.GetProducts();
                _response.Result = products;

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };

            }
            return _response;       
        }

        [HttpGet]
        [Authorize]
        [Route("{id}")]
        public async Task<object> GetProductById(int id)
        {
            try
            {
                ProductModel product = await _productRepository.GetProductById(id);
                _response.Result = product;

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };

            }
            return _response;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<object> AddProduct([FromBody] ProductModel productModel)
        {
            try
            {
                ProductModel product = await _productRepository.AddProduct(productModel);
                _response.Result = productModel;
            }
            catch(Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPut]
        [Authorize]
        public async Task<object> UpdateProduct([FromBody] ProductModel productModel)
        {
            try
            {
                ProductModel product = await _productRepository.UpdateProduct(productModel);
                _response.Result = productModel;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        [Route("{id}")]
        public async Task<object> DeleteProduct(int id)
        {
            try
            {
                bool isSucces = await _productRepository.DeleteProduct(id);
                _response.Result = isSucces;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

    }
}
