using System.Linq;

namespace SportSrore.Models
{
    public interface IProductRepository
    {
        IQueryable<Productcs> Productcs { get; }
    }
}
