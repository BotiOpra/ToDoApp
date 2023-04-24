using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo_App.NavigationStores;
using ToDo_App.ViewModels;

namespace ToDo_App.Commands
{
	public class ShowFindTaskDialogCommand : CommandBase
	{
		private MainViewModel _mainViewModel;
		private ModalNavigationStore _navigationStore;

		public ShowFindTaskDialogCommand(MainViewModel mainViewModel, ModalNavigationStore navigationStore)
		{
			_mainViewModel = mainViewModel;
			_navigationStore = navigationStore;
		}

		public override void Execute(object parameter)
		{
			_navigationStore.CurrentViewModel = new FindTaskDialogViewModel(_mainViewModel, _navigationStore);
		}
	}
}
