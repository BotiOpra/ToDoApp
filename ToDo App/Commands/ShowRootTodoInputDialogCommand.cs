using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo_App.NavigationStores;
using ToDo_App.ViewModels;

namespace ToDo_App.Commands
{
    public class ShowRootTodoInputDialogCommand : CommandBase
    {
        private MainViewModel mainViewModel;
        private ModalNavigationStore _navigationStore;

        public ShowRootTodoInputDialogCommand(MainViewModel mainVM, ModalNavigationStore navigationStore)
        {
            mainViewModel = mainVM;
            _navigationStore = navigationStore;
        }

        public ShowRootTodoInputDialogCommand(MainViewModel mainViewModel, ModalNavigationStore navigationStore, Action execute)
        {
            this.mainViewModel = mainViewModel;
            _navigationStore = navigationStore;
        }

        public override void Execute(object parameter)
        {
            _navigationStore.CurrentViewModel = new TodoInputViewModel(mainViewModel, _navigationStore);
        }
    }
}
