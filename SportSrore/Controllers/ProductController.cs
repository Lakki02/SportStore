using Microsoft.AspNetCore.Mvc;
using SportStore.Models;

namespace SportStore.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository _repository;

        public ProductController(IProductRepository repo)
        {
            _repository = repo;
        }
        /*public IActionResult Index()
        {
            return View();
        }*/

        public ViewResult list() => View(_repository.Products);

    }
}
