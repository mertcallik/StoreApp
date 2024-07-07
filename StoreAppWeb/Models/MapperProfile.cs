using AutoMapper;
using StoreApp.Data.Model;

namespace StoreAppWeb.Models
{
    public class MapperProfile:Profile
    {
        public MapperProfile()
        {
            CreateMap<Product, ProductViewModel>();
        }
    }
}
