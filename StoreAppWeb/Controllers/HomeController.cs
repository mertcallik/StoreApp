using Microsoft.AspNetCore.Mvc;
using StoreApp.Data.Abstract;
using StoreApp.Data.Concreate;

namespace StoreAppWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStoreRepository<Product> _productRepository;
        public HomeController(IStoreRepository<Product> productRepository)
        {
            _productRepository= productRepository;
        }
        public IActionResult Index()
        {
            var model = _productRepository.GetAll;
            return View(model);
        }

    }
}
