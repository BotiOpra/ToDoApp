using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo_App.NavigationStores;

namespace ToDo_App.Commands
{
    public class CloseTaskDialogCommand : CommandBase
    {
        private ModalNavigationStore _navigationStore;

        public CloseTaskDialogCommand(ModalNavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
        }

        public override void Execute(object parameter)
        {
            _navigationStore.CloseModal();
        }
    }
}
