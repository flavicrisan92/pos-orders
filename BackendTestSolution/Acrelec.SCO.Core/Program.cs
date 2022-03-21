﻿using Acrelec.SCO.Core.Helpers;
using Acrelec.SCO.Core.Interfaces;
using Acrelec.SCO.Core.Managers;
using Acrelec.SCO.Core.Providers;
using Acrelec.SCO.DataStructures;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Acrelec.SCO.Core
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("SCO - Self Check Out System");

            var builder = new ConfigurationBuilder();
            BuildConfig(builder);
            var serviceProvider = ConfigureServices(builder);

            //init
            IItemsProvider itemsProvider = new ItemsProvider(serviceProvider.GetService<IHttpClientFactory>(), serviceProvider.GetService<IConfiguration>());
            
            IOrderManager orderManager = new OrderManager(itemsProvider);

            //list POS items
            ListAllItems(itemsProvider);

            //todo - check if server is available for order injection
            var isServerAvailable = await itemsProvider.CheckServerAvailabilityAsync();
            if (isServerAvailable)
            {
                //todo - create an order containing the following items:
                //1*Coke
                //2*Water
                var newOrder = new Order();
                foreach (var itemToBeOrdered in GetItemsToBeOrdered(itemsProvider))
                {
                    newOrder.AddOrderItems(itemToBeOrdered.Key, itemToBeOrdered.Value);
                }
                //...

                //inject the order to POS
                Customer customer = GetCustomer();
                var assignedOrderNumber = await orderManager.InjectOrderAsync(newOrder, customer);

                if (!string.IsNullOrEmpty(assignedOrderNumber) == true)
                    Console.WriteLine("Order injected with success");
                else
                    Console.WriteLine("Error injecting order");

                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Server not available");
            }
        }

        private static Customer GetCustomer()
        {
            return new Customer() { Address = "Bucharest", Firstname = "John" };
        }

        /// <summary>
        /// list in Console all items (with all their properties)
        /// </summary>
        private static void ListAllItems(IItemsProvider itemsProvider)
        {
            //todo - list items and for each item a short code generated by the POSItemExtensions
            foreach (var item in itemsProvider.AllPOSItems)
            {
                Console.WriteLine($"{item.GetShortCode()}: {item.ItemCode} {item.Name} {item.IsAvailable} {item.UnitPrice}");
            }
        }

        private static Dictionary<POSItem, int> GetItemsToBeOrdered(IItemsProvider itemsProvider)
        {
            return new Dictionary<POSItem, int>
            {
                { itemsProvider.AvailablePOSItems.FirstOrDefault(q => q.ItemCode == "100"), 1 },
                { itemsProvider.AvailablePOSItems.FirstOrDefault(q => q.ItemCode == "200"), 2 }
            };
        }

        private static void BuildConfig(ConfigurationBuilder builder)
        {
            builder.SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        }

        private static IServiceProvider ConfigureServices(ConfigurationBuilder builder)
        {
            var host =  Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddHttpClient("ItemsProvider", client =>
                    {
                        client.BaseAddress = new Uri(builder.Build().GetValue<string>("itemsProviderBaseUrl"));
                    });
                }).Build();

            return host.Services;
        }

    }
}
