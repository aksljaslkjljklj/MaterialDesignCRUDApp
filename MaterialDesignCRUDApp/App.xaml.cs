using MaterialDesignCRUDApp.Models;
using MaterialDesignCRUDApp.Services;
using MaterialDesignCRUDApp.ViewModels;
using MaterialDesignCRUDApp.ViewModels.Dialogs;
using MaterialDesignCRUDApp.Views;
using MaterialDesignCRUDApp.Views.Dialogs;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace MaterialDesignCRUDApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost _host;
        private readonly MainWindow _window;
        public App()
        {
            _host = Host.CreateDefaultBuilder()
                .ConfigureServices(services =>
                {
                    services.AddSingleton<INavigationService, NavigationService>();
                    services.AddSingleton<IDialogService, DialogService>();

                    services.AddTransient<DialogWindow>();
                    services.AddSingleton<MainWindow>();

                    services.AddSingleton<MainViewModel>();
                    services.AddTransient<ProductListViewModel>();
                    services.AddTransient<ProductItemViewModel>();
                    services.AddTransient<CategoryListViewModel>();
                    services.AddTransient<MessageDialogViewModel>();
                    services.AddTransient<CategoryItemViewModel>();

                    services.AddSingleton<IGenericDataService<Product>, ProductDataService>();
                    services.AddSingleton<IGenericDataService<Category>, GenericDataService<Category>>();
                })
                .Build();
            _window = _host.Services.GetRequiredService<MainWindow>();
            _window.DataContext = _host.Services.GetRequiredService<MainViewModel>();
            _window.ShowDialog();
        }
    }
}
