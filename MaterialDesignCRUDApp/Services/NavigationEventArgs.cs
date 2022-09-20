using MaterialDesignCRUDApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterialDesignCRUDApp.Services
{
    public class NavigationEventArgs:EventArgs
    {
        public BaseViewModel ViewModel { get; set; }
        public object Parameters { get; set; }
    }
}
