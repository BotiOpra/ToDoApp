using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using ToDo_App.Models;
using ToDo_App.NavigationStores;
using ToDo_App.ViewModels;

namespace ToDo_App.Commands
{
	public class ShowFilterTasksDialogCommand : CommandBase
	{
		private CollectionViewSource _tasksViewSource;
		private IList<Category> _taskCategories;
		private ModalNavigationStore _navigationStore;

		public ShowFilterTasksDialogCommand(CollectionViewSource tasksViewSource, IList<Category> taskCategories, ModalNavigationStore navigationStore)
		{
			_tasksViewSource = tasksViewSource;
			_taskCategories = taskCategories;
			_navigationStore = navigationStore;
		}

		public override void Execute(object parameter)
		{
			_navigationStore.CurrentViewModel = new FilterTasksDialogViewModel(_tasksViewSource.View, _taskCategories, _navigationStore);
		}
	}
}
