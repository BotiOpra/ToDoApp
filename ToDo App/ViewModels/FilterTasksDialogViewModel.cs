using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ToDo_App.Commands;
using ToDo_App.Models;
using ToDo_App.NavigationStores;

namespace ToDo_App.ViewModels
{
	public class FilterTasksDialogViewModel : ViewModelBase
	{
		private ICollectionView _tasksView;
		private IList<Category> _taskCategories;
		private IList<string> _otherFilters;
		private ModalNavigationStore _navigationStore;

		public IList<Category> TaskCategories
		{
			get => _taskCategories;
		}

		public IList<string> OtherFilters
		{
			get => _otherFilters;
		}

		private object _selectedItem;
		public object SelectedItem
		{
			get => _selectedItem;
			set
			{
				_selectedItem = value;
				OnPropertyChanged(nameof(SelectedItem));
			}
		}

		public ICommand CloseCommand { get; }
		public ICommand FilterCommand { get; }
		public ICommand ResetFilterCommand { get; }

		public FilterTasksDialogViewModel(ICollectionView tasksView, IList<Category> taskCategories, ModalNavigationStore navigationStore)
		{
			_tasksView = tasksView;
			_taskCategories = taskCategories;
			_navigationStore = navigationStore;

			_otherFilters = new List<string>
			{
				"Done Tasks",
				"Overdue Tasks",
				"Due Today Tasks",
				"Future Tasks"
			};

			CloseCommand = new CloseDialogCommand(navigationStore);
			FilterCommand = new RelayCommand(FilterTasks);
			ResetFilterCommand = new RelayCommand(ResetFilter);
		}

		public void FilterTasks()
		{
			_tasksView.Filter = null;
			if (SelectedItem == null)
				return;
			if (SelectedItem as Category != null)
			{
				Category selectedCategory = SelectedItem as Category;
				_tasksView.Filter = (obj) =>
				{
					TaskVM taskVM = obj as TaskVM;
					return taskVM.Category.Name.Equals(selectedCategory.Name);
				};
			}
			else
			{
				string selectedFilter = SelectedItem as string;
				switch(selectedFilter)
				{
					case "Done Tasks":
						_tasksView.Filter = (obj) =>
						{
							TaskVM taskVM = obj as TaskVM;
							return taskVM.Status == StatusType.Done;
						};
						break;
					case "Overdue Tasks":
						_tasksView.Filter = (obj) =>
						{
							TaskVM taskVM = obj as TaskVM;
							return taskVM.DueDate.Date < DateTime.Today;
						};
						break;
					case "Due Today Tasks":
						_tasksView.Filter = (obj) =>
						{
							TaskVM taskVM = obj as TaskVM;
							return taskVM.DueDate.Date == DateTime.Today;
						};
						break;
					case "Future Tasks":
						_tasksView.Filter = (obj) =>
						{
							TaskVM taskVM = obj as TaskVM;
							return taskVM.DueDate.Date > DateTime.Today;
						};
						break;
				}
			}
		}

		public void ResetFilter()
		{
			_tasksView.Filter = null;
		}
	}
}
