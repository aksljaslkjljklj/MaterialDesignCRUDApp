using MaterialDesignCRUDApp.Models;
using MaterialDesignCRUDApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MaterialDesignCRUDApp.ViewModels.Dialogs
{
    public class CategoryItemViewModel:BaseViewModel
    {
        private int id = -1;
        private readonly IDialogService _dialogService;
        private readonly IGenericDataService<Category> _categoryDataService;

        private string _Name;
        public string Name
        {
            get { return _Name; }
            set => SetProperty(ref _Name, value);
        }

        private string _Description;

        public string Description
        {
            get { return _Description; }
            set => SetProperty(ref _Description, value);
        }
        public ICommand CloseDialogCommand { get; set; }
        private void OnCloseDialogCommandExecuted(object parameter)
        {
            _dialogService.Close(false);
        }

        public ICommand SaveDialogCommand { get; set; }
        private async Task OnSaveDialogCommandExecuted(object parameter)
        {
            Category category = new Category
            {
                Name = Name,
                Description = Description
            };
            if (id == -1)
                await _categoryDataService.AddAsync(category);
            else
                await _categoryDataService.UpdateAsync(id, category);
            _dialogService.Close(true);
        }

        public ICommand LoadCommand { get; set; }
        private async Task OnLoadCommandExecuted(object p)
        {
            if (id > 0)
            {
                Category category = await _categoryDataService.GetAsync(id);
                id = category.Id;
                Name = category.Name;
                Description = category.Description;
            }
            else
            {
                id = -1;
                Name = string.Empty;
                Description = string.Empty;
            }
        }

        public override void OnNavigatedTo(object parameter)
        {
            var p = parameter as int?;
            if (p.HasValue)
                id = p.Value;
            else
                id = -1;
        }

        public CategoryItemViewModel(IDialogService dialogService,IGenericDataService<Category> categoryDataService)
        {
            _dialogService = dialogService;
            _categoryDataService = categoryDataService;
            CloseDialogCommand = new RelayCommand(OnCloseDialogCommandExecuted);
            SaveDialogCommand = new RelayCommandAsync(OnSaveDialogCommandExecuted, (p) => true);
            LoadCommand = new RelayCommandAsync(OnLoadCommandExecuted,(p)=> true);
            Title = "Category";
        }
    }
}
