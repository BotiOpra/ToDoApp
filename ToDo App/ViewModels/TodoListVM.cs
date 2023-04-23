using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Data;
using ToDo_App.Commands;
using ToDo_App.Models;

namespace ToDo_App.ViewModels
{
    public class TodoListVM : ViewModelBase
    {
        // TODO, when adding new TodoList, subscribe a function to collectionchanged event

        private readonly TodoList _todoList;
        public TodoList TodoListModel { get { return _todoList; } }

        public ObservableCollection<TodoListVM> Todos { get; set; }
        public ObservableCollection<TaskVM> Tasks { get; set; }

        public string Name
        {
            get
            {
                return _todoList.Name;
            }
            set
            {
                _todoList.Name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        public string ImagePath
        {
            get => _todoList.ImagePath;
            set
            {
                _todoList.ImagePath = value;
                OnPropertyChanged(nameof(ImagePath));
            }
        }

        private TodoListVM _parentVM;
        public TodoListVM ParentTodoVM
        {
            get => _parentVM;
            set => _parentVM = value;
        }
        public TodoList ParentTodo
        {
            get => _parentVM?.TodoListModel;
        }

        public TodoListVM(TodoList todoList)
        {
            Todos = new ObservableCollection<TodoListVM>();
            Tasks = new ObservableCollection<TaskVM>();

            _todoList = todoList;

            foreach (var todo in _todoList.Todos)
            {
                Todos.Add(new TodoListVM(todo));
            }

            foreach (var task in _todoList.Tasks)
            {
                Tasks.Add(new TaskVM(task));
            }
        }

        public void AddTask(TaskVM taskVM)
        {
            Task task = new Task(
                taskVM.Title,
                taskVM.Description,
                taskVM.DueDate,
                taskVM.Status,
                taskVM.Category,
                taskVM.Priority
                );
            _todoList.AddTask(task);
            Tasks.Add(new TaskVM(task));
        }

        public void RemoveTask(TaskVM task)
        {
            _todoList.RemoveTask(task.TaskModel);
            Tasks.Remove(task);
        }

        // TODO
        public void UpdateTask(TaskVM newTask)
        {
            _todoList.UpdateTask(newTask.TaskModel);
            for (int i = 0; i < Tasks.Count; i++)
            {
                Tasks[i] = new TaskVM(_todoList.Tasks[i]);
            }
        }

        public void AddTodo(TodoListVM newTodo)
        {
            newTodo.ParentTodoVM = this;
            newTodo.TodoListModel.ParentTodo = ParentTodo;
            _todoList.AddTodo(newTodo.TodoListModel);

            // we don't add the newtodo, so that the todolistvm (that is shown on the screen) has, as model reference the todoList.
            Todos.Add(newTodo);
        }

        public void UpdateTodo(TodoListVM newTodo)
        {
            _todoList.UpdateTodo(newTodo.TodoListModel);
            ParentTodoVM = newTodo.ParentTodoVM;
            Name = newTodo.Name;
            ImagePath = newTodo.ImagePath;
        }

        public void EraseTodo(TodoListVM todo)
        {
            todo.Tasks.Clear();
            todo.Todos.Clear();
            Todos.Remove(todo);
            _todoList.RemoveTodo(todo.TodoListModel);
        }

        public void RemoveTodo(TodoListVM todo)
        {
            Todos.Remove(todo);
            _todoList.RemoveTodo(todo.TodoListModel);
        }

        public void RemoveTodo(string Id)
        {
            TodoListVM todo = Todos.First(t => t.TodoListModel.Id == Id);
            Todos.Remove(todo);
            _todoList.RemoveTodo(todo.TodoListModel);
        }
    }
}
