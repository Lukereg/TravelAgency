// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using TravelAgency.Application.Models.Order;
using TravelAgency.Application.Services.CustomerService;
using TravelAgency.Application.Services.OrderService;
using TravelAgency.Domain.Entities;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace TravelAgency.Presentation
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class OrdersPage : Page
    {
        private Window _window;
        private IEnumerable<GetOrderDto> _orders = new List<GetOrderDto>();
        private readonly OrderService _orderService;
        private readonly CustomerService _customerService;

        public OrdersPage(Window window)
        {
            _orderService = new OrderService();
            _customerService = new CustomerService();
            _window = window;
            this.InitializeComponent();
            LoadOrders();
        }

        public void OnAddOrderButtonClick(object sender, RoutedEventArgs e)
        {
            var page = new AddOrderPage(_window);
            _window.Content = page;
        }

        private async void LoadOrders()
        {
            _orders = await _orderService.GetAllOrders();
            OrdersListView.ItemsSource = _orders;
        }

        public async void OnDeleteButtonClick(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            GetOrderDto getOrderDto = (GetOrderDto)btn.DataContext;

            await _orderService.DeleteOrder(getOrderDto.Id);
        }

        private async void OrdersListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (OrdersListView.SelectedItem != null)
            {
                var selectedItem = (GetOrderDto)OrdersListView.SelectedItem;
                var customerSelectedOrder = await _customerService.GetCustomerById(selectedItem.CustomerId);
                
                
                string message = $"First Name: {customerSelectedOrder.FirstName}\n" +
                                 $"Last Name: {customerSelectedOrder.LastName}\n" +
                                 $"Phone Number: {customerSelectedOrder.PhoneNumber}\n" +
                                 $"Address: {customerSelectedOrder.Address}";
                
                var dialog = new ContentDialog
                {
                    Title = "Customer",
                    Content = message,
                    CloseButtonText = "OK"
                };
                dialog.XamlRoot = this.XamlRoot;
                await dialog.ShowAsync();

                OrdersListView.SelectedItem = null;
            }

        }

        
    }
}
