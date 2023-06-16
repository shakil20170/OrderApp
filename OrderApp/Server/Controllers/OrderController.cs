using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderApp.Server.Repositories.Contracts;
using OrderApp.Shared.Dtos;
using OrderApp.Server.Extensions;
using OrderApp.Client.Pages;

namespace OrderApp.Server.Controllers
{


    /// <summary>
    /// Adding here comments for new changes.
    /// I will add this changes too
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;

        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrders() {
            try
            {
                var orders = await _orderRepository.GetOrders();
                


                if (orders == null)
                { 
                    return NotFound();
                }
                else
                {
                    //var orderDtos1 = orders.ConvertToDto();
                    var orderDtos = orders.ConvertToDto();
                    return Ok(orderDtos);
                }
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,"Error retriving data from the database");
            }
        }


        [HttpGet("{id:int}")]
        public async Task<ActionResult<OrderDto>> GetSingleOrder(int id)
        {
            try
            {
                var order = await _orderRepository.GetSingleOrder(id);

                if (order == null)
                {
                    return BadRequest();
                }
                else
                {
                    var orderDtos = order.ConvertToDto();
                    return Ok(orderDtos);
                }
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error retriving data from the database");
            }
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<OrderDto>>> CreateOrder(OrderDto order)
        {
            try
            {
                var createOrder = await _orderRepository.CreateOrder(order);
                return Ok(createOrder.ConvertToDto());
                
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error retriving data from the database");
            }
        }



        [HttpPut("{id}")]
        public async Task<ActionResult<IEnumerable<OrderDto>>> UpdateOrder(OrderDto order,int id)
        {
            try
            {
                var updateOrder = await _orderRepository.UpdateOrder(order, id);
                return Ok(updateOrder.ConvertToDto());
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error retriving data from the database");
            }
        }



        [HttpDelete("{id}")]
        public async Task<ActionResult<IEnumerable<OrderDto>>> DeleteOrder(int id)
        {
            try
            {
                var deleteOrder = await _orderRepository.DeleteOrder(id);
                return Ok(deleteOrder.ConvertToDto());
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error retriving data from the database");
            }
        }

    }

   
}
