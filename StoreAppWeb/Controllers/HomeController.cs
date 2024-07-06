using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreApp.Data.Abstract;
using StoreApp.Data.Model;
using StoreAppWeb.Models;

namespace StoreAppWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStoreRepository<Product> _productRepository;
        public HomeController(IStoreRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        private int _pageSize = 6;
        public IActionResult Index(string category, int page = 1)
        {


            var model = new ProductListViewModel()
            {
                Products = _productRepository.GetProductsByCategory(category, page, _pageSize).Select(x=>new ProductViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Price = x.Price
                }),
                PageInfo = new PageInfo()
                {
                    TotalItems = _productRepository.GetProductCount(category),
                    ItemsPerPage = _pageSize,
                    CurrentPage = page
                }
            };
            return View(model);
        }

    }
}
