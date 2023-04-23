using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo_App.NavigationStores;
using ToDo_App.ViewModels;

namespace ToDo_App.Commands
{
    public class EditTaskCommand : CommandBase
    {
        private ModalNavigationStore _navigationStore;
        private TodoListVM _selectedTodo;
        private TaskVM _selectedTask;

        public EditTaskCommand(ModalNavigationStore navigationStore, TodoListVM selectedTodo, TaskVM selectedTask)
        {
            _navigationStore = navigationStore;
            _selectedTodo = selectedTodo;
            _selectedTask = selectedTask;
        }

        public override void Execute(object parameter)
        {
            //_selectedTodo.UpdateTask(_selectedTask);

            //_navigationStore.CloseModal();
        }
    }
}
