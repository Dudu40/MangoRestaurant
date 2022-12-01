
using Mango.Services.API.Models;
using Mango.Services.API.Repository;
using Microsoft.AspNetCore.Mvc;


namespace Mango.Services.API.Controllers
{
    [Route("api/shoppingCart")]
    public class ShoppingCartAPIController : Controller
    {
        protected ResponseModel _response;
        private IShoppingCartRepository _shoppingCartRepository;

        public ShoppingCartAPIController(IShoppingCartRepository shoppingCartRepository)
        {
            _response = new ResponseModel();
            _shoppingCartRepository = shoppingCartRepository;
        }

        [HttpGet("getAllDetails/{idUser}")]
        public async Task<object> getAllDetails(string idUser)
        {
            try
            {
                List<CartDetailModel> cartDetails = await _shoppingCartRepository.GetAllDetailsByIdUser(idUser);
                _response.Result = cartDetails;

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };

            }
            return _response;
        }

        [HttpDelete("removeItem/{id}")]
        public async Task<object> removeItem(int id)
        {
            try
            {
                bool isSucces = await _shoppingCartRepository.RemoveItem(id);
                _response.Result = isSucces;

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };

            }
            return _response;
        }

        [HttpGet("getCartbyProductId/{id}")]
        public async Task<object> getCartbyProductId(int id)
        {
            try
            {
                CartDetailModel cartDetail = await _shoppingCartRepository.GetCartbyProductId(id);
                _response.Result = cartDetail;

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };

            }
            return _response;
        }
        [HttpPut("editItem")]
        public async Task<object> editItem([FromBody] CartDetailModel cartDetail)
        {
            try
            {
                CartDetailModel cartDetailModel = await _shoppingCartRepository.EditItem(cartDetail);
                _response.Result = cartDetailModel;

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };

            }
            return _response;
        }

        [HttpGet("getHeaderByIdUser/{idUser}")]
        public async Task<object> getHeaderByIdUser(string idUser)
        {
            try
            {
                CartHeaderModel cartDetail = await _shoppingCartRepository.GetHeaderByIdUser(idUser);
                _response.Result = cartDetail;

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };

            }
            return _response;
        }

        [HttpPost("applyCoupon")]
        public async Task<object> ApplyCoupon([FromBody] CartHeaderModel cartHeader)
        {
            try
            {
                bool isSucces = await _shoppingCartRepository.ApplyCoupon(cartHeader);
                _response.Result = isSucces;

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };

            }
            return _response;
        }

        [HttpPost("removeCoupon")]
        public async Task<object> RemoveCoupon([FromBody] string UserId)
        {
            try
            {
                bool isSucces = await _shoppingCartRepository.RemoveCoupon(UserId);
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
