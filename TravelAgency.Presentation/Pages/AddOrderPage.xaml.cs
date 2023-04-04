// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TravelAgency.Application.Models.Customer;
using TravelAgency.Application.Models.Order;
using TravelAgency.Application.Services.CustomerService;
using TravelAgency.Application.Services.OrderService;
using TravelAgency.Application.Validators;

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
            var errors = new StringBuilder();
            decimal price;

            if (_selectedCustomer == null)
            {
                var dialog = new ContentDialog
                {
                    Title = "Error",
                    Content = "Please select a customer",
                    CloseButtonText = "OK"
                };
                dialog.XamlRoot = this.XamlRoot;
                await dialog.ShowAsync();
                return;
            }

            if (!decimal.TryParse(PriceTextBox.Text, out price))
            {
                var dialog = new ContentDialog
                {
                    Title = "Error",
                    Content = "Invalid price",
                    CloseButtonText = "OK"
                };
                dialog.XamlRoot = this.XamlRoot;
                await dialog.ShowAsync();
                return;
            }
                
            var customerId = _selectedCustomer.Id;

            var order = new AddOrderDto
            {
                TourName = TourNameTextBox.Text,
                Description = DescriptionTextBox.Text,
                Price = price,
                StartDate = StartDatePicker.Date.DateTime,
                EndDate = EndDatePicker.Date.DateTime,
                CustomerId = customerId,
                CreatorId = AppSettings.UserLoggedInId                
            };

            var validator = new AddOrderDtoValidator();
            var validationResult = validator.Validate(order);

            if (validationResult.IsValid)
            {
                await _orderService.AddOrder(order);
            }
            else
            {   
                foreach(var error in validationResult.Errors)
                {
                    errors.AppendLine(error.ErrorMessage);
                }

                var dialog = new ContentDialog
                {
                    Title = "Error",
                    Content = errors,
                    CloseButtonText = "OK"
                };
                dialog.XamlRoot = this.XamlRoot;
                await dialog.ShowAsync();
                return;
            }      
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
