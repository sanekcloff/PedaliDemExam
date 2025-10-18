using Microsoft.EntityFrameworkCore;
using PedaliDemExam.Context;
using PedaliDemExam.ViewModels.Base;
using PedaliDemExam.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            LogoutCommand = new(lc =>
            {
                OpenNewWindowThenCloseOld(new LoginWindow());
            });
        }

        private AppDbContext _ctx;
        private User? _user;

        private Product _selectedProduct;
        private List<Product> _products;

        public RelayCommand LogoutCommand { get; }
        public Product SelectedProduct { get => _selectedProduct; set => Set(ref _selectedProduct, value, nameof(SelectedProduct)); }
        public List<Product> Products { get => _products; set => Set(ref _products, value, nameof(Products)); }

        private ICollection<Product> GetProducts() => _ctx.Products.Include(p => p.Manufacturer).Include(p => p.ProductTypes).Include(p => p.Provider).ToList();
    }
}
