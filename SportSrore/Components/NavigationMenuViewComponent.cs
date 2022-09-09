using Microsoft.AspNetCore.Mvc;
using System.Linq;
using SportSrore.Models;
using SportStore.Models;

namespace SportSrore.Components
{
    public class NavigationMenuViewComponent :ViewComponent
    {
        private IProductRepository _repository;

        public NavigationMenuViewComponent(IProductRepository repo)
        {
            _repository = repo;
        }

        /*
        public string Invoke()
        {
            return "hello world";
        }*/
         
        
        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedCategory = RouteData?.Values["category"];
            return View(_repository.Products
                .Select(x=>x.Category)
                .Distinct()
                .OrderBy(x=>x));
        }
        
    }
}
