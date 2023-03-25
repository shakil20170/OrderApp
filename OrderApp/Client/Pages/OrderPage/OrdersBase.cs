using Microsoft.AspNetCore.Components;
using OrderApp.Client.Services.Contracts;
using OrderApp.Shared.Dtos;

namespace OrderApp.Client.Pages.OrderPage
{
    public class OrdersBase : ComponentBase
    {
        [Inject]
        public IOrderService OrderService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        public IEnumerable<OrderDto> Orders { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Orders = await OrderService.GetOrders();
        }

        public void ShowOrder(int id)
        {
            NavigationManager.NavigateTo($"OrderCreateEdit/{id}");

        }

        public void CreateNewOrder()
        {
            NavigationManager.NavigateTo("/OrderCreateEdit");

        }



    }
}
