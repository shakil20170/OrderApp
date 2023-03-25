using BlazorFullStackCrud.Shared;
using OrderApp.Shared.Dtos;

namespace OrderApp.Client.Services.Contracts
{
    public interface IWindowService
    {
        Task<IEnumerable<WindowDto>> GetAllWindows();
        Task<WindowDto> GetWindow(int Id,int orderId);
        Task CreateWindow(WindowDto window);
        Task UpdateWindow(WindowDto window);
        Task DeleteWindow(int id);
        Task GetDropdown();
        List<DropdownForOrder> orderListDropdown { get; set; }
    }
}
