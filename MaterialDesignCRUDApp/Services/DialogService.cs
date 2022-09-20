using MaterialDesignCRUDApp.ViewModels;
using MaterialDesignCRUDApp.Views;
using MaterialDesignCRUDApp.Views.Dialogs;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterialDesignCRUDApp.Services
{
    public class DialogService : IDialogService
    {
        private readonly IHost _host;

        private DialogWindow dialogWindow;
        private Action<bool,object> _callback;

        public bool IsOpen { get; set; }

        public DialogService(IHost host)
        {
            _host = host;
        }

        public void ShowDialog(Type type, object parameter = null, Action<bool,object> callback = null)
        {
            BaseViewModel viewModel = _host.Services.GetService(type) as BaseViewModel;
            if (viewModel == null)
                throw new ArgumentNullException($"No service for type {type.FullName} has been registered.");
            dialogWindow = _host.Services.GetRequiredService<DialogWindow>();
            viewModel.OnNavigatedTo(parameter);
            _callback = callback;
            dialogWindow.DataContext = viewModel;
            IsOpen = true;
            dialogWindow.Owner = _host.Services.GetRequiredService<MainWindow>();
            dialogWindow.ShowDialog();
        }

        public void Close(bool dialogResult,object parameter = null)
        {
            if (!IsOpen)
                return;
            dialogWindow.DialogResult = true;
            dialogWindow.Close();
            IsOpen = false;
            _callback?.Invoke(dialogResult,parameter);
        }
    }
}
