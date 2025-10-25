using Microsoft.EntityFrameworkCore;
using PedaliDemExam.Context;
using PedaliDemExam.ViewModels.Base;
using PedaliDemExam.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PedaliDemExam.ViewModels
{
    public class ProductsWindowViewModel : ViewModelBase
    {
        public ProductsWindowViewModel() { }

        public ProductsWindowViewModel(AppDbContext ctx, User? user)
        {
            _ctx = ctx;
            _user = user;
            _selectedProduct = null!;
            _products = GetProducts().ToList();
            FilthValues.AddRange(_ctx.Providers.Select(p=>p.Title));

            _searchValue = string.Empty;
            _sortValue = SortValues[0];
            _filthValue = FilthValues[0];
            LogoutCommand = new(lc =>
            {
                OpenNewWindowThenCloseOld(new LoginWindow());
            });
            AddProductCommand = new(apc => 
            {
                if (user == null || user.EmployeeRole != User.Role.Admin) return;
                new ProductManageWindow(ctx, null).ShowDialog();
                UpdateList();
            });
            UpdateProductCommand = new(upc => 
            {
                if (user == null || user.EmployeeRole != User.Role.Admin) return;
                if (_selectedProduct == null)
                {
                    MessageBox.Show("Необходимо выбрать продукт", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                new ProductManageWindow(ctx, _selectedProduct).ShowDialog();
                UpdateList();
            });
            DeleteProductCommand = new(dpc =>
            {
                if (user == null || user.EmployeeRole != User.Role.Admin) return;

                if (_selectedProduct == null)
                {
                    MessageBox.Show("Необходимо выбрать продукт!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                if (_selectedProduct.Orders.Any())
                {
                    MessageBox.Show("Продукт присутсвует в заказах!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                if (MessageBox.Show($"Удалить продукт {_selectedProduct.Article}?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes) return;

                _ctx.Products.Remove(_selectedProduct);
                _ctx.SaveChanges();
                UpdateList();
            });
        }

        private AppDbContext _ctx;
        private User? _user;

        private Product _selectedProduct;
        private List<Product> _products;

        private string _searchValue;

        private string _sortValue;
        private string _filthValue;

        public Product SelectedProduct { get => _selectedProduct; set => Set(ref _selectedProduct, value, nameof(SelectedProduct)); }
        public List<Product> Products { get => _products; set => Set(ref _products, value, nameof(Products)); }
        public string SearchValue
        {
            get => _searchValue;
            set
            {
                if(Set(ref _searchValue, value, nameof(SearchValue)))
                {
                    UpdateList();
                }
            }
        }
        public List<string> SortValues { get; } = new List<string>()
        {
            "Без сортировки",
            "Количество на складе(возр.)",
            "Количество на складе(уб.)",
        };
        public string SortValue
        {
            get => _sortValue;
            set
            {
                if (Set(ref _sortValue, value, nameof(SortValue)))
                {
                    UpdateList();
                }
            }
        }
        public List<string> FilthValues { get; } = new List<string>()
        {
            "Все поставщики",
        };
        public string FilthValue
        {
            get => _filthValue;
            set
            {
                if(Set(ref _filthValue, value, nameof(FilthValue)))
                {
                    UpdateList();
                }
            }
        }

        private ICollection<Product> GetProducts() => _ctx.Products.Include(p => p.Manufacturer).Include(p => p.ProductTypes).Include(p => p.Provider).Include(p=>p.Orders).ToList();

        private void UpdateList()
        {
            Products = Filth(Sort(Search(GetProducts()))).ToList();
        }
        private ICollection<Product> Search(ICollection<Product> products)
        {
            if (string.IsNullOrWhiteSpace(_searchValue))
                return products;
            else
                return products.Where(p => p.Article.ToLower().Contains(_searchValue.ToLower()) || p.CategoryText.ToLower().Contains(_searchValue.ToLower()) || p.ProductTypes.Title.ToLower().Contains(_searchValue.ToLower())).ToList();
        }
        private ICollection<Product> Sort(ICollection<Product> products)
        {
            if (_sortValue == SortValues[1])
                return products.OrderBy(p=>p.CountOnStorage).ToList();
            else if (_sortValue == SortValues[2])
                return products.OrderByDescending(p => p.CountOnStorage).ToList();
            else
                return products;
        }
        private ICollection<Product> Filth(ICollection<Product> products)
        {
            if (_filthValue == FilthValues[0])
                return products;
            else
                return products.Where(p=>p.Provider.Title == _filthValue).ToList();
        }
        
        public RelayCommand LogoutCommand { get; }
        public RelayCommand AddProductCommand { get; }
        public RelayCommand UpdateProductCommand { get; }
        public RelayCommand DeleteProductCommand { get; }

    }
}
