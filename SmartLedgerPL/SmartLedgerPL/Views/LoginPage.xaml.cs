using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;

using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using Windows.Storage;
using SmartLedgerPL.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace SmartLedgerPL.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginPage : Page
    {
        private bool _hasAnimated = false;

        public LoginViewModel ViewModel { get; }
        public LoginPage()
        {
            this.InitializeComponent();
            //this.Activated += LoginPage_Activated;
            ViewModel = App.Services.GetRequiredService<LoginViewModel>();
            this.DataContext = ViewModel;
        }

        private void RootGrid_Loaded(object sender, RoutedEventArgs e)
        {
            // Placeholder for future use
        }

    }
}
