using System;

namespace MaterialDesignCRUDApp.Services
{
    public interface INavigationService
    {
        event EventHandler<NavigationEventArgs> ViewModelChanged;

        void Navigate(Type type, object parameter = null);
    }
}