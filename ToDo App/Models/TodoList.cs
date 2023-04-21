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

        public IList<TodoList> Todos { get; set; }
        public IList<Task> Tasks { get; set; }
        TodoList() 
        {
            Id = RandomIdGenerator.GenerateId();
        }

        public TodoList(string name) : this()
        {
            Name = name;
            Todos= new List<TodoList>();
            Tasks= new List<Task>();
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
    }
}
