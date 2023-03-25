using Microsoft.AspNetCore.Components;
using OrderApp.Client.Pages.OrderPage;
using OrderApp.Client.Services.Contracts;
using OrderApp.Shared.Dtos;

namespace OrderApp.Client.Pages.WindowPage
{
    public class WindowBase : ComponentBase
    {
        [Inject]
        public IWindowService windowService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        public IEnumerable<WindowDto> Windows { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Windows = await windowService.GetAllWindows();
        }

        public void ShowWindow(int windowId,int orderId)
        {
            NavigationManager.NavigateTo($"WindowCreateEdit/{windowId}/{orderId}");

        }

        public void CreateNewWindow()
        {
            NavigationManager.NavigateTo("/WindowCreateEdit");

        }



    }
}
