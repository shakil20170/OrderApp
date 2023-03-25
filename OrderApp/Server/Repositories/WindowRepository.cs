using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderApp.Client.Pages;
using OrderApp.Server.Data;
using OrderApp.Server.Entity;
using OrderApp.Server.Repositories.Contracts;
using OrderApp.Shared.Dtos;
using static OrderApp.Shared.Dtos.WindowDto;

namespace OrderApp.Server.Repositories
{
    public class WindowRepository : IWindowRepository
    {
        public readonly OrderAppDbContext _orderAppDbContext;
        public WindowRepository(OrderAppDbContext orderAppDbContext)
        {
            _orderAppDbContext = orderAppDbContext;
        }

        

        public async Task<IEnumerable<SubElementTable>> GetSubElements()
        {
            var subElement = await _orderAppDbContext.SubElementTable.ToListAsync();
            return subElement;
        }

        public async Task<IEnumerable<WindowTable>> GetAllWindows()
        {
            var windows = await _orderAppDbContext.WindowTable.ToListAsync();
            return windows;
        }

        //public async Task<Dictionary<int, string>> GetWindowList()
        //{
        //    var windowsIdNameList = await _orderAppDbContext.WindowTable.ToDictionaryAsync(x => x.WindowID, x => x.WindowName); 
        //    return windowsIdNameList;
        //}

        public async Task<WindowTable>GetSingleWindow(int id)
        {
            var singleWindow = _orderAppDbContext.WindowTable.SingleOrDefault(o => o.WindowID == id);
            return singleWindow;
        }

        public async Task<IEnumerable<WindowTable>> GetWindowByOrderId(int orderId)
        {

            var windows = await _orderAppDbContext.WindowTable
                .Where(w => w.OrderTableOrderId == orderId)
                .ToListAsync();

            //return windows.Select(w => new WindowTable
            //{
            //    WindowID = w.WindowID,
            //    OrderTableOrderId = w.OrderTableOrderId,
            //    WindowName = w.WindowName,
            //    QuantityOfWindows = w.QuantityOfWindows,
            //    TotalSubElements = w.TotalSubElements,
            //    SubElements = w.SubElements.Select(se => new SubElementTable
            //    {
            //        SubElementID = se.SubElementID,
            //        WindowID = se.WindowID,
            //        ElementNumber = se.ElementNumber,
            //        Type = se.Type,
            //        Width = se.Width,
            //        Height = se.Height
            //    }).ToList()
            //});

            return windows;

        }

        public async Task<IEnumerable<SubElementTable>> GetSubElementByWindowId(int windowId)
        {
            var subElement = await _orderAppDbContext.SubElementTable
               .Where(s => s.WindowTableWindowID == windowId)
               .ToListAsync();

            return subElement;
        }

        public async Task<List<WindowTable>> CreateWindow(WindowDto window)
        {
            ////first create data in order table
            //OrderTable orderObj = new OrderTable();
            //orderObj.Name= window.Name;
            ////orderObj.State= window.State;
            //_orderAppDbContext.OrderTable.Add(orderObj);
            //await _orderAppDbContext.SaveChangesAsync();


            WindowTable windowObj = new WindowTable();
            windowObj.WindowName = window.WindowName;
            windowObj.QuantityOfWindows = window.QuantityOfWindows;
            windowObj.TotalSubElements = window.TotalSubElements;
            windowObj.OrderTableOrderId = window.OrderId;
            _orderAppDbContext.WindowTable.Add(windowObj);

            await _orderAppDbContext.SaveChangesAsync();

            return (await GetDbWindow());
        }

        private async Task<List<WindowTable>> GetDbWindow()
        {
            return await _orderAppDbContext.WindowTable.ToListAsync();
        }


        public async Task<List<WindowTable>> UpdateWindow(WindowDto window, int id)
        {
            var updateWindow = await GetSingleWindow(id);
            if (updateWindow == null)
            {
                return null;
            }
            else
            {
               updateWindow.WindowID = window.WindowID;
               updateWindow.WindowName = window.WindowName;
               updateWindow.QuantityOfWindows = window.QuantityOfWindows;
               updateWindow.TotalSubElements = window.TotalSubElements;
               updateWindow.OrderTableOrderId = window.OrderId;

                await _orderAppDbContext.SaveChangesAsync();

            }
            return (await GetDbWindow());
        }


        public async Task<List<WindowTable>> DeleteWindow(int id)
        {
            // Retrieve  order table  
            var window = _orderAppDbContext.WindowTable.Include(o => o.SubElements)
                                        .FirstOrDefault(o => o.WindowID == id);
            if (window != null)
            {
                // Remove all related sub-element table rows
                if (window.SubElements.Count != 0)
                {
                    // Remove all related window table rows
                    _orderAppDbContext.SubElementTable.RemoveRange(window.SubElements);
                }
                // Remove the order table row
                _orderAppDbContext.WindowTable.Remove(window);

                // Save changes to the database
                await _orderAppDbContext.SaveChangesAsync();
            }

            return (await GetDbWindow());
        }



       
    }
}
