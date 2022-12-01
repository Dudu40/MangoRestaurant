using Mango.Web.Model;
using Mango.Web.Models.ViewModel;
using Mango.Web.Services.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace Mango.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly List<LoginViewModel> users = null;
        private readonly IProductService _productService;
        public HomeController(IProductService productServices)
        {
            users = new List<LoginViewModel>();
            users.Add(new LoginViewModel()
            {
                Name= "Admin",
                Password = "Admin",
            });
            _productService = productServices;
        }

        public async Task<IActionResult> Index()
        {
            // get all Products
            // view list of this products

            var response = await _productService.GetProductsAsync<ResponseModel>();
            if (response.IsSuccess && response.Result!= null)
            {
                var productList = JsonConvert.DeserializeObject<List<ProductModel>>(response.Result.ToString());
                return View(productList);
            }
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult Login(LoginViewModel userModel)
        {   
            return View();
        }
        public IActionResult Logout()
        {
            return SignOut("Cookies", "oidc");
        }

    }
}
