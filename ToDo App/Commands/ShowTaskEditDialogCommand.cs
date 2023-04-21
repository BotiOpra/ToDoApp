using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo_App.NavigationStores;
using ToDo_App.ViewModels;

namespace ToDo_App.Commands
{
    public class ShowTaskEditDialogCommand : CommandBase
    {
        private MainViewModel mainViewModel;
        private ModalNavigationStore _navigationStore;

        public ShowTaskEditDialogCommand(MainViewModel mainVM, ModalNavigationStore navigationStore)
        {
            mainViewModel = mainVM;
            _navigationStore = navigationStore;
        }

        public override void Execute(object parameter)
        {
            _navigationStore.CurrentViewModel = new TaskEditViewModel(mainViewModel.SelectedTodo, mainViewModel.SelectedTask, _navigationStore);
        }
    }
}
