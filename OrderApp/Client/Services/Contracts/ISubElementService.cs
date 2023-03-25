using BlazorFullStackCrud.Shared;
using OrderApp.Shared.Dtos;

namespace OrderApp.Client.Services.Contracts
{
    public interface ISubElementService
    {
        Task<IEnumerable<SubElementDto>> GetAllSubElements();
        Task<SubElementDto> GetSubElement(int Id);

        Task CreateSubElement(SubElementDto subElementDot);
        Task UpdateSubElement(SubElementDto order);
        Task DeleteSubElement(int id);
        Task GetDropdown();
        List<DropdownForWindow> windowListDropdown { get; set; }
    }
}
