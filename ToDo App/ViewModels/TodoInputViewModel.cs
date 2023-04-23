using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ToDo_App.Commands;
using ToDo_App.Models;
using ToDo_App.NavigationStores;

namespace ToDo_App.ViewModels
{
    public class TodoInputViewModel : ViewModelBase
    {
        private string _name;
        private ModalNavigationStore _navigationStore;
        private MainViewModel _mainViewModel;
        private TodoListVM _selectedTodo;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public ICommand AddTodoCommand { get; set; }
        public ICommand CloseCommand { get; set; }

        public TodoInputViewModel(TodoListVM selectedTodo, ModalNavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            _selectedTodo = selectedTodo;

            AddTodoCommand = new RelayCommand<string>(
                commandParameter => ExecuteAddTodo(commandParameter as string),
                commandParameter => true);

            CloseCommand = new CloseDialogCommand(navigationStore);
            //AddTaskCommand = new AddTaskCommand(navigationStore, selectedTodo);
        }

        public TodoInputViewModel(MainViewModel mainViewModel, ModalNavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            _mainViewModel = mainViewModel;

            AddTodoCommand = new RelayCommand<string>(
                commandParameter => ExecuteAddRootTodo(commandParameter as string),
                commandParameter => true);

            CloseCommand = new CloseDialogCommand(navigationStore);
        }

        public void ExecuteAddTodo(string filePath)
        {
            TodoList newTodo;
            if (filePath != null)
                newTodo = new TodoList(Name, filePath);
            else
                newTodo = new TodoList(Name);

            _selectedTodo.AddTodo(new TodoListVM(newTodo));

            _navigationStore.CloseModal();
        }

        public void ExecuteAddRootTodo(string filePath)
        {
            TodoList newTodo;
            if (filePath != null)
                newTodo = new TodoList(Name, filePath);
            else
                newTodo = new TodoList(Name);

            _mainViewModel.RootTodos.Add(new TodoListVM(newTodo));

            _navigationStore.CloseModal();
        }
    }
}
