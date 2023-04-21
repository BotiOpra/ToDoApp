using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
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

        public int NrDueTomorrowTasks
        {
            get
            {
                return CountAllTodoTasks(RootTodos);
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

        public ICommand AddTodoCommand { get; }

        public MainViewModel(ModalNavigationStore navigationStore)
        {
            _modalNavigationStore = navigationStore;
            _modalNavigationStore.CurrentViewModelChanged += OnCurrentModalViewModelChanged;

            RootTodos = new ObservableCollection<TodoListVM>();
            RootTodos.CollectionChanged += OnTodoListTodosChanged;

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

            AddTodoCommand = new RelayCommand(AddTodo);
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
            if(e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach(var todoList in e.OldItems.OfType<TodoListVM>())
                {
                    todoList.Todos.CollectionChanged -= OnTodoListTodosChanged;
                    todoList.Tasks.CollectionChanged -= OnSelectedTodoTasksChanged;
                }
            }
        }

        public void AddTodo()
        {
            Debug.Assert(SelectedTodo != null);

            SelectedTodo.AddTodo(new TodoListVM(new TodoList("Pityu")));
        }

        public int CountAllTodoTasks(ObservableCollection<TodoListVM> todos)
        {
            int nrTasksDueToday = 0;
            foreach(var todo in todos)
            {
                foreach(var task in todo.Tasks)
                {
                    if(task.DueDate.Date == DateTime.Today)
                        nrTasksDueToday++;
                }
                nrTasksDueToday += CountAllTodoTasks(todo.Todos);
            }
            return nrTasksDueToday;
        }

        private void OnSelectedTodoTasksChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(NrDueTomorrowTasks));
        }

        public void RemoveSelectedTask()
        {
            _modalNavigationStore.CloseModal();
            SelectedTodo.RemoveTask(SelectedTask);
        }

        public void SetTaskDone()
        {
            SelectedTask.Status = StatusType.Done;
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

        private void OnCurrentModalViewModelChanged()
        {
            OnPropertyChanged(nameof(IsModalOpen));
            OnPropertyChanged(nameof(CurrentModal));
        }
    }
}
