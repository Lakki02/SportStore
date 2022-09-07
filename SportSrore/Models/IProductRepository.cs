using System.Linq;

namespace SportSrore.Models
{
    public interface IProductRepository
    {
        IQueryable<Product> Productcs { get; }
    }
}
