using MaterialDesignCRUDApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MaterialDesignCRUDApp.ViewModels
{
    public class MainViewModel:BaseViewModel
    {
        private readonly INavigationService _navigationService;

        private BaseViewModel _CurrentViewModel;

        public BaseViewModel CurrentViewModel
        {
            get { return _CurrentViewModel; }
            set => SetProperty(ref _CurrentViewModel, value);
        }

        public ICommand NavigateCommand { get; set; }
        public void OnNavigateCommandExecuted(object parameter)
        {
            if(parameter is Type type)
            {
                _navigationService.Navigate(type,"Hello world!");
            }
        }

        public MainViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            _navigationService.ViewModelChanged += _navigationService_ViewModelChanged;
            NavigateCommand = new RelayCommand(OnNavigateCommandExecuted);
        }

        private void _navigationService_ViewModelChanged(object? sender, NavigationEventArgs e)
        {
            CurrentViewModel = e.ViewModel;
        }
    }
}
