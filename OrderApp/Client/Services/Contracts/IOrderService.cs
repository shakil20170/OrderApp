using OrderApp.Shared.Dtos;

namespace OrderApp.Client.Services.Contracts
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDto>> GetOrders();
        Task<OrderDto> GetOrder(int id);

        Task CreateOrder(OrderDto order);
        Task UpdateOrder(OrderDto order);
        Task DeleteOrder(int id);
    }
}
