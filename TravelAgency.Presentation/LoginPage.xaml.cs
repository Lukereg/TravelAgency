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
using TravelAgency.Application.Models.User;
using TravelAgency.Application.Services.UserService;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace TravelAgency.Presentation
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginPage : Page
    {
        private readonly UserService _userService;
        private Window _window;

        public LoginPage(Window window)
        {
            _window = window;
            _userService = new UserService();
            this.InitializeComponent();
        }

        public async void OnLoginButtonClick(object sender, RoutedEventArgs e)
        {
            var username = UsernameTextBox.Text;
            var password = PasswordBox.Password;

            var loginUser = new LoginUserDto
            {
                Login = username,
                Password = password
            };

            var result = await _userService.LoginUser(loginUser);

            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            localSettings.Values["IsUserLoggedIn"] = result;

            if (result)
            {

            }
            else
            {
                var dialog = new ContentDialog
                {
                    Title = "Login error",
                    Content = "Incorrect login details!",
                    CloseButtonText = "OK"
                };
                dialog.XamlRoot = this.XamlRoot;
                await dialog.ShowAsync();
            }
        }
    }
}
