using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PedaliDemExam.ViewModels.Base
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public bool Set<T>(ref T field,T value, string propertyName)
        {
            if (EqualityComparer<T>.Default.Equals(field,value)) return false;
            field = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            return true;
        }
        protected void OpenNewWindowThenCloseOld(Window windowToOpen)
        {
            var temp = Application.Current.MainWindow;
            Application.Current.MainWindow = windowToOpen;
            temp.Close();
            windowToOpen.Show();
        }
    }
    public class RelayCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;
        private Action<object> _execute;
        private Func<object, bool> _canExecute;

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null!)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object? parameter)
        {
            return _canExecute == null || _canExecute(parameter!);
        }

        public void Execute(object? parameter)
        {
            _execute(parameter!);
        }
    }
}
