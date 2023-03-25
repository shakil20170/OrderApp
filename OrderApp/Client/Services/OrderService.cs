using Microsoft.AspNetCore.Components;
using OrderApp.Client.Pages;
using OrderApp.Client.Services.Contracts;
using OrderApp.Shared.Dtos;
using System.Net.Http;
using System.Net.Http.Json;

namespace OrderApp.Client.Services
{
    public class OrderService : IOrderService
    {
        private readonly HttpClient _httpClient;
        private readonly NavigationManager _navigationManager;
        public OrderService(HttpClient httpClient, NavigationManager navigationManager)
        {
            _httpClient= httpClient;
            _navigationManager = navigationManager;
        }

        public async Task<IEnumerable<OrderDto>> GetOrders()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/Order");

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    { 
                        return Enumerable.Empty<OrderDto>();
                    }

                    return await response.Content.ReadFromJsonAsync<IEnumerable<OrderDto>>();
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

        public async Task<OrderDto> GetOrder(int Id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/Order/{Id}");
                //return orders;

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return default(OrderDto);
                    }

                    return await response.Content.ReadFromJsonAsync<OrderDto>();
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

        public IEnumerable<OrderDto> orderDtos { get; set; } = new List<OrderDto>();

        public async Task CreateOrder(OrderDto order)
        {
            var result = await _httpClient.PostAsJsonAsync("api/Order/",order);
            await SetOrders(result);

        }

        public async Task UpdateOrder(OrderDto order)
        {
            var result = await _httpClient.PutAsJsonAsync($"api/Order/{order.OrderId}", order);
            await SetOrders(result);
        }

        public async Task DeleteOrder(int id)
        {
            var result = await _httpClient.DeleteAsync($"api/Order/{id}");
            await SetOrders(result);

        }

        private async Task SetOrders(HttpResponseMessage result)
        {
            var response = await result.Content.ReadFromJsonAsync<IEnumerable<OrderDto>>();
            if (response != null)
            {
                orderDtos = response;
                _navigationManager.NavigateTo("/");
            }
        }
    }
}
