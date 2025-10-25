using PedaliDemExam.Context;
using PedaliDemExam.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PedaliDemExam.ViewModels
{
    public class ProductManageWindowViewModel : ViewModelBase
    {
        public ProductManageWindowViewModel() { }
        public ProductManageWindowViewModel(AppDbContext ctx, Product? product)
        {
            _ctx = ctx;
            if (product == null) 
            {
                _localProduct = new();
            }
            else
            {
                _localProduct = _currentProduct = product;
            }
        }
        private AppDbContext _ctx;
        private Product _localProduct; // только для окна
        private Product? _currentProduct; // ссылка на бд экземепляр

        public Product LocalProduct 
        { 
            get => _localProduct; 
            set => Set(ref _localProduct, value, nameof(LocalProduct)); 
        }
    }
}
