using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using SmartLedgerPL.Helpers;
using SmartLedgerPL.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace SmartLedgerPL
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public Frame ContentFramePublic => ContentFrame;
        public NavigationView NavViewPublic => NavView;

        private readonly HelperUtilities _helper;
        public MainWindow()
        {

            this.InitializeComponent();
            _helper = App.Services.GetRequiredService<HelperUtilities>();
            NavView.BackRequested += NavView_BackRequested;


        }

        private bool _isNavigating = false;

        private async void NavView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            if (_isNavigating)
                return;

            _isNavigating = true;
            

            string pageName = args.InvokedItemContainer?.Tag as string;
            bool confirm;
            switch (pageName)
            {
                case "JournalEntryPage":
                    ContentFrame.Navigate(typeof(JournalEntry));
                    break;
                case "LoginPage":
                    ContentFrame.Navigate(typeof(LoginPage));
                    break;
                case "ReportPage":
                    ContentFrame.Navigate(typeof(ReportPage));
                    break;
                case "LogoutPage":
                    confirm = await _helper.ShowConfirmationDialogAsync("متأكد انك عايز تعمل تسجيل خروج؟", "نعم", "الغاء");
                    if (confirm)
                    {
                        SessionManager.Logout();
                        await UpdateNavViewByRoleAsync();
                        ContentFrame.Navigate(typeof(LoginPage));
                        _helper.FocusOn("LoginPage");
                    }

                    break;
                default:
                    break;
            }

            NavView.IsPaneOpen = false;
            
            // Give time for navigation to finish before allowing another
            await Task.Delay(500);
            _isNavigating = false;
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            await UpdateNavViewByRoleAsync();
        }

        public async Task UpdateNavViewByRoleAsync()
        {
            var user = SessionManager.CurrentUser;
            bool isLoggedIn = user != null;

            // تحكم في العناصر حسب الدور
            foreach (var item in NavView.MenuItems.OfType<NavigationViewItem>())
            {
                if ((string)item.Tag == "LoginPage")
                    item.IsEnabled = !isLoggedIn;

                if ((string)item.Tag == "LogoutPage")
                    item.IsEnabled = isLoggedIn;

                if ((string)item.Tag == "JournalEntryPage" || (string)item.Tag == "RequestPage" || (string)item.Tag == "NotificationsPage" || (string)item.Tag == "ReportPage")
                {
                    item.IsEnabled = isLoggedIn && user?.Role == "Admin";
                }
            }

            // تفعيل زر تسجيل الخروج
            //var logoutItem = NavView.MenuItems
            //    .OfType<NavigationViewItem>()
            //    .FirstOrDefault(i => (string)i.Content == "تسجيل الخروج");

            //if (logoutItem != null)
            //    logoutItem.IsEnabled = isLoggedIn;
        }

        private void NavView_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
        {
            if (ContentFrame.BackStack.LastOrDefault().SourcePageType.Name == "LoginPage")
                return;
            
            // تحقق إذا كان يمكن الرجوع في الـ Frame
            if (ContentFrame.CanGoBack)
            {
                ContentFrame.GoBack();
            }
        }
    }
}
