using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MaterialDesignCRUDApp.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        private bool _IsBusy;

        public bool IsBusy
        {
            get { return _IsBusy; }
            set => SetProperty(ref _IsBusy, value);
        }

        private string _Title;

        public string Title
        {
            get { return _Title; }
            set => SetProperty(ref _Title, value);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public bool SetProperty<T>(ref T field, T value, [CallerMemberName] string property = null)
        {
            if (Equals(field, value))
                return false;
            field = value;
            OnPropertyChanged(property);
            return true;
        }

        public void OnPropertyChanged([CallerMemberName] string property = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        public virtual void OnNavigatedTo(object parameter) { }
    }
}
