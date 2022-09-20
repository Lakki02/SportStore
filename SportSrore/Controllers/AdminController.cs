using Microsoft.AspNetCore.Mvc;
using SportSrore.Models;
using SportStore.Models;
using System.Linq;

namespace SportSrore.Controllers
{
    public class AdminController : Controller
    {
        private IProductRepository _repository;

        public AdminController(IProductRepository repo)
        {
            _repository = repo;
        }

        public ViewResult Index() => View(_repository.Products);

        public ViewResult Edit(int productId) =>
            View(_repository.Products
                .FirstOrDefault(p => p.ProductID == productId));

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                _repository.SaveProduct(product);
                TempData["message"] = $"{product.Name} has been saved";
                return RedirectToAction("Index");
            }
            else
            {
                //Что то  не так с данными
                return View(product);
            }
        }

        public ViewResult Create() => View("Edit", new Product());

        [HttpPost]
        public IActionResult Delete(int productId)
        {
            Product deleteProduct = _repository.DeleteProduct(productId);

            if(deleteProduct != null)
            {
                TempData["message"] = $"{deleteProduct.Name} was delete";
            }
            return RedirectToAction("Index");
            
        }
    }
}
