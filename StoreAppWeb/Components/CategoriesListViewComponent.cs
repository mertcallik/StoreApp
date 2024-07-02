using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreApp.Data.Abstract;
using StoreApp.Data.Model;
using StoreAppWeb.Models;

namespace StoreAppWeb.Components
{
    public class CategoriesListViewComponent:ViewComponent
    {

        private IStoreRepository<Product> _productStoreRepository;
        private IStoreRepository<Category> _categoryStoreRepository;
        public CategoriesListViewComponent(IStoreRepository<Product> productStoreRepository,IStoreRepository<Category> categoryStoreRepository)
        {
            _productStoreRepository = productStoreRepository;
            _categoryStoreRepository = categoryStoreRepository;
        }
        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedCategory = RouteData?.Values["category"];
            var categories = _categoryStoreRepository.GetAll.Select(c => new CategoryViewModel()
            {
                Id = c.Id,
                Name = c.Name,
                Url = c.Url,
            });
            return View(categories);
        }
    }
}
