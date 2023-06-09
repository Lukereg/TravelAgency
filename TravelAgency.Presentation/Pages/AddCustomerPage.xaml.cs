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
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using TravelAgency.Application.Models.Customer;
using TravelAgency.Application.Services.CustomerService;
using TravelAgency.Application.Validators;
using TravelAgency.Domain.Entities;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace TravelAgency.Presentation.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AddCustomerPage : Page
    {
        private Window _window;
        private readonly ICustomerService _customerService;

        public AddCustomerPage(Window window)
        {
            _window = window;
            _customerService = new CustomerService();
            this.InitializeComponent();
        }

        public async void OnAddButtonClick(object sender, RoutedEventArgs e)
        {
            var customer = new AddCustomerDto
            {
                FirstName = FirstNameTextBox.Text,
                LastName = LastNameTextBox.Text,
                Address = AddressTextBox.Text,
                PhoneNumber = PhoneNumberTextBox.Text
            };


            var validator = new AddCustomerDtoValidator();
            var validationResult = validator.Validate(customer);

            if (validationResult.IsValid)
            {
                await _customerService.AddCustomer(customer);
            }
            else
            {
                var errors = new StringBuilder();

                foreach (var error in validationResult.Errors)
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

            var page = new OrdersPage(_window);
            _window.Content = page;
        }

        public void OnCancelButtonClick(object sender, RoutedEventArgs e) 
        {
            var page = new OrdersPage(_window);
            _window.Content = page;
        }
    }
}
