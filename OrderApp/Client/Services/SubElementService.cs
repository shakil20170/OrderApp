using BlazorFullStackCrud.Shared;
using Microsoft.AspNetCore.Components;
using OrderApp.Client.Pages;
using OrderApp.Client.Pages.SubElementPage;
using OrderApp.Client.Services.Contracts;
using OrderApp.Shared.Dtos;
using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OrderApp.Client.Services
{
    public class SubElementService : ISubElementService
    {
        private readonly HttpClient _httpClient;
        private readonly NavigationManager _navigationManager;
        public SubElementService(HttpClient httpClient, NavigationManager navigationManager)
        {
            _httpClient= httpClient;
            _navigationManager = navigationManager;
        }

        public async Task<IEnumerable<SubElementDto>> GetAllSubElements()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/SubElement");

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    { 
                        return Enumerable.Empty<SubElementDto>();
                    }

                    return await response.Content.ReadFromJsonAsync<IEnumerable<SubElementDto>>();
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception(message);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<SubElementDto> GetSubElement(int Id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/SubElement/{Id}");
                //return orders;

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return default(SubElementDto);
                    }

                    return await response.Content.ReadFromJsonAsync<SubElementDto>();
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception(message);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public IEnumerable<SubElementDto> subElementDto { get; set; } = new List<SubElementDto>();
        public List<DropdownForWindow> windowListDropdown { get; set; } = new List<DropdownForWindow>();


        public async Task CreateSubElement(SubElementDto subElementDto)
        {
            subElementDto.WindowName = "Create";
            var result = await _httpClient.PostAsJsonAsync("api/SubElement/", subElementDto);
            await SetSubElement(result);

        }

        public async Task UpdateSubElement(SubElementDto subElementDto)
        {
            var result = await _httpClient.PutAsJsonAsync($"api/SubElement/{subElementDto.SubElementID}", subElementDto);
            await SetSubElement(result);
        }

        public async Task DeleteSubElement(int id)
        {
            var result = await _httpClient.DeleteAsync($"api/SubElement/{id}");
            await SetSubElement(result);
        }



        
        private async Task SetSubElement(HttpResponseMessage result)
        {
            try
            {
                var response = await result.Content.ReadFromJsonAsync<IEnumerable<SubElementDto>>();
                if (response != null)
                {
                    subElementDto = response;
                    _navigationManager.NavigateTo("/subElement");
                }
            }
            catch (Exception ex)
            {

               throw ex;
            }
           
        }

        public async Task GetDropdown()
        {
            var result = await _httpClient.GetFromJsonAsync<List<DropdownForWindow>>("api/SubElement/windowListDropdown");
            if (result != null)
                windowListDropdown = result;
        }
    }
}
