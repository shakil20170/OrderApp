using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderApp.Client.Pages;
using OrderApp.Server.Data;
using OrderApp.Server.Entity;
using OrderApp.Server.Repositories.Contracts;
using OrderApp.Shared.Dtos;

namespace OrderApp.Server.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        public readonly OrderAppDbContext _orderAppDbContext;
        public OrderRepository(OrderAppDbContext orderAppDbContext)
        {
            _orderAppDbContext = orderAppDbContext;
        }

        public async Task<IEnumerable<OrderTable>> GetOrders()
        {
            var orders = await _orderAppDbContext.OrderTable.ToListAsync();
            return orders;
        }

      

        

        public async Task<OrderTable> GetSingleOrder(int id)
        {
            var singleOrders = _orderAppDbContext.OrderTable.Include(x => x.Windows).ThenInclude(y => y.SubElements).SingleOrDefault(o => o.OrderId == id);
            return singleOrders;
        }


        public async Task<IEnumerable<SubElementTable>> GetSubElementByWindowId(int windowId)
        {
            var subElement = await _orderAppDbContext.SubElementTable
               .Where(s => s.WindowTableWindowID == windowId)
               .ToListAsync();

            return subElement;
        }

        public async Task<List<OrderTable>> CreateOrder(OrderDto order)
        {
            OrderTable dbObject = new OrderTable();
            dbObject.OrderId = order.OrderId;
            dbObject.Name = order.Name;
            dbObject.State = order.State;

            _orderAppDbContext.OrderTable.Add(dbObject);

            await _orderAppDbContext.SaveChangesAsync();

            return (await GetDbOrder());
        }

        private async Task<List<OrderTable>> GetDbOrder()
        {
            return await _orderAppDbContext.OrderTable.ToListAsync();
        }


        public async Task<List<OrderTable>> UpdateOrder(OrderDto order, int id)
        {
            var updateOrder = await GetSingleOrder(id);
            if (updateOrder == null)
            {
                return null;
            }
            else
            {
                updateOrder.OrderId = order.OrderId;
                updateOrder.Name = order.Name;
                updateOrder.State = order.State;
                await _orderAppDbContext.SaveChangesAsync();

            }
            return (await GetDbOrder());
        }


        public async Task<List<OrderTable>> DeleteOrder(int id)
        {
            // Retrieve  order table  
            var order = _orderAppDbContext.OrderTable.Include(o => o.Windows).ThenInclude(w => w.SubElements)
                                        .FirstOrDefault(o => o.OrderId == id);
            if (order != null)
            {
                // Remove all related sub-element table rows
                if (order.Windows.Count != 0)
                {
                    if (order.Windows.Select(x => x.SubElements).Count() != 0)
                    {
                        foreach (var window in order.Windows)
                        {
                            _orderAppDbContext.SubElementTable.RemoveRange(window.SubElements);
                        }
                    }
                    // Remove all related window table rows
                    _orderAppDbContext.WindowTable.RemoveRange(order.Windows);
                }
                // Remove the order table row
                _orderAppDbContext.OrderTable.Remove(order);

                // Save changes to the database
                await _orderAppDbContext.SaveChangesAsync();
            }

            return (await GetDbOrder());
        }
    }
}
