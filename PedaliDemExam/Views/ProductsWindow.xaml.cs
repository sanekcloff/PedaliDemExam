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
    /// Логика взаимодействия для ProductsWindow.xaml
    /// </summary>
    public partial class ProductsWindow : Window
    {
        public ProductsWindow(AppDbContext ctx, User? user)
        {
            InitializeComponent();

            if (user == null)
            {
                FullNameTextBlock.Text = "Гость";
                AddProductButton.Visibility = Visibility.Collapsed;
                SearchTextBox.Visibility = Visibility.Collapsed;
                SortComboBox.Visibility = Visibility.Collapsed;
                FilthComboBox.Visibility = Visibility.Collapsed;
            }
            else 
            {
                FullNameTextBlock.Text = user.FullName;
            }
            DataContext = new ProductsWindowViewModel(ctx, user);
        }
    }
}
