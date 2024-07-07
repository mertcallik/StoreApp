using AutoMapper;
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
        private readonly IMapper _mapper;
        public HomeController(IStoreRepository<Product> productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        private int _pageSize = 6;
        public IActionResult Index(string category, int page = 1)
        {


            var model = new ProductListViewModel()
            {
                Products = _mapper.Map<IEnumerable<Product>,
                IEnumerable<ProductViewModel>>(_productRepository.GetProductsByCategory(category,page,_pageSize)),
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
