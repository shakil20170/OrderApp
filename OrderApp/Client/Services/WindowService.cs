using BlazorFullStackCrud.Shared;
using Microsoft.AspNetCore.Components;
using OrderApp.Client.Pages;
using OrderApp.Client.Services.Contracts;
using OrderApp.Shared.Dtos;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using static System.Net.WebRequestMethods;

namespace OrderApp.Client.Services
{
    public class WindowService : IWindowService
    {
        private readonly HttpClient _httpClient;
        private readonly NavigationManager _navigationManager;
        public WindowService(HttpClient httpClient, NavigationManager navigationManager)
        {
            _httpClient = httpClient;
            _navigationManager = navigationManager;
        }

        public async Task<IEnumerable<WindowDto>> GetAllWindows()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/Window");

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return Enumerable.Empty<WindowDto>();
                    }

                    return await response.Content.ReadFromJsonAsync<IEnumerable<WindowDto>>();
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

        public async Task<WindowDto> GetWindow(int Id, int orderId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/Window/{Id}/{orderId}");
                //return orders;

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return default(WindowDto);
                    }

                    return await response.Content.ReadFromJsonAsync<WindowDto>();
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

        public IEnumerable<WindowDto> windowDtos { get; set; } = new List<WindowDto>();
        public List<DropdownForOrder> orderListDropdown { get; set; } = new List<DropdownForOrder>();

        public async Task CreateWindow(WindowDto window)
        {
            window.Name = "Create";
            var result = await _httpClient.PostAsJsonAsync($"api/Window/", window);
            await SetOrders(result);

        }

        public async Task UpdateWindow(WindowDto window)
        {
            var result = await _httpClient.PutAsJsonAsync($"api/Window/{window.WindowID}", window);
            await SetOrders(result);
        }

        public async Task DeleteWindow(int id)
        {
            var result = await _httpClient.DeleteAsync($"api/Window/{id}");
            await SetOrders(result);

        }


        public async Task GetDropdown()
        {
            var result = await _httpClient.GetFromJsonAsync<List<DropdownForOrder>>("api/Window/orderListDropdown");
            if (result != null)
                orderListDropdown = result;
        }




        private async Task SetOrders(HttpResponseMessage result)
        {
            try
            {
                var response = await result.Content.ReadFromJsonAsync<IEnumerable<WindowDto>>();

                if (response != null)
                {
                    windowDtos = response;
                    _navigationManager.NavigateTo("/window");
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}
