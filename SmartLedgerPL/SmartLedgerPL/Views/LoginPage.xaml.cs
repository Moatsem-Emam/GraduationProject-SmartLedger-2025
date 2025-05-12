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

        //private void LoginPage_Activated(object sender, WindowActivatedEventArgs e)
        //{
        //    if (!_hasAnimated && e.WindowActivationState != WindowActivationState.Deactivated)
        //    {
        //        _hasAnimated = true;

        //        var welcomeTextAnimation = new DoubleAnimation
        //        {
        //            From = -200,
        //            To = 0,
        //            Duration = new Duration(TimeSpan.FromSeconds(0.8))
        //        };

        //        Storyboard.SetTarget(welcomeTextAnimation, WelcomeText);
        //        Storyboard.SetTargetProperty(welcomeTextAnimation, "(UIElement.RenderTransform).(TranslateTransform.X)");

        //        var storyboard = new Storyboard();
        //        storyboard.Children.Add(welcomeTextAnimation);
        //        storyboard.Begin();
        //    }
        //}

        //private void Page_Loaded(object sender, RoutedEventArgs e)
        //{
        //    if (_hasAnimated)
        //        return;

        //    _hasAnimated = true;

        //    // Ensure WelcomeText is loaded
        //    if (WelcomeText == null)
        //    {
        //        Debug.WriteLine("WelcomeText is null in Page_Loaded");
        //        return;
        //    }

        //    // Use DispatcherQueue to run animation on the UI thread
        //    var dispatcherQueue = Windows.System.DispatcherQueue.GetForCurrentThread();
        //    if (dispatcherQueue == null)
        //    {
        //        Debug.WriteLine("DispatcherQueue is null in Page_Loaded");
        //        return;
        //    }

        //    dispatcherQueue.TryEnqueue(() =>
        //    {
        //        try
        //        {
        //            var welcomeTextAnimation = new DoubleAnimation
        //            {
        //                From = -200,
        //                To = 0,
        //                Duration = new Duration(TimeSpan.FromSeconds(0.8)),
        //                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseOut }
        //            };

        //            Storyboard.SetTarget(welcomeTextAnimation, WelcomeText);
        //            Storyboard.SetTargetProperty(welcomeTextAnimation, "(UIElement.RenderTransform).(TranslateTransform.X)");

        //            var storyboard = new Storyboard();
        //            storyboard.Children.Add(welcomeTextAnimation);
        //            storyboard.Begin();
        //        }
        //        catch (Exception ex)
        //        {
        //            Debug.WriteLine($"Animation failed: {ex.Message}");
        //        }
        //    });
        //}

        //private async void ShowMessageAsync(string title, string message)
        //{
        //    var dialog = new ContentDialog
        //    {
        //        Title = title,
        //        Content = message,
        //        CloseButtonText = "OK",
        //        XamlRoot = this.Content.XamlRoot,  // مهم جدًا لتشغيل ContentDialog في WinUI 3
        //    };

        //    await dialog.ShowAsync();
        //}

        //private void Window_Loaded(object sender, RoutedEventArgs e)
        //{
        //    var localSettings = ApplicationData.Current.LocalSettings;

        //    if (localSettings.Values.ContainsKey("SavedEmail"))
        //    {
        //        EmailTextBox.Text = localSettings.Values["SavedEmail"].ToString();
        //        PasswordBox.Password = localSettings.Values["SavedPassword"].ToString();
        //        //RememberMeCheckBox.IsChecked = true;
        //    }

        //    // لتشغيل الأنيميشن مثلاً:
        //    var sb = (Storyboard)Application.Current.Resources["WelcomeTextAnimation"];
        //    sb?.Begin();
        //}


        //private void LoginButton_Click(object sender, RoutedEventArgs e)
        //{
        //    string email = EmailTextBox.Text;
        //    string password = PasswordBox.Password;

        //    // مثال: تحقق من صحة الدخول (هنا logic بسيط كمثال)
        //    if (email == "test@example.com" && password == "123456")
        //    {
        //        // لو تم التحديد على Remember Me، خزّن البيانات
        //        if (RememberMeCheckBox.IsChecked == true)
        //        {
        //            var localSettings = ApplicationData.Current.LocalSettings;
        //            localSettings.Values["SavedEmail"] = email;
        //            localSettings.Values["SavedPassword"] = password;
        //        }
        //        else
        //        {
        //            // لو مش متعلم عليها، احذفهم
        //            var localSettings = ApplicationData.Current.LocalSettings;
        //            localSettings.Values.Remove("SavedEmail");
        //            localSettings.Values.Remove("SavedPassword");
        //        }

        //        // تابع باقي اللوجيك: فتح صفحة جديدة أو رسالة نجاح
        //        ShowMessageAsync("Success", "Login successful!");
        //    }
        //    else
        //    {
        //        ShowMessageAsync("Error", "Invalid credentials.");
        //    }
        //}

        //private void GoToRegisterPage_Click(object sender, RoutedEventArgs e)
        //{
        //    Frame rootFrame = new Frame();
        //    //rootFrame.Navigate(typeof(RegisterPage));
        //    this.Content = rootFrame;
        //}
    }
}
