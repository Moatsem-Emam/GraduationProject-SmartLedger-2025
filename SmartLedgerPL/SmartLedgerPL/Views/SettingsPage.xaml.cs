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
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace SmartLedgerPL.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SettingsPage : Page
    {
        public SettingsPage()
        {
            this.InitializeComponent();

            // Load saved theme on open
            string savedTheme = ApplicationData.Current.LocalSettings.Values["AppTheme"] as string;
            switch (savedTheme)
            {
                case "Light":
                    ThemeSelector.SelectedIndex = 0;
                    break;
                //case "Dark":
                //    ThemeSelector.SelectedIndex = 1;
                //    break;
                //default:
                //    ThemeSelector.SelectedIndex = 2;
                //    break;
            }
        }

        private void ThemeSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (ThemeSelector.SelectedIndex)
            {
                case 0:
                    SetTheme(ElementTheme.Light);
                    break;
                //case 1:
                //    SetTheme(ElementTheme.Dark);
                //    break;
                //case 2:
                //    SetTheme(ElementTheme.Default);
                //    break;
            }
        }

        private void SetTheme(ElementTheme theme)
        {
            if (App.MainWindow.Content is FrameworkElement rootElement)
            {
                rootElement.RequestedTheme = theme;
            }

            ApplicationData.Current.LocalSettings.Values["AppTheme"] = theme.ToString();
        }
    }

}
