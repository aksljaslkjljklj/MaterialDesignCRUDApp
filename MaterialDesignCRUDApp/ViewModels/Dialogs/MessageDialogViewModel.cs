using MaterialDesignCRUDApp.Models;
using MaterialDesignCRUDApp.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Linq;

namespace MaterialDesignCRUDApp.ViewModels.Dialogs
{
    public class MessageDialogViewModel:BaseViewModel
    {
		private readonly IDialogService _dialogService;

		private string _Message;

		public string Message
		{
			get { return _Message; }
			set => SetProperty(ref _Message, value);
		}

		public override void OnNavigatedTo(object parameter)
		{
			var p = parameter as Tuple<string, string>;
			if (p != null)
			{
				Title = p.Item1;
				Message = p.Item2;
			}

		}
        public ICommand CloseDialogCommand { get; set; }
        private void OnCloseDialogCommandExecuted(object parameter)
        {
            _dialogService.Close(false);
        }

        public ICommand SaveDialogCommand { get; set; }
        private void OnSaveDialogCommandExecuted(object parameter)
        {
            _dialogService.Close(true);
        }

        public MessageDialogViewModel(IDialogService dialogService)
		{
			_dialogService = dialogService;
			SaveDialogCommand = new RelayCommand(OnSaveDialogCommandExecuted);
			CloseDialogCommand = new RelayCommand(OnCloseDialogCommandExecuted);
		}
	}
}
