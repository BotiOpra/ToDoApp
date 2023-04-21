using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo_App.Commands;
using ToDo_App.Models;

namespace ToDo_App.ViewModels
{
    public class TreeViewModel : ViewModelBase
    {
        private ObservableCollection<TodoListVM> _rootTodos;
        private TodoListVM _selectedTodo;
        public ObservableCollection<TodoListVM> RootTodos
        {
            get => _rootTodos;
            set
            {
                if (_rootTodos != value)
                {
                    _rootTodos = value;
                    OnPropertyChanged(nameof(RootTodos));
                }

                if(_rootTodos != null)
                {
                    _rootTodos.CollectionChanged += OnRootTodosChanged;
                }
            }
        }

        public TodoListVM SelectedTodo
        {
            get { return _selectedTodo; }
            set
            {
                if(_selectedTodo != value)
                {
                    _selectedTodo = value;
                    OnPropertyChanged(nameof(SelectedTodo));
                }
            }
        }

        private void OnRootTodosChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (var node in e.NewItems.OfType<TodoListVM>())
                {
                    var command = new SubscribeToCollectionChangedCommand(node.Todos);
                    command.Execute(null);
                }
            }
        }

        public TreeViewModel()
        {
            RootTodos = new ObservableCollection<TodoListVM>();

            var todoList1 = new TodoList("Test");
            var todoList2 = new TodoList("Test1");
            var todoList3 = new TodoList("Test3");
            var todoList4 = new TodoList("Test2");

            todoList1.AddTodo(todoList3);
            todoList2.AddTodo(todoList4);

            RootTodos.Add(new TodoListVM(todoList1));
            RootTodos.Add(new TodoListVM(todoList2));
        }
    }
}
