using OrderApp.Server.Entity;
using OrderApp.Shared.Dtos;

namespace OrderApp.Server.Repositories.Contracts
{
    public interface ISubElementRepository
    {
        Task<IEnumerable<SubElementTable>> GetAllSubElements();
        Task<SubElementTable> GetSingleSubElement(int id);
        Task<List<SubElementTable>> CreateSubElement(SubElementDto subElement);
        Task<List<SubElementTable>> UpdateSubElement(SubElementDto subElement, int id);
        //Task<List<SubElementTable>> UpdateSubElement(OrderDto orderDto, WindowDto windowDto, SubElementDto subElement, int id);
        Task<List<SubElementTable>> DeleteSubElement(int id);
    }
}
