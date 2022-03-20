using Acrelec.SCO.Core.Interfaces;
using Acrelec.SCO.Core.Model.RestExchangedMessages;
using Acrelec.SCO.DataStructures;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Acrelec.SCO.Core.Providers
{
    /// <summary>
    /// class providing the list of items as retrieved from POS system
    /// </summary>
    public class ItemsProvider : IItemsProvider
    {
        private readonly HttpClient _httpClient;
        private List<POSItem> _posItems;

        public List<POSItem> AllPOSItems => _posItems;

        public List<POSItem> AvailablePOSItems => _posItems.Where(q => q.IsAvailable).ToList();

        /// <summary>
        /// constructor
        /// </summary>
        public ItemsProvider(IHttpClientFactory clientFactory)
        {
            _httpClient = clientFactory.CreateClient("ItemsProvider");
            _posItems = new List<POSItem>();
            LoadItemsFromPOS();
        }

        /// <summary>
        /// retrieving items from POS is a simple parse of a json
        /// </summary>
        public void LoadItemsFromPOS()
        {
            //todo - implement the code to load items from Data\ContentItems.json file
            using (StreamReader r = new("Data/ContentItems.json"))
            {
                string json = r.ReadToEnd();

                var items = JsonConvert.DeserializeObject<List<POSItem>>(json);
                if (items != null)
                {
                    _posItems = items;
                }
            }
        }

        //todo - implement missing methods of interface
        public async Task<bool> CheckServerAvailabilityAsync()
        {
            var httpResponseMessage = await _httpClient.GetAsync("/api-sco/v1/Availability");
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                string responseBody = await httpResponseMessage.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<CheckAvailabilityResponse>(responseBody);
                return result.CanInjectOrders;
            }
            return false;
        }

        public async Task<string> SendOrderAsync(Order order, Customer customer)
        {
            InjectOrderRequest injectOrderRequest = new()
            {
                Order = order,
                Customer = customer
            };

            var stringContent = new StringContent(JsonConvert.SerializeObject(injectOrderRequest), Encoding.UTF8, "application/json");
            var httpResponseMessage = await _httpClient.PostAsync("/api-sco/v1/InjectOrder", stringContent);
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var responseBody = await httpResponseMessage.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<InjectOrderResponse>(responseBody);
                return result.OrderNumber;
            }
            else
            {
                var errorMessage = await httpResponseMessage.Content.ReadAsStringAsync();
                Console.WriteLine(errorMessage);
                return null;
            }
        }
    }
}
