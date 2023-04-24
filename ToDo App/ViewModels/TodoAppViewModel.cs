using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo_App.Models;
using ToDo_App.NavigationStores;

namespace ToDo_App.ViewModels
{
	public class TodoAppViewModel : ViewModelBase
	{
		private TodoApp _todoApp;
		private TodoListVM _selectedTodo;
		private TaskVM _selectedTask;
		public ObservableCollection<TodoListVM> RootTodos { get; set; }

		public TodoListVM SelectedTodo
		{
			get
			{
				return _selectedTodo;
			}
			set
			{
				if (_selectedTodo != value)
				{
					_selectedTodo = value;
					cvs.Source = null;
					if (_selectedTodo != null)
					{
						cvs.Source = _selectedTodo.Tasks;
						SelectedTodoTasksView.Refresh();
					}
					OnPropertyChanged(nameof(SelectedTodoTasksView));
					OnPropertyChanged(nameof(SelectedTodo));
					OnPropertyChanged(nameof(IsTodoSelected));
					OnPropertyChanged(nameof(TodoLabelMessage));

					MoveUpTodoItemCommand.RaiseCanExecuteChanged();
					MoveDownTodoItemCommand.RaiseCanExecuteChanged();

					_modalNavigationStore.CloseModal();
				}
			}
		}
		public TaskVM SelectedTask
		{
			get
			{
				return _selectedTask;
			}
			set
			{
				if (_selectedTask != value)
				{
					_selectedTask = value;
					OnPropertyChanged(nameof(SelectedTask));
					OnPropertyChanged(nameof(IsTaskSelected));
					MoveUpTaskItemCommand.RaiseCanExecuteChanged();
					MoveDownTaskItemCommand.RaiseCanExecuteChanged();

					_modalNavigationStore.CloseModal();
				}
			}
		}

		public TodoAppViewModel(TodoApp todoApp) 
		{
			_todoApp = todoApp;
			RootTodos = new ObservableCollection<TodoListVM>();

			foreach(var todo in todoApp.RootTodos)
			{
				RootTodos.Add(new TodoListVM(todo));
			}
		}

		public void AddRootTodo(TodoListVM todolistVM)
		{
			RootTodos.Add(todolistVM);
			_todoApp.RootTodos.Add(todolistVM.TodoListModel);
		}
	}
}
