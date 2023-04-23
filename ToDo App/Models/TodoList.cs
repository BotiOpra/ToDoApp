using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo_App.Utilities;

namespace ToDo_App.Models
{
    public class TodoList
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public string ImagePath { get; set; }

        public TodoList ParentTodo { get; set; }

        public IList<TodoList> Todos { get; set; }
        public IList<Task> Tasks { get; set; }
        TodoList() 
        {
            ParentTodo = null;
            Id = RandomIdGenerator.GenerateId();
            ImagePath = "C:\\Users\\Botond\\Pictures\\icons\\book.png";
        }

        public TodoList(string name) : this()
        {
            Name = name;
            Todos= new List<TodoList>();
            Tasks= new List<Task>();
        }

        public TodoList(TodoList parent, string name) : this()
        {
            ParentTodo = parent;
            Name = name;
            Todos = new List<TodoList>();
            Tasks = new List<Task>();
        }


        public TodoList(string name, string imagePath) : this()
        {
            Name = name;
            ImagePath = imagePath;
            Todos = new List<TodoList>();
            Tasks = new List<Task>();
        }

        public TodoList(TodoList parent, string name, string imagePath) : this()
        {
            ParentTodo = parent;
            Name = name;
            ImagePath = imagePath;
            Todos = new List<TodoList>();
            Tasks = new List<Task>();
        }

        public void AddTodo(TodoList t)
        {
            Todos.Add(t);
        }

        public void AddTask(Task t)
        {
            Tasks.Add(t);
        }

        public void RemoveTask(Task task)
        {
            Tasks.Remove(task);
        }

        public void UpdateTask(Task newTask)
        {
            for (int i = 0; i < Tasks.Count; i++)
            {
                if (Tasks[i].Id == newTask.Id)
                    Tasks[i] = newTask;
            }
        }

        public void UpdateTodo(TodoList t)
        {
            Name = t.Name;
            ImagePath = t.ImagePath;
        }

        public void RemoveTodo(TodoList t)
        {
            Todos.Remove(t);
        }
    }
}
