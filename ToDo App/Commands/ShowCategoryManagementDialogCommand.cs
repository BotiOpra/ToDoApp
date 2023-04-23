using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ToDo_App.Models;
using ToDo_App.NavigationStores;
using ToDo_App.ViewModels;

namespace ToDo_App.Commands
{
    public class ShowCategoryManagementDialogCommand : CommandBase
    {
        private ModalNavigationStore _modalNavigationStore;
        private ObservableCollection<Category> _categories;

        public ShowCategoryManagementDialogCommand(ObservableCollection<Category> categories, ModalNavigationStore modalNavigationStore)
        {
            _modalNavigationStore = modalNavigationStore;
            _categories = categories;
        }

        public override void Execute(object parameter)
        {
            _modalNavigationStore.CurrentViewModel = new CategoryManagementViewModel(_categories, _modalNavigationStore);
        }
    }
}
