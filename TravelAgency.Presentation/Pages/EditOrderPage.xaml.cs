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
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using TravelAgency.Application.Models.Order;
using TravelAgency.Application.Services.OrderService;
using TravelAgency.Application.Validators;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace TravelAgency.Presentation.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class EditOrderPage : Page
    {
        private Window _window;
        private GetOrderDto _order;
        private readonly IOrderService _orderService;

        public EditOrderPage(Window window, GetOrderDto order)
        {
            _order= order;
            _orderService = new OrderService();
            this.InitializeComponent();
            FormInitialize();
            _window = window;
        }

        private void FormInitialize()
        {
            TourNameTextBox.Text = _order.TourName;
            DescriptionTextBox.Text = _order.Description;
            PriceTextBox.Text = _order.Price.ToString();
            StartDatePicker.Date = new DateTimeOffset(_order.StartDate.Year, _order.StartDate.Month, _order.StartDate.Day, 0, 0, 0, TimeSpan.Zero);
            EndDatePicker.Date = new DateTimeOffset(_order.EndDate.Year, _order.EndDate.Month, _order.EndDate.Day, 0, 0, 0, TimeSpan.Zero);
        }

        public void OnCancelButtonClick(object sender, RoutedEventArgs e)
        {
            var page = new OrdersPage(_window);
            _window.Content = page;
        }

        public async void OnUpdateButtonClick(object sender, RoutedEventArgs e)
        {

            var errors = new StringBuilder();
            decimal price;

            try
            {    
                if (!decimal.TryParse(PriceTextBox.Text, out price))
                    throw new FormatException("Invalid price");

                var order = new UpdateOrderDto
                {
                    TourName = TourNameTextBox.Text,
                    Description = DescriptionTextBox.Text,
                    Price = price,
                    StartDate = StartDatePicker.Date.DateTime,
                    EndDate = EndDatePicker.Date.DateTime,
                };

                var validator = new UpdateOrderDtoValidator();
                var validationResult = validator.Validate(order);

                if (validationResult.IsValid)
                {
                    await _orderService.UpdateOrder(order, _order.Id);

                    var page = new OrdersPage(_window);
                    _window.Content = page;
                }
                else
                {
                    foreach (var error in validationResult.Errors)
                        errors.AppendLine(error.ErrorMessage);

                    throw new ValidationException(errors.ToString());
                }
            }

            catch (Exception ex)
            {
                var dialog = new ContentDialog
                {
                    Title = "Error",
                    Content = ex.Message,
                    CloseButtonText = "OK"
                };
                dialog.XamlRoot = this.XamlRoot;
                await dialog.ShowAsync();
            }
        }
    }
}
