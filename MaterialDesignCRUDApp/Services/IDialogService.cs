using System;

namespace MaterialDesignCRUDApp.Services
{
    public interface IDialogService
    {
        bool IsOpen { get; set; }

        void Close(bool dialogResult,object parameter = null);
        void ShowDialog(Type type, object parameter = null,Action<bool, object> callback = null);
    }
}