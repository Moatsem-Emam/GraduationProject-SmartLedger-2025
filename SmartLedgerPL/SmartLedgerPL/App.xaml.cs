using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml.Shapes;
using SmartLedger.Application.Interfaces.IAppDb;
using SmartLedger.Application.Interfaces.IServices;
using SmartLedger.Application.Mapping;
using SmartLedger.Domain.Interfaces.IRepositories;
using SmartLedger.Domain.Interfaces.IUnitOfWork;
using SmartLedger.Infrastructure.Data;
using SmartLedger.Infrastructure.Repositories;
using SmartLedger.Infrastructure.Services;
using SmartLedgerPL.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace SmartLedgerPL
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public static IHost AppHost { get; private set; }
        public static IServiceProvider Services => AppHost.Services;
        public IConfiguration Configuration { get; }

        public App()
        {
            AppHost = Host.CreateDefaultBuilder()
                .ConfigureAppConfiguration(config =>
                {
                    config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                })
                .ConfigureServices((context, services) =>
                {
                    ConfigureServices(context.Configuration, services);
                })
                .Build();
            
            InitializeComponent();
        }

        private void ConfigureServices(IConfiguration configuration, IServiceCollection services)
        {
            // DB Config
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            // Unit Of Work
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            // Repositories
            services.AddScoped<ILedgerRepository, LedgerRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            // Services
            services.AddScoped<IJournalService, JournalService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ICategoryService, CategoryService>();
            // Database Connection Establishing
            services.AddScoped<IAppDbContext>(provider => provider.GetRequiredService<AppDbContext>());
            // View Models
            services.AddTransient<JournalEntryViewModel>();
            // Auto Mapper
            services.AddAutoMapper(typeof(MappingProfile).Assembly);


        }

        public static MainWindow MainWindow { get; private set; } // Store main window reference
        /// <summary>
        /// Invoked when the application is launched.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            MainWindow = new MainWindow(); // Set global reference
            MainWindow.Activate();
        }

        //private Window m_window;
    }
}
