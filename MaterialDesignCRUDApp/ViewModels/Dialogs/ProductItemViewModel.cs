using MaterialDesignCRUDApp.Models;
using MaterialDesignCRUDApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MaterialDesignCRUDApp.ViewModels.Dialogs
{
    public class ProductItemViewModel:BaseViewModel
    {
        private int id = -1;
        private readonly IDialogService _dialogService;
        private readonly IGenericDataService<Product> _productDataService;
        private readonly IGenericDataService<Category> _categoryDataService;

        private string _Name;
        public string Name
        {
            get { return _Name; }
            set => SetProperty(ref _Name, value);
        }

        private decimal _Price;

        public decimal Price
        {
            get { return _Price; }
            set => SetProperty(ref _Price, value);
        }

        private Category _Category;

        public Category Category
        {
            get { return _Category; }
            set => SetProperty(ref _Category, value);
        }

        public ObservableCollection<Category> Categories { get; set; }

        public ICommand CloseDialogCommand { get; set; }
        private void OnCloseDialogCommandExecuted(object parameter)
        {
            _dialogService.Close(false);
        }

        public ICommand SaveDialogCommand { get; set; }
        private async Task OnSaveDialogCommandExecuted(object parameter)
        {
            Product product = new Product
            {
                Name = Name,
                Price = Price,
                CategoryId = Category.Id
            };
            if(id == -1)
                await _productDataService.AddAsync(product);
            else
                await _productDataService.UpdateAsync(id, product);
            _dialogService.Close(true);
        }
        public ICommand LoadCommand { get; set; }
        private async Task OnLoadCommandExecuted(object p)
        {
            Categories.Clear();
            var list = await _categoryDataService.GetListAsync();
            foreach (var item in list)
                Categories.Add(item);

            if (id > 0)
            {
                Product product = await _productDataService.GetAsync(id);
                id = product.Id;
                Name = product.Name;
                Price = product.Price;
                Category = Categories.FirstOrDefault(c => c.Id == product.CategoryId);
            }
            else
            {
                id = -1;
                Name = string.Empty;
                Category = null;
                Price = 0;
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

        public ProductItemViewModel(IDialogService dialogService, IGenericDataService<Product> productDataService, IGenericDataService<Category> categoryDataService)
        {
            _dialogService = dialogService;
            _productDataService = productDataService;
            _categoryDataService = categoryDataService;
            Categories = new ObservableCollection<Category>();
            LoadCommand = new RelayCommandAsync(OnLoadCommandExecuted, (p) => true);
            CloseDialogCommand = new RelayCommand(OnCloseDialogCommandExecuted);
            SaveDialogCommand = new RelayCommandAsync(OnSaveDialogCommandExecuted, (p) => true);
            Title = "Product";
        }
    }
}
