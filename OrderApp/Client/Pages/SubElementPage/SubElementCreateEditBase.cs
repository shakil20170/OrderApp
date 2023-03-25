using Microsoft.AspNetCore.Components;
using OrderApp.Client.Services;
using OrderApp.Client.Services.Contracts;
using OrderApp.Shared.Dtos;

namespace OrderApp.Client.Pages.SubElementPage
{
    public class SubElementCreateEditBase : ComponentBase
    {
        [Parameter]
        public int? Id { get; set; }
        [Inject]
        public ISubElementService SubElementService { get; set; }



        public string btnText = string.Empty;
        public string ErrorMessage { get; set; }

        public SubElementDto SubElementDto = new SubElementDto();

        protected override async Task OnInitializedAsync()
        {
            btnText = Id == null ? "Create" : "Update";
            await SubElementService.GetDropdown();

        }

        protected override async Task OnParametersSetAsync()
        {
            if (Id == null)
            {

            }
            else
            {
                SubElementDto = await SubElementService.GetSubElement((int)Id);
            }
        }

        public async Task HandleSubmit()
        {
            if (Id == null)
            {
                await SubElementService.CreateSubElement(SubElementDto);                                                                                                                                      
            }
            else
            {
                await SubElementService.UpdateSubElement(SubElementDto);
            }
        }
        public async Task DeleteSubElement()
        {
            await SubElementService.DeleteSubElement(SubElementDto.SubElementID);
        }


    }
}
