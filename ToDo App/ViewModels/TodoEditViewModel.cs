using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using ToDo_App.Commands;
using ToDo_App.Models;
using ToDo_App.NavigationStores;

namespace ToDo_App.ViewModels
{
    public class TodoEditViewModel : ViewModelBase
    {
        private string _name;
        private ModalNavigationStore _navigationStore;
        private MainViewModel _mainViewModel;
        public TodoListVM SelectedTodo
        {
            get => _mainViewModel._selectedTodo;
        }

        private TodoListVM _selectedParent;
        public TodoListVM SelectedParent
        {
            get { return _selectedParent; }
            set
            {
                _selectedParent = value;
                OnPropertyChanged(nameof(SelectedParent));
            }
        }

        public IList<TodoListVM> FlatTodoLists
        {
            get => _mainViewModel.FlatTodoLists.Where(t => t != SelectedTodo && !_mainViewModel.FlattenTodoLists(SelectedTodo).Contains(t)).ToList();
        }
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private bool _isSubTodo;
        public bool IsSubTodo
        {
            get { return _isSubTodo; }
            set
            {
                _isSubTodo = value;
                SelectedParent = null;
                OnPropertyChanged(nameof(IsSubTodo));
            }
        }
        public ICommand CloseCommand { get; }
        public ICommand EditTodoCommand { get; }

        public TodoEditViewModel(MainViewModel mainViewModel, ModalNavigationStore navigationStore)
        {
            _mainViewModel = mainViewModel;

            IsSubTodo = SelectedTodo.ParentTodoVM != null;

            _name = SelectedTodo.Name;
            _selectedParent = SelectedTodo.ParentTodoVM;
            _navigationStore = navigationStore;

            CloseCommand = new CloseDialogCommand(navigationStore);
            EditTodoCommand = new RelayCommand<string>(
                commandParameter => ExecuteUpdateTodo(commandParameter as string),
                commandParameter => true);
        }

        public void ExecuteUpdateTodo(string filePath)
        {
            SelectedTodo.Name = Name;
            SelectedTodo.TodoListModel.Name = Name;
            if (filePath != null)
            {
                SelectedTodo.ImagePath = filePath;
                SelectedTodo.TodoListModel.ImagePath = filePath;
            }

            if (SelectedParent == null)
                return;
            TodoListVM oldParent = SelectedTodo.ParentTodoVM;
            //it becomes subtodo
            if (IsSubTodo)
            {
                // todo was subtodo
                if (SelectedTodo.ParentTodoVM != null)
                {
                    // add to new parent
                    SelectedParent.AddTodo(SelectedTodo);

                    // remove from old parent
                    oldParent.RemoveTodo(SelectedTodo);
                }
                // todo was root
                else
                {
                    SelectedParent.AddTodo(SelectedTodo);
                    _mainViewModel.RootTodos.Remove(SelectedTodo);
                }
            }
            else
            {
                if(SelectedTodo.ParentTodoVM != null)
                {
                    _mainViewModel.RootTodos.Add(SelectedTodo);
                    SelectedTodo.ParentTodoVM = null;
                    SelectedTodo.TodoListModel.ParentTodo = null;

                    oldParent.RemoveTodo(SelectedTodo);
                }
            }

            //SelectedTodo.UpdateTodo(newTodoVM);

            _navigationStore.CloseModal();
        }
    }
}
