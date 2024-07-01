using Microsoft.AspNetCore.Mvc;
using StoreApp.Data.Abstract;
using StoreApp.Data.Model;

namespace StoreAppWeb.Components
{
    public class CategoriesListViewComponent:ViewComponent
    {
        private IStoreRepository<Product> _storeRepository;
        public CategoriesListViewComponent(IStoreRepository<Product> storeRepository)
        {
            _storeRepository=storeRepository;
        }
        public IViewComponentResult Invoke()
        {
            var categories = _storeRepository.GetAll.Select(x => x.Category).Distinct().ToList();
            return View(categories);
        }
    }
}
