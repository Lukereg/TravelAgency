// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows.Forms;
using TravelAgency.Application.Models.Customer;
using TravelAgency.Application.Models.Order;
using TravelAgency.Application.Services.CustomerService;
using TravelAgency.Application.Services.OrderService;
using TravelAgency.Application.Services.UserService;
using TravelAgency.Domain.Entities;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Networking;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace TravelAgency.Presentation
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AddOrderPage : Page
    {
        private Window _window;
        private readonly ICustomerService _customerService;
        private readonly IOrderService _orderService;
        private GetCustomerDto _selectedCustomer;
        private IEnumerable<GetCustomerDto> _customers = new List<GetCustomerDto>();

        public AddOrderPage(Window window)
        {
            _customerService = new CustomerService();
            _orderService = new OrderService();
            _window = window;
            this.InitializeComponent();
            LoadCustomers();
        }

        private async void LoadCustomers()
        {
            _customers = await _customerService.GetAllCustomers();
            CustomersListView.ItemsSource = _customers;
        }

        public async void OnAddButtonClick(object sender, RoutedEventArgs e)
        {
            var tourName = TourNameTextBox.Text;
            var description = DescriptionTextBox.Text;
            var price = decimal.Parse(PriceTextBox.Text);
            var startDate = StartDatePicker.Date.DateTime;
            var endDate = EndDatePicker.Date.DateTime;

            var customerId = _selectedCustomer.Id;

            var order = new AddOrderDto
            {
                TourName = tourName,
                Description = description,
                Price = price,
                StartDate = startDate,
                EndDate = endDate,
                CustomerId = customerId,
                CreatorId = AppSettings.UserLoggedInId
                 
            };

            await _orderService.AddOrder(order);
        }

        public void OnCancelButtonClick(object sender, RoutedEventArgs e)
        {
            var page = new OrdersPage(_window);
            _window.Content = page;
        }

        public void CustomersListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedCustomer = (GetCustomerDto)CustomersListView.SelectedItem;
        }
    }
}
