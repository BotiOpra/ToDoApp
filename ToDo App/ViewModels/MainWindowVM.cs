using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ToDo_App.Commands;

namespace ToDo_App.ViewModels
{
    public class MainWindowVM
    {
        public ICommand ExitCommand { get; private set; }
        public MainWindowVM() 
        {
            ExitCommand = new RelayCommand(() => 
            {
                Application.Current.Shutdown();
            });
        }


    }
}
