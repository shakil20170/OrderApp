using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderApp.Client.Pages;
using OrderApp.Server.Data;
using OrderApp.Server.Entity;
using OrderApp.Server.Repositories.Contracts;
using OrderApp.Shared.Dtos;

namespace OrderApp.Server.Repositories
{
    public class SubElementRepository : ISubElementRepository
    {
        public readonly OrderAppDbContext _orderAppDbContext;
        public SubElementRepository(OrderAppDbContext orderAppDbContext)
        {
            _orderAppDbContext = orderAppDbContext;
        }

        public async Task<IEnumerable<SubElementTable>> GetAllSubElements()
        {
            var subElements = await _orderAppDbContext.SubElementTable.ToListAsync();
            return subElements;
        }

        public async Task<SubElementTable> GetSingleSubElement(int id)
        {
            var singleSubElement = _orderAppDbContext.SubElementTable.SingleOrDefault(o => o.SubElementID == id);
            return singleSubElement;
        }


        public async Task<IEnumerable<SubElementTable>> GetSubElementByWindowId(int windowId)
        {
            var subElement = await _orderAppDbContext.SubElementTable
               .Where(s => s.WindowTableWindowID == windowId)
               .ToListAsync();

            return subElement;
        }

        public async Task<List<SubElementTable>> CreateSubElement(SubElementDto subElement)
        {
            SubElementTable dbObject = new SubElementTable();
            dbObject.WindowTableWindowID = subElement.WindowTableWindowID;
            dbObject.Element = subElement.Element;
            dbObject.SubElementType = subElement.SubElementType;
            dbObject.Width = subElement.Width;
            dbObject.Height = subElement.Height;

            _orderAppDbContext.SubElementTable.Add(dbObject);

            await _orderAppDbContext.SaveChangesAsync();

            return (await GetDbSubElement());
        }

        private async Task<List<SubElementTable>> GetDbSubElement()
        {
            return await _orderAppDbContext.SubElementTable.ToListAsync();
        }


        public async Task<List<SubElementTable>> UpdateSubElement(SubElementDto subElement, int id)
        {
            var updateSubElement = await GetSingleSubElement(id);
            if (updateSubElement == null)
            {
                return null;
            }
            else
            {
                updateSubElement.Element = subElement.Element;
                updateSubElement.SubElementType = subElement.SubElementType;
                updateSubElement.Width = subElement.Width;
                updateSubElement.Height = subElement.Height;
                updateSubElement.WindowTableWindowID= subElement.WindowTableWindowID;
                await _orderAppDbContext.SaveChangesAsync();

            }
            return (await GetDbSubElement());
        }


        public async Task<List<SubElementTable>> DeleteSubElement(int id)
        {
            // Retrieve  order table  
            var subElement = _orderAppDbContext.SubElementTable.FirstOrDefault(o => o.SubElementID == id);
            if (subElement != null)
            {
                _orderAppDbContext.SubElementTable.Remove(subElement);

                // Save changes to the database
                await _orderAppDbContext.SaveChangesAsync();
            }

            return (await GetDbSubElement());
        }


    }
}
