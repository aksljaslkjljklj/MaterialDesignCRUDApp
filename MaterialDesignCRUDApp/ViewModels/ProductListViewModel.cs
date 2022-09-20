using MaterialDesignCRUDApp.Models;
using MaterialDesignCRUDApp.Services;
using MaterialDesignCRUDApp.ViewModels.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MaterialDesignCRUDApp.ViewModels
{
    public class ProductListViewModel:BaseViewModel
    {
        private readonly IGenericDataService<Product> _productDataService;
        private readonly IDialogService _dialogService;

        public ObservableCollection<Product> Products { get; set; }

        private Product _SelectedProduct;

        public Product SelectedProduct
        {
            get { return _SelectedProduct; }
            set => SetProperty(ref _SelectedProduct, value);
        }

        public ICommand LoadCommand { get; set; }
        private async Task OnLoadCommandExecuted(object p) => await LoadProducts();
        private async Task LoadProducts()
        {
            var list = await _productDataService.GetListAsync();
            Products.Clear();
            foreach (var item in list)
                Products.Add(item);
        }

        public ICommand AddCommand { get; set; }
        private async Task OnAddCommandExecuted(object p)
        {
            bool update = false;
            _dialogService.ShowDialog(typeof(ProductItemViewModel), null, (dialogResult,p) =>
            {
                update = dialogResult;
            });

            if (update)
                await LoadProducts();
        }

        public ICommand EditCommand { get; set; }
        private async Task OnEditCommandExecuted(object p)
        {
            bool update = false;
            _dialogService.ShowDialog(typeof(ProductItemViewModel), SelectedProduct.Id, (dialogResult, p) =>
            {
                update = dialogResult;
            });

            if (update)
                await LoadProducts();
        }

        public ICommand DeleteCommand { get; set; }
        private async Task OnDeleteCommandExecuted(object p)
        {
            bool delete = false;
            _dialogService.ShowDialog(typeof(MessageDialogViewModel), new Tuple<string,string>("Delete product",$"Are you sure you want to delete {SelectedProduct.Name}?"), (dialogResult, p) =>
            {
                delete = dialogResult;
            });

            if (delete)
            {
                await _productDataService.RemoveAsync(SelectedProduct.Id);
                await LoadProducts();
            }
        }

        public ProductListViewModel(IDialogService dialogService, IGenericDataService<Product> productDataService)
        {
            _dialogService = dialogService;
            _productDataService = productDataService;
            Products = new ObservableCollection<Product>();
            LoadCommand = new RelayCommandAsync(OnLoadCommandExecuted, (p) => true);
            AddCommand = new RelayCommandAsync(OnAddCommandExecuted, (p) =>true);
            EditCommand = new RelayCommandAsync(OnEditCommandExecuted, (p) => true);
            DeleteCommand = new RelayCommandAsync(OnDeleteCommandExecuted,(p) => true);
        }
    }
}
