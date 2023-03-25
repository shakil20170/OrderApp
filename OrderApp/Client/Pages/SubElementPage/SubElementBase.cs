using Microsoft.AspNetCore.Components;
using OrderApp.Client.Services.Contracts;
using OrderApp.Shared.Dtos;

namespace OrderApp.Client.Pages.SubElementPage
{
    public class SubElementBase : ComponentBase
    {
        [Inject]
        public ISubElementService SubElementService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        public IEnumerable<SubElementDto> SubElementDto { get; set; }

        protected override async Task OnInitializedAsync()
        {
            SubElementDto = await SubElementService.GetAllSubElements();
        }

        public void ShowSubElement(int id)
        {
            NavigationManager.NavigateTo($"SubElementCreateEdit/{id}");

        }

        public void CreateNewSubElement()
        {
            NavigationManager.NavigateTo("/SubElementCreateEdit");
        }
    }
}
