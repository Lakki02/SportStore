using SportStore.Models;
using System.Collections.Generic;
using System.Linq;

namespace SportSrore.Models
{
    public class Cart
    {
        private List<CartLine> _lineCollection = new List<CartLine>();

        public virtual void AddItem( Product product, int quantity)
        {
            CartLine line = _lineCollection
                .Where(p => p.Product.ProductID == product.ProductID)
                .FirstOrDefault();
            if (line == null)
            {
                _lineCollection.Add(new CartLine
                {
                    Product = product,
                    Quantity = quantity
                });
            } else
            {
                line.Quantity += quantity;
            }
        }

        public virtual void RemoveLine(Product product) =>
               _lineCollection.RemoveAll(i => i.Product.ProductID == product.ProductID);

        public virtual decimal ComputeTotalValue() =>
            _lineCollection.Sum(e => e.Product.Price * e.Quantity);

        public virtual void Clear() => _lineCollection.Clear();

        public virtual IEnumerable<CartLine> Lines => _lineCollection;
    }

    public class CartLine
    {
        public int CartLineId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
