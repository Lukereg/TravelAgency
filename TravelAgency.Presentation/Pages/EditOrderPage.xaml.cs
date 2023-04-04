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
using TravelAgency.Application.Models.Order;
using TravelAgency.Application.Services.OrderService;
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
            var tourName = TourNameTextBox.Text;
            var description = DescriptionTextBox.Text;
            var price = decimal.Parse(PriceTextBox.Text);
            var startDate = StartDatePicker.Date.DateTime;
            var endDate = EndDatePicker.Date.DateTime;

            var order = new UpdateOrderDto
            {
                TourName = tourName,
                Description = description,
                Price = price,
                StartDate = startDate,
                EndDate = endDate,
            };

            await _orderService.UpdateOrder(order, _order.Id);

            var page = new OrdersPage(_window);
            _window.Content = page;

        }
    }
}
