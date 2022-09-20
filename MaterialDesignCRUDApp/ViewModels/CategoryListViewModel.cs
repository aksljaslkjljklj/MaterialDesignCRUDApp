using MaterialDesignCRUDApp.Models;
using MaterialDesignCRUDApp.Services;
using MaterialDesignCRUDApp.ViewModels.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MaterialDesignCRUDApp.ViewModels
{
    public class CategoryListViewModel:BaseViewModel
    {
        private readonly IGenericDataService<Category> _categoryDataService;
        private readonly IDialogService _dialogService;

        public ObservableCollection<Category> Categories { get; set; }

        private Category _SelectedCategory;

        public Category SelectedCategory
        {
            get { return _SelectedCategory; }
            set
            {
                if(SetProperty(ref _SelectedCategory, value))
                {
                    EditCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public ICommand LoadCommand { get; set; }
        private async Task OnLoadCommandExecuted(object p) => await LoadCategories();
        private async Task LoadCategories()
        {
            var list = await _categoryDataService.GetListAsync();
            Categories.Clear();
            foreach (var item in list)
                Categories.Add(item);
        }

        public RelayCommandAsync AddCommand { get; set; }
        private async Task OnAddCommandExecuted(object p)
        {
            bool update = false;
            _dialogService.ShowDialog(typeof(CategoryItemViewModel), null, (dialogResult, p) =>
            {
                update = dialogResult;
            });

            if (update)
                await LoadCategories();
        }

        public RelayCommandAsync EditCommand { get; set; }
        private async Task OnEditCommandExecuted(object p)
        {
            bool update = false;
            _dialogService.ShowDialog(typeof(CategoryItemViewModel), SelectedCategory.Id, (dialogResult, p) =>
            {
                update = dialogResult;
            });

            if (update)
                await LoadCategories();
        }

        private bool CanEditCommandExecute(object p) => SelectedCategory != null;
        public ICommand DeleteCommand { get; set; }
        private async Task OnDeleteCommandExecuted(object p)
        {
            bool delete = false;
            _dialogService.ShowDialog(typeof(MessageDialogViewModel), new Tuple<string, string>("Delete product", $"Are you sure you want to delete {SelectedCategory.Name}?"), (dialogResult, p) =>
            {
                delete = dialogResult;
            });

            if (delete)
            {
                await _categoryDataService.RemoveAsync(SelectedCategory.Id);
                await LoadCategories();
            }
        }

        public CategoryListViewModel(IDialogService dialogService, IGenericDataService<Category> categoryDataService)
        {
            _dialogService = dialogService;
            _categoryDataService = categoryDataService;
            Categories = new ObservableCollection<Category>();
            LoadCommand = new RelayCommandAsync(OnLoadCommandExecuted, (p) => true);
            AddCommand = new RelayCommandAsync(OnAddCommandExecuted, (p)=>true);
            EditCommand = new RelayCommandAsync(OnEditCommandExecuted, (p)=>true);
            DeleteCommand = new RelayCommandAsync(OnDeleteCommandExecuted, (p) => true);

        }
    }
}
