using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo_App.Models
{
	public class TodoApp
	{
		public IList<TodoList> RootTodos;

		public TodoApp() 
		{
			RootTodos = new List<TodoList>();
		}

		public TodoApp(IList<TodoList> rootTodos)
		{
			RootTodos = rootTodos;
		}

		public void AddRootTodo(TodoList todo)
		{
			RootTodos.Add(todo);
		}
	}
}
