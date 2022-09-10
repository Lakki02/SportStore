using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SportSrore.Infrastructure;
using SportSrore.Models;
using SportStore.Models;
using SportSrore.Models.ViewModels;


namespace SportSrore.Controllers
{
    public class CartController : Controller
    {
        private IProductRepository _repository;

        private Cart _cart;

        public CartController(IProductRepository repo, Cart cartService)
        {
            _repository = repo;
            _cart = cartService;
        }

        public ViewResult Index(string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = _cart,
                ReturnUrl = returnUrl
            });
        }

        public RedirectToActionResult AddToCart(int productId, string returnUrl)
        {
            Product product = _repository.Products
                .FirstOrDefault(p => p.ProductID == productId);

            if (product != null)
            {
                _cart.AddItem(product,1);
                /*Cart cart = GetCart(); После использования служюы упрощение
                cart.AddItem(product,1);
                SaveCart(cart);*/
            }
            return RedirectToAction("Index", new {returnUrl});
        }

        public RedirectToActionResult RemoveFromCart(int productId, string returnUrl)
        {
            Product product = _repository.Products
                .FirstOrDefault(p => p.ProductID == productId);
            if (product != null)
            {
                _cart.RemoveLine(product);
                /*Cart cart = GetCart();
                cart.RemoveLine(product);
                SaveCart(cart);*/
            }
            return RedirectToAction("Index", new { returnUrl });
        }
        /*
        private void SaveCart(Cart cart)
        {
            HttpContext.Session.SetJson("Cart", cart);
        }

        private Cart GetCart()
        {
            Cart cart = HttpContext.Session.GetJson<Cart>("Cart") ?? new Cart();
            return cart;
        }
        */
    }
}
