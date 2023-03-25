using Microsoft.AspNetCore.Components;
using OrderApp.Client.Services.Contracts;
using OrderApp.Shared.Dtos;

namespace OrderApp.Client.Pages.OrderPage
{
    public class OrderCreateEditBase : ComponentBase
    {
        [Parameter]
        public int? Id { get; set; }

        [Inject]
        public IOrderService OrderService { get; set; }

        public string btnText = string.Empty;
        public string ErrorMessage { get; set; }

        public OrderDto Order = new OrderDto();

        protected override async Task OnInitializedAsync()
        {
            btnText = Id == null ? "Create" : "Update";
        }

        protected override async Task OnParametersSetAsync()
        {
            if (Id == null)
            {

            }
            else
            {
                Order = await OrderService.GetOrder((int)Id);
            }
        }

        public async Task HandleSubmit()
        {
            if (Id == null)
            {
                await OrderService.CreateOrder(Order);                                                                                                                                      
            }
            else
            {
                await OrderService.UpdateOrder(Order);
            }
        }
        public async Task DeleteOrder()
        {
            await OrderService.DeleteOrder(Order.OrderId);
        }


    }
}
