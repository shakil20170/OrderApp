using Microsoft.AspNetCore.Components;
using OrderApp.Client.Services.Contracts;
using OrderApp.Shared.Dtos;

namespace OrderApp.Client.Pages.WindowPage
{
    public class WindowCreateEditBase : ComponentBase
    {
        [Parameter]
        public int? Id { get; set; }
        [Parameter]
        public int? orderId { get; set; }

        [Inject]
        public IWindowService WindowService { get; set; }

        public string btnText = string.Empty;
        public string ErrorMessage { get; set; }

        public WindowDto windowDto = new WindowDto();

        protected override async Task OnInitializedAsync()
        {
            btnText = Id == null ? "Create" : "Update";
            await WindowService.GetDropdown();

        }

        protected override async Task OnParametersSetAsync()
        {
            if (Id == null )
            {
 
            }
            else
            {
                if (orderId == null)
                {
                    windowDto = await WindowService.GetWindow((int)Id,(int)0);
                }
                else
                {
                    windowDto = await WindowService.GetWindow((int)Id,(int)orderId);

                }
            }
        }

        public async Task HandleSubmit()
            {
            if (Id == null)
            {
                await WindowService.CreateWindow(windowDto);
            }
            else
            {
                await WindowService.UpdateWindow(windowDto);
            }
        }
        public async Task DeleteWindow()
        {
            await WindowService.DeleteWindow(windowDto.WindowID);
        }

        







    }
}
