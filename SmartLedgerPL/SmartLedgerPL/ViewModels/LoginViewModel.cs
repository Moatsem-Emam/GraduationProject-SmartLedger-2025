using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using SmartLedger.Application.Interfaces.IServices;
using SmartLedger.Infrastructure.Services;
using SmartLedgerPL.Helpers;
using SmartLedgerPL.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Windows.Web.Syndication;

namespace SmartLedgerPL.ViewModels
{
    public partial class LoginViewModel:ObservableObject
    {
        public IRelayCommand LoginCommand { get; }
        private readonly IAuthService _authService;
        private readonly INavigationService _navigationService;
        private readonly HelperUtilities _helper;


        [ObservableProperty]
        private string email;

        [ObservableProperty]
        private string password;

        //[ObservableProperty]
        //private bool isLoggingIn;

        public LoginViewModel(IAuthService authService, INavigationService navigationService, HelperUtilities helper)
        {
            _authService = authService;
            _navigationService = navigationService;
            _helper = helper;
            LoginCommand = new AsyncRelayCommand(Login);
        }

        public NavigationView NavView => App.MainWindow.NavViewPublic;
        private async Task Login()
        {
            //IsLoggingIn = true; // Start spinner
            try
            {
                var user = await _authService.Authenticate(Email, Password);
                if (user != null)
                {
                    SessionManager.SetUser(user);

                    // Show welcome
                    //await _helper.ShowMessageDialogAsync($"Welcome, {user.Role}", "Login Successful");

                    // تحديث MainWindow.NavigationView حسب الدور
                    var window = (MainWindow)App.MainWindow;
                    await window.UpdateNavViewByRoleAsync();

                    // Navigate
                    if (user.Role == "Admin")
                    {
                        window.ContentFramePublic.Navigate(typeof(ReportPage));
                        // عشان نفوكس علي الحاجة اللي اتنقلنا ليها
                        _helper.FocusOn("ReportPage");

                    }
                    //else
                    //    window.ContentFramePublic.Navigate(typeof(UserHomePage)); // إذا عندك صفحة للمستخدم العادي
                }
                else
                {
                    await _helper.ShowMessageDialogAsync("Invalid Email or Password", "Login Failed");
                }
            }
            catch (Exception ex)
            {

                await _helper.ShowMessageDialogAsync("Check the service of SQLSERVER might be closed", "Server NotFound");
            }
            //finally
            //{
            //    IsLoggingIn = false; // Stop spinner
            //}

        }

    }
}
