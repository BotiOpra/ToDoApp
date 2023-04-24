using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ToDo_App.Commands;
using ToDo_App.Models;
using ToDo_App.NavigationStores;

namespace ToDo_App.ViewModels
{
	public class TaskInputViewModel : ViewModelBase
	{
		private MainViewModel _mainViewModel;

		private string _title;
		public string Title
		{
			get => _title;
			set
			{
				_title = value;
				OnPropertyChanged(nameof(Title));
			}
		}

		private string _description;
		public string Description
		{
			get { return _description; }
			set
			{
				_description = value;
				OnPropertyChanged(nameof(Description));
			}
		}

		private DateTime _dueDate = DateTime.Now;
		public DateTime DueDate
		{
			get { return _dueDate; }
			set
			{
				_dueDate = value;
				OnPropertyChanged(nameof(DueDate));
			}
		}

		public ObservableCollection<Category> TaskCategories
		{
			get
			{
				return _mainViewModel.TaskCategories;
			}
			//set
			//{
			//    _category = value;
			//    OnPropertyChanged(nameof(Category));
			//}
		}

		private Category _selectedCategory;
		public Category SelectedCategory
		{
			get => _selectedCategory;
			set
			{
				_selectedCategory = value;
				OnPropertyChanged(nameof(SelectedCategory));
			}
		}

		private PriorityType _priority;
		public PriorityType Priority
		{
			get { return _priority; }
			set
			{
				if (_priority != value)
				{
					_priority = value;
				}
				OnPropertyChanged(nameof(Priority));
			}
		}

		public ICommand CloseCommand { get; }

		public ICommand AddTaskCommand { get; }

		public TodoListVM SelectedTodo
		{
			get
			{
				return _mainViewModel.SelectedTodo;
			}
		}

		public TaskInputViewModel(MainViewModel mainViewModel, ModalNavigationStore navigationStore)
		{
			_mainViewModel = mainViewModel;
			_selectedCategory = TaskCategories.FirstOrDefault();

			CloseCommand = new CloseDialogCommand(navigationStore);
			AddTaskCommand = new AddTaskCommand(navigationStore, SelectedTodo);
		}
	}
}
