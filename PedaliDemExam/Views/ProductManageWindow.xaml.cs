using PedaliDemExam.Context;
using PedaliDemExam.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PedaliDemExam.Views
{
    /// <summary>
    /// Логика взаимодействия для ProductManageWindow.xaml
    /// </summary>
    public partial class ProductManageWindow : Window
    {
        public ProductManageWindow(AppDbContext ctx, Product? product)
        {
            InitializeComponent();
            DataContext = new ProductManageWindowViewModel(ctx,product);
        }
    }
}
