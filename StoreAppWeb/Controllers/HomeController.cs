using Microsoft.AspNetCore.Mvc;
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
            _productRepository= productRepository;
        }

        private int _pageSize = 3;
        public IActionResult Index(int page=1)
        {
            var products = _productRepository.GetAll.Skip((page-1)*_pageSize)
                .Select(p => new ProductViewModel()
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Description = p.Description,
                Category = p.Category,
            }).Take(_pageSize);

            var model = new ProductListViewModel()
            {
                Products = products,
                PageInfo = new PageInfo()
                {
                    TotalItems = _productRepository.GetAll.Count(),
                    ItemsPerPage = _pageSize,
                    CurrentPage = page
                }
            };
            return View(model);
        }

    }
}
