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
    public class LoginWindowViewModel : ViewModelBase
    {
        public LoginWindowViewModel()
        {
            _ctx = new AppDbContext();
            _login = string.Empty;
            _password = string.Empty;

            LoginCommand = new(lc => 
            {
                var user = _ctx.Users.FirstOrDefault(u=>u.Login == _login && u.Password == _password);
                if (user != null)
                {
                    MessageBox.Show($"Вход под учётной записью {user.FullName}. Роль: {user.RoleText}","Успешно",MessageBoxButton.OK,MessageBoxImage.Information);
                    OpenNewWindowThenCloseOld(new ProductsWindow(_ctx, user));
                }
                else
                {
                    var result = MessageBox.Show($"Данные не найдены, войти как гость?", "Успешно", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if (result == MessageBoxResult.Yes)
                    {
                        MessageBox.Show($"Вход как гость", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
                        OpenNewWindowThenCloseOld(new ProductsWindow(_ctx, null));
                    }

                }
            });
        }
        private AppDbContext _ctx;

        private string _login;
        private string _password;

        public string Login { get => _login; set => Set(ref _login,value,nameof(Login)); }
        public string Password { get => _password; set => Set(ref _password,value,nameof(Password)); }

        public RelayCommand LoginCommand { get; }
    }
}
