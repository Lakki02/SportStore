using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SportStore.Models;
using SportSrore.Models.ViewModels;

namespace SportStore.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository _repository;

        public ProductController(IProductRepository repo)
        {
            _repository = repo;
        }

        public int PageSize = 4;
        /*public IActionResult Index()
        {
            return View();
        }*/

        public ViewResult list(int productPage = 1) =>
            //View(_repository.Products
            //    .OrderBy(p => p.ProductID)
            //    .Skip((productPage-1)*PageSize)
            //    .Take(PageSize));
            View(new ProductsListViewModel
            {
                Products = _repository.Products
                .OrderBy(p => p.ProductID)
                .Skip((productPage - 1) * PageSize)
                .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = productPage,
                    ItemsPerPage = PageSize,
                    TotalItem = _repository.Products.Count()
                }
            });

    }
}
