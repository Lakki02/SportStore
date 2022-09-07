using System.Collections.Generic;
using System.Linq;

namespace SportSrore.Models
{
    public class FakeProductRepository :IProductRepository
    {
        public IQueryable<Productcs> Productcs => new List<Productcs>
        {
            new Productcs {Name = "Football", Price = 25},
            new Productcs {Name = "Surf board", Price = 179},
            new Productcs {Name = "Running shoes", Price = 95}
        }.AsQueryable<Productcs>();
    }
}
