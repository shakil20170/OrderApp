using OrderApp.Server.Entity;
using OrderApp.Shared.Dtos;
using static OrderApp.Shared.Dtos.WindowDto;

namespace OrderApp.Server.Repositories.Contracts
{
    public interface IWindowRepository
    {
        Task<IEnumerable<WindowTable>> GetAllWindows();
        Task<WindowTable> GetSingleWindow(int id);
        Task<List<WindowTable>> CreateWindow(WindowDto window);
        Task<List<WindowTable>> UpdateWindow(WindowDto window, int id);
        Task<List<WindowTable>> DeleteWindow(int id);
        //List<DropdownItemList> GetAllOrderForDropdown();
    }
}
