using Microsoft.AspNetCore.Mvc;
using SportSrore.Models;
using SportStore.Models;

namespace SportSrore.Components
{
    public class CartSummaryViewComponent : ViewComponent
    {
        private Cart _cart;

        public CartSummaryViewComponent(Cart cartSevice)
        {
            _cart = cartSevice;
        }

        public IViewComponentResult Invoke()
        {
            return View(_cart);
        }

    }
}
