using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderApp.Server.Repositories.Contracts;
using OrderApp.Shared.Dtos;
using OrderApp.Server.Extensions;
using OrderApp.Client.Pages;
using OrderApp.Server.Entity;
using OrderApp.Client.Services;
using Microsoft.EntityFrameworkCore;
using BlazorFullStackCrud.Shared;

namespace OrderApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WindowController : ControllerBase
    {
        private readonly IWindowRepository _windowRepository;
        private readonly IOrderRepository _orderRepository;

        public WindowController(IWindowRepository windowRepository, IOrderRepository orderRepository)
        {
            _windowRepository = windowRepository;
            _orderRepository = orderRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<WindowTable>>> GetAllWindows()
        {
            try
            {
                var orders = await _orderRepository.GetOrders();
                var windows = await _windowRepository.GetAllWindows();

                if (windows == null)
                {
                    return NotFound();
                }
                else
                {
                    var windowDtos = windows.ConvertToWindowDto(orders);
                    return Ok(windowDtos);
                }
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error retriving data from the database");
            }
        }

        [HttpGet("{windowId:int}/{orderId:int}")]
        public async Task<ActionResult<OrderDto>> GetSingleWindow(int windowId, int orderId)
        {
            try
            {
                var orders = orderId != 0 ? await _orderRepository.GetSingleOrder(orderId) : null;
                var window = await _windowRepository.GetSingleWindow(windowId);

                if (window == null)
                {
                    return BadRequest();
                }
                else
                {

                    var windowDtos = orders != null ? window.ConvertToSingleWindowDto(orders) : window.ConvertToSingleWindowDto();
                    return Ok(windowDtos);
                }
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error retriving data from the database");
            }
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<WindowDto>>> CreateWindow(WindowDto window)
        {
            try
            {
                var createWindow = await _windowRepository.CreateWindow(window);
                return Ok(createWindow);

            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error retriving data from the database");
            }
        }



        [HttpPut("{id}")]
        public async Task<ActionResult<IEnumerable<WindowDto>>> UpdateWindow(WindowDto window, int id)
        {
            try
            {
                //var getOrderRow = _orderRepository.GetSingleOrder(window.OrderId);
                //if (getOrderRow != null)
                //{
                //    OrderDto orederData = new OrderDto();
                //    orederData.OrderId = getOrderRow.Result.OrderId;
                //    orederData.Name = getOrderRow.Result.Name;
                //    orederData.State = getOrderRow.Result.State;

                //    await _orderRepository.UpdateOrder(orederData, window.OrderId);
                //}
                var updateWindow = await _windowRepository.UpdateWindow(window, id);
                return Ok(updateWindow);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error retriving data from the database");
            }
        }



        [HttpDelete("{id}")]
        public async Task<ActionResult<IEnumerable<WindowDto>>> DeleteWindow(int id)
        {
            try
            {
                var deleteWindow = await _windowRepository.DeleteWindow(id);
                return Ok(deleteWindow);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error retriving data from the database");
            }
        }

        [HttpGet("orderListDropdown")]
        public async Task<ActionResult<List<DropdownForOrder>>> GetDropdown()
        {
            var orderListDropdown = await _orderRepository.GetOrders();
            return Ok(orderListDropdown);
        }
    }


}
