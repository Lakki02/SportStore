using System.Linq;

namespace SportSrore.Models
{
    public interface IOrderRepository
    {
        public IQueryable<Order> Orders { get; }

        void SaveOrder(Order order);
    }
}
