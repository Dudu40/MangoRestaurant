using Mango.Services.ProductAPI.Model;
using Mango.Services.ProductAPI.Repository;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<object> Get()
        {
            try
            {
                IEnumerable<ProductModel> products = await _productRepository.GetProducts();
                _response.result = products;

            }
            catch (Exception ex)
            {
                _response.isSucces = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };

            }
            return _response;       
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<object> GetProductById(int id)
        {
            try
            {
                ProductModel product = await _productRepository.GetProductById(id);
                _response.result = product;

            }
            catch (Exception ex)
            {
                _response.isSucces = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };

            }
            return _response;
        }

        [HttpPost]
        public async Task<object> Add([FromBody] ProductModel productModel)
        {
            try
            {
                ProductModel product = await _productRepository.AddProduct(productModel);
                _response.result = productModel;
            }
            catch(Exception ex)
            {
                _response.isSucces = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPut]
        public async Task<object> Update([FromBody] ProductModel productModel)
        {
            try
            {
                ProductModel product = await _productRepository.UpdateProduct(productModel);
                _response.result = productModel;
            }
            catch (Exception ex)
            {
                _response.isSucces = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpDelete]
        public async Task<object> Delete(int id)
        {
            try
            {
                bool isSucces = await _productRepository.DeleteProduct(id);
                _response.result = isSucces;
            }
            catch (Exception ex)
            {
                _response.isSucces = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

    }
}
