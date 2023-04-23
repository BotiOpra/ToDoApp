using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using ToDo_App.Commands;
using ToDo_App.Models;
using ToDo_App.NavigationStores;

namespace ToDo_App.ViewModels
{
	public class MainViewModel : ViewModelBase
	{
		public ObservableCollection<TodoListVM> RootTodos { get; set; }
		public TodoListVM _selectedTodo;
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
					OnPropertyChanged(nameof(SelectedTodo));
					OnPropertyChanged(nameof(IsTodoSelected));
					OnPropertyChanged(nameof(TodoLabelMessage));

					MoveUpTodoItemCommand.RaiseCanExecuteChanged();
					MoveDownTodoItemCommand.RaiseCanExecuteChanged();

					_modalNavigationStore.CloseModal();
				}
			}
		}

		private TaskVM _selectedTask;
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

		public IList<TodoListVM> FlatTodoLists
		{
			get
			{
				var result = new List<TodoListVM>();
				foreach (var root in RootTodos)
				{
					result.AddRange(FlattenTodoLists(root));
				}
				return result;
			}
		}

		public string TodoLabelMessage
		{
			get
			{
				if (_selectedTodo != null)
					return string.Format("Viewing {0} to-do list. {1} tasks shown.", _selectedTodo.Name, _selectedTodo.Tasks.Count);
				return "No Todo selected";
			}
		}

		public ObservableCollection<Category> TaskCategories { get; set; } = new ObservableCollection<Category>();

		public bool IsTaskSelected
		{
			get
			{
				return _selectedTask != null;
			}
		}

		public bool IsTodoSelected
		{
			get
			{
				return _selectedTodo != null;
			}
		}

		public int NrDueTodayTasks
		{
			get
			{
				return CountAllTodoTasks(RootTodos, (TaskVM task) => { return task.DueDate.Date == DateTime.Today; });
			}
		}

		public int NrDueTomorrowTasks
		{
			get
			{
				return CountAllTodoTasks(RootTodos, (TaskVM task) => { return task.DueDate.Date == DateTime.Today.AddDays(1); });
			}
		}

		public int NrOverdueTasks
		{
			get
			{
				return CountAllTodoTasks(RootTodos, (TaskVM task) => { return task.DueDate.Date < DateTime.Today; });
			}
		}

		public int NrDoneTasks
		{
			get
			{
				return CountAllTodoTasks(RootTodos, (TaskVM task) => { return task.Status == StatusType.Done; });
			}
		}
		public int NrTasksLeft
		{
			get
			{
				return CountAllTodoTasks(RootTodos, (TaskVM task) => { return task.Status != StatusType.Done; });
			}
		}

		private ModalNavigationStore _modalNavigationStore;
		public ViewModelBase CurrentModal => _modalNavigationStore.CurrentViewModel;

		public bool IsModalOpen => _modalNavigationStore.IsOpen;

		//public ICommand ExitCommand { get; private set; }

		public ICommand ShowTaskInputDialogCommand { get; }

		public ICommand ShowTaskEditDialogCommand { get; }

		public ICommand DeleteTaskCommand { get; }

		public ICommand SetTaskDoneCommand { get; }

		public RelayCommand MoveUpTaskItemCommand { get; }
		public RelayCommand MoveDownTaskItemCommand { get; }

		public RelayCommand MoveUpTodoItemCommand { get; }
		public RelayCommand MoveDownTodoItemCommand { get; }

		public ICommand ShowTodoInputDialogCommand { get; }
		public ICommand ShowRootTodoInputDialogCommand { get; }

		public ICommand ShowTodoEditDialogCommand { get; }

		public ICommand DeleteTodoCommand { get; }

		public ICommand ShowCategoryManagementDialogCommand { get; }

		public MainViewModel(ModalNavigationStore navigationStore)
		{
			_modalNavigationStore = navigationStore;
			_modalNavigationStore.CurrentViewModelChanged += OnCurrentModalViewModelChanged;

			RootTodos = new ObservableCollection<TodoListVM>();
			RootTodos.CollectionChanged += OnTodoListTodosChanged;
			RootTodos.CollectionChanged += OnSelectedTodoTasksChanged;


			TaskCategories.Add(new Category("kecske"));
			TaskCategories.Add(new Category("foo"));
			TaskCategories.Add(new Category("bar"));
			TaskCategories.Add(new Category("baz"));

			var todoList1 = new TodoList("Test");
			var todoList2 = new TodoList("Test1");
			var todoList3 = new TodoList("Test3");
			var todoList4 = new TodoList("Test2");

			var todoList1VM = new TodoListVM(todoList1);
			var todoList2VM = new TodoListVM(todoList2);

			RootTodos.Add(todoList1VM);
			RootTodos.Add(todoList2VM);

			todoList1VM.AddTodo(new TodoListVM(todoList3));
			todoList2VM.AddTodo(new TodoListVM(todoList4));

			ShowTaskInputDialogCommand = new ShowTaskInputDialogCommand(this, _modalNavigationStore);
			ShowTaskEditDialogCommand = new ShowTaskEditDialogCommand(this, _modalNavigationStore);
			DeleteTaskCommand = new RelayCommand(RemoveSelectedTask);
			SetTaskDoneCommand = new RelayCommand(SetTaskDone);
			MoveUpTaskItemCommand = new RelayCommand(MoveUpTaskItem, () =>
			{
				return SelectedTodo?.Tasks.IndexOf(SelectedTask) != 0;
			});
			MoveDownTaskItemCommand = new RelayCommand(MoveDownTaskItem, () =>
			{
				return SelectedTodo?.Tasks.IndexOf(SelectedTask) != SelectedTodo?.Tasks.Count - 1;
			});

			MoveUpTodoItemCommand = new RelayCommand(MoveUpTodoItem, () =>
			{
				if (SelectedTodo != null)
				{
					if (SelectedTodo.ParentTodoVM != null)
						return SelectedTodo.ParentTodoVM?.Todos.IndexOf(SelectedTodo) != 0;
					return RootTodos.IndexOf(SelectedTodo) != 0;
				}
				return false;
			});

			MoveDownTodoItemCommand = new RelayCommand(MoveDownTodoItem, () =>
			{
				if (SelectedTodo != null)
				{
					if (SelectedTodo.ParentTodoVM != null)
						return SelectedTodo.ParentTodoVM?.Todos.IndexOf(SelectedTodo) != SelectedTodo.ParentTodoVM?.Todos.Count - 1;
					return RootTodos.IndexOf(SelectedTodo) != RootTodos.Count - 1;
				}
				return false;
			});

			ShowTodoInputDialogCommand = new ShowTodoInputDialogCommand(this, _modalNavigationStore);
			ShowRootTodoInputDialogCommand = new ShowRootTodoInputDialogCommand(this, _modalNavigationStore);

			ShowTodoEditDialogCommand = new ShowTodoEditDialogCommand(this, _modalNavigationStore);

			DeleteTodoCommand = new RelayCommand(RemoveSelectedTodo);

			ShowCategoryManagementDialogCommand = new ShowCategoryManagementDialogCommand(TaskCategories, _modalNavigationStore);
		}

		private void OnTodoListTodosChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			if (e.Action == NotifyCollectionChangedAction.Add)
			{
				foreach (var todoList in e.NewItems.OfType<TodoListVM>())
				{
					todoList.Todos.CollectionChanged += OnTodoListTodosChanged;
					todoList.Tasks.CollectionChanged += OnSelectedTodoTasksChanged;
				}
			}
			//if(e.Action == NotifyCollectionChangedAction.Remove || e.Action==NotifyCollectionChangedAction.Reset)
			//{
			//    foreach(var todoList in e.OldItems.OfType<TodoListVM>())
			//    {
			//        todoList.Todos.CollectionChanged += OnTodoListTodosChanged;
			//        todoList.Tasks.CollectionChanged += OnSelectedTodoTasksChanged;
			//    }
			//}
			//if(e.Action == NotifyCollectionChangedAction.Replace)
			//{
			//    Console.WriteLine("Kecske");
			//}
		}

		public void AddTodo()
		{
			Debug.Assert(SelectedTodo != null);

			SelectedTodo.AddTodo(new TodoListVM(new TodoList("Pityu")));
		}

		public int CountAllTodoTasks(ObservableCollection<TodoListVM> todos, Predicate<TaskVM> incrementCondition)
		{
			int nrTasksDueToday = 0;
			foreach (var todo in todos)
			{
				foreach (var task in todo.Tasks)
				{
					if (incrementCondition(task))
						nrTasksDueToday++;
				}
				nrTasksDueToday += CountAllTodoTasks(todo.Todos, incrementCondition);
			}
			return nrTasksDueToday;
		}

		private void OnSelectedTodoTasksChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			OnPropertyChanged(nameof(NrDueTodayTasks));
			OnPropertyChanged(nameof(NrDueTomorrowTasks));
			OnPropertyChanged(nameof(NrOverdueTasks));
			OnPropertyChanged(nameof(NrDoneTasks));
			OnPropertyChanged(nameof(NrTasksLeft));

			OnPropertyChanged(nameof(TodoLabelMessage));
		}

		public void RemoveSelectedTask()
		{
			_modalNavigationStore.CloseModal();
			SelectedTodo.RemoveTask(SelectedTask);
		}

		public IList<TodoListVM> FlattenTodoLists(TodoListVM root)
		{
			var result = new List<TodoListVM>
			{
				root
			};
			foreach (var child in root.Todos)
			{
				result.AddRange(FlattenTodoLists(child));
			}
			return result;
		}

		public void RemoveSelectedTodo()
		{
			_modalNavigationStore.CloseModal();
			// delete todo
			Debug.Assert(SelectedTodo != null);

			TodoListVM parentTodo = SelectedTodo.ParentTodoVM;
			if (parentTodo != null)
				parentTodo.EraseTodo(SelectedTodo);
			else
				RootTodos.Remove(SelectedTodo);
		}

		public void SetTaskDone()
		{
			SelectedTask.Status = StatusType.Done;
			OnPropertyChanged(nameof(NrDoneTasks));
			OnPropertyChanged(nameof(NrTasksLeft));
		}

		public void MoveUpTaskItem()
		{
			int selectedItemIndex = _selectedTodo.Tasks.IndexOf(SelectedTask);
			int newIndex = selectedItemIndex - 1;
			_selectedTodo.Tasks.Move(selectedItemIndex, newIndex);
			MoveUpTaskItemCommand.RaiseCanExecuteChanged();
			MoveDownTaskItemCommand.RaiseCanExecuteChanged();
		}

		public void MoveDownTaskItem()
		{
			int selectedItemIndex = _selectedTodo.Tasks.IndexOf(SelectedTask);
			int newIndex = selectedItemIndex + 1;
			_selectedTodo.Tasks.Move(selectedItemIndex, newIndex);
			MoveUpTaskItemCommand.RaiseCanExecuteChanged();
			MoveDownTaskItemCommand.RaiseCanExecuteChanged();
		}

		public void MoveUpTodoItem()
		{
			TodoListVM parent = SelectedTodo.ParentTodoVM;

			if (parent != null)
			{
				int selectedItemIndex = parent.Todos.IndexOf(_selectedTodo);
				int newIndex = selectedItemIndex - 1;
				parent.Todos.Move(selectedItemIndex, newIndex);
			}
			else
			{
				int selectedItemIndex = RootTodos.IndexOf(_selectedTodo);
				int newIndex = selectedItemIndex - 1;
				RootTodos.Move(selectedItemIndex, newIndex);
			}
			MoveUpTodoItemCommand.RaiseCanExecuteChanged();
			MoveDownTodoItemCommand.RaiseCanExecuteChanged();
		}

		public void MoveDownTodoItem()
		{

			TodoListVM parent = SelectedTodo.ParentTodoVM;

			if (parent != null)
			{
				int selectedItemIndex = parent.Todos.IndexOf(_selectedTodo);
				int newIndex = selectedItemIndex + 1;
				parent.Todos.Move(selectedItemIndex, newIndex);
			}
			else
			{
				int selectedItemIndex = RootTodos.IndexOf(_selectedTodo);
				int newIndex = selectedItemIndex + 1;
				RootTodos.Move(selectedItemIndex, newIndex);
			}

			MoveUpTodoItemCommand.RaiseCanExecuteChanged();
			MoveDownTodoItemCommand.RaiseCanExecuteChanged();
		}

		private void OnCurrentModalViewModelChanged()
		{
			OnPropertyChanged(nameof(IsModalOpen));
			OnPropertyChanged(nameof(CurrentModal));
		}
	}
}
