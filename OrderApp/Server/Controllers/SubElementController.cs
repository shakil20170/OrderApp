using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderApp.Server.Repositories.Contracts;
using OrderApp.Shared.Dtos;
using OrderApp.Server.Extensions;
using OrderApp.Client.Pages;
using OrderApp.Server.Repositories;
using Microsoft.EntityFrameworkCore;
using BlazorFullStackCrud.Shared;

namespace OrderApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubElementController : ControllerBase
    {
        private readonly ISubElementRepository _subElementRepository;
        private readonly IWindowRepository _windowRepository;
        private readonly IOrderRepository _orderRepository;

        public SubElementController(ISubElementRepository subElementRepository, IWindowRepository windowRepository, IOrderRepository orderRepository)
        {
            _subElementRepository = subElementRepository;
            _windowRepository = windowRepository;
            _orderRepository = orderRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubElementDto>>> GetAllSubElement() {
            try
            {
                var windows = await _windowRepository.GetAllWindows();
                var subElements = await _subElementRepository.GetAllSubElements();

                if (subElements == null)
                { 
                    return NotFound();
                }
                else
                {
                    //var orderDtos1 = orders.ConvertToDto();
                    var subElementDtos = subElements.ConvertToSubElementDto(windows);
                    return Ok(subElementDtos);
                }
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,"Error retriving data from the database");
            }
        }

       


        [HttpGet("{id:int}")]
        public async Task<ActionResult<SubElementDto>> GetSingleSubElement(int id)
        {
            try
            {
                var subElelment = await _subElementRepository.GetSingleSubElement(id);

                if (subElelment == null)
                {
                    return BadRequest();
                }
                else
                {
                    var windowName = _windowRepository.GetSingleWindow(subElelment.WindowTableWindowID).Result.WindowName;
                    //var windowList = _windowRepository.GetWindowList();
                    var subElementDtos = subElelment.ConvertToSingleSubElelmentDto(windowName);
                    
                    return Ok(subElementDtos);
                }
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error retriving data from the database");
            }
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<SubElementDto>>> CreateSubElement(SubElementDto subElement)
        {
            try
            {
                var createSubElement = await _subElementRepository.CreateSubElement(subElement);
                return Ok(createSubElement);

            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error retriving data from the database");
            }
        }



        [HttpPut("{id}")]
        public async Task<ActionResult<IEnumerable<SubElementDto>>> UpdateSubElement(SubElementDto subElementDto, int id)
        {
            try
            {
                var updateSubElement = await _subElementRepository.UpdateSubElement(subElementDto, id);
                return Ok(updateSubElement);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error retriving data from the database");
            }
        }



        [HttpDelete("{id}")]
        public async Task<ActionResult<IEnumerable<SubElementDto>>> DeleteSubElement(int id)
        {
            try
            {
                var deleteSubElement = await _subElementRepository.DeleteSubElement(id);
                return Ok(deleteSubElement);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error retriving data from the database");
            }
        }


        [HttpGet("windowListDropdown")]
        public async Task<ActionResult<List<DropdownForWindow>>> GetDropdown()
        {
            var windowListDropdown = await _windowRepository.GetAllWindows();
            return Ok(windowListDropdown);
        }

    }

   
}
