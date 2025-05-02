using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLedgerPL.Helpers
{
    public static class HelperUtilities
    {
        // Methods to show a ContentDialog
        public static async Task ShowMessageDialogAsync(string message, string title)
        {
            ContentDialog dialog = new ContentDialog
            {
                Title = title,
                Content = message,
                CloseButtonText = "تمام",
                DefaultButton = ContentDialogButton.Close,
                XamlRoot = App.MainWindow.Content.XamlRoot, // Required in WinUI 3
                
            };

            await dialog.ShowAsync();
        }

        public static async Task<bool> ShowConfirmationDialogAsync(string message,string primaryBtn, string closeBtn)
        {
            ContentDialog dialog = new ContentDialog
            {
                Title = "Confirmation",
                Content = message,
                PrimaryButtonText = primaryBtn,
                CloseButtonText = closeBtn,
                DefaultButton = ContentDialogButton.Primary,
                XamlRoot = App.MainWindow.Content.XamlRoot // Required in WinUI 3
            };

            var result = await dialog.ShowAsync();
            return result == ContentDialogResult.Primary; // Returns true if PrimaryButtonText is clicked
        }

    }
}
