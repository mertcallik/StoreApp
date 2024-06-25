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
            return View(_productRepository.GetAll);
        }

    }
}
