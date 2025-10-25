using PedaliDemExam.Context;
using PedaliDemExam.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PedaliDemExam.ViewModels
{
    public class ProductManageWindowViewModel : ViewModelBase
    {
        public ProductManageWindowViewModel() { }
        public ProductManageWindowViewModel(AppDbContext ctx, Product? product)
        {
            _ctx = ctx;
            Providers = _ctx.Providers.ToList();
            ProductTypes = _ctx.ProductTypes.ToList();
            Manufacturers = _ctx.Manufacturers.ToList();
            Categories = new List<string>()
            {
                "Женская обувь",
                "Мужская обувь"
            };
            if (product == null) 
            {
                _localProduct = new();
                _selectedCategory = Categories[0];
                _localProduct.Manufacturer = Manufacturers[0];
                _localProduct.Provider = Providers[0];
                _localProduct.ProductTypes = ProductTypes[0];
                ActionCommand = new(ac =>
                {
                    try
                    {
                        _ctx.Products.Add(_localProduct);
                        _ctx.SaveChanges();
                        MessageBox.Show($"Добавлена новая запись в таблицу продуктов: {_localProduct.Article}!","Уведомление",MessageBoxButton.OK,MessageBoxImage.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"{ex.Message}: {ex.InnerException}!","Ошибка",MessageBoxButton.OK,MessageBoxImage.Error);
                    }
                });
            }
            else
            {
                _localProduct = new Product(_currentProduct = product);
                _selectedCategory = product.ProductCategory == Product.Category.Woman ? Categories[0] : Categories[1];
                ActionCommand = new(ac =>
                {
                    try
                    {
                        _currentProduct.Update(_localProduct);
                        _ctx.Products.Update(_currentProduct);
                        _ctx.SaveChanges();
                        MessageBox.Show($"Обновлена запись в таблице продуктов: {_currentProduct.Article}!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"{ex.Message}: {ex.InnerException}!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                });
            }
        }
        private AppDbContext _ctx;
        private Product _localProduct; // только для окна
        private Product? _currentProduct; // ссылка на бд экземепляр
        private string _selectedCategory;
        public List<ProductType> ProductTypes { get; }
        public List<Provider> Providers { get; }
        public List<Manufacturer> Manufacturers { get; }
        public List<string> Categories { get; }


        public Product LocalProduct 
        { 
            get => _localProduct; 
            set => Set(ref _localProduct, value, nameof(LocalProduct)); 
        }
        public string SelectedCategory 
        { 
            get => _selectedCategory; 
            set => Set(ref _selectedCategory, value, nameof(SelectedCategory)); 
        }
        public RelayCommand ActionCommand { get; }
    }
}
