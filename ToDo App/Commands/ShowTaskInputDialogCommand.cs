using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo_App.Models;
using ToDo_App.NavigationStores;
using ToDo_App.ViewModels;

namespace ToDo_App.Commands
{
    public class ShowTaskInputDialogCommand : CommandBase
    {
        private MainViewModel mainViewModel;
        private ModalNavigationStore _navigationStore;

        public ShowTaskInputDialogCommand(MainViewModel mainVM, ModalNavigationStore navigationStore)
        {
            mainViewModel = mainVM;
            _navigationStore = navigationStore;
        }

        public ShowTaskInputDialogCommand(MainViewModel mainViewModel, ModalNavigationStore navigationStore, Action execute)
        {
            this.mainViewModel = mainViewModel;
            _navigationStore = navigationStore;
        }

        public override void Execute(object parameter)
        {
            _navigationStore.CurrentViewModel = new TaskInputViewModel(mainViewModel._selectedTodo, _navigationStore);
        }
    }
}
