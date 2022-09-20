using MaterialDesignCRUDApp.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterialDesignCRUDApp.Services
{
    public class NavigationService : INavigationService
    {
        public event EventHandler<NavigationEventArgs> ViewModelChanged;

        private readonly IHost _host;

        public NavigationService(IHost host)
        {
            _host = host;
        }

        public void Navigate(Type type, object parameter = null)
        {
            BaseViewModel viewModel = _host.Services.GetService(type) as BaseViewModel;
            if (viewModel == null)
                throw new ArgumentNullException($"No service for type {type.FullName} has been registered.");
            viewModel.OnNavigatedTo(parameter);
            ViewModelChanged?.Invoke(this, new NavigationEventArgs
            {
                ViewModel = viewModel,
                Parameters = parameter
            });
        }

    }
}
