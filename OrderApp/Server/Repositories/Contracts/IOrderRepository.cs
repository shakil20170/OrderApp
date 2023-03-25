using OrderApp.Server.Entity;
using OrderApp.Shared.Dtos;

namespace OrderApp.Server.Repositories.Contracts
{
    public interface IOrderRepository
    {
        Task<IEnumerable<OrderTable>> GetOrders();
        Task<OrderTable> GetSingleOrder(int id);
        Task<List<OrderTable>> CreateOrder(OrderDto order);
        Task<List<OrderTable>> UpdateOrder(OrderDto order, int id);
        Task<List<OrderTable>> DeleteOrder(int id);
    }
}
