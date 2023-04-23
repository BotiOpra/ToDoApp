using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ToDo_App.Models;
using ToDo_App.NavigationStores;
using ToDo_App.ViewModels;

namespace ToDo_App.Commands
{
    public class AddTaskCommand : CommandBase
    {
        private ModalNavigationStore _navigationStore;
        private TodoListVM _selectedTodo;

        public AddTaskCommand(ModalNavigationStore navigationStore, TodoListVM selectedTodo)
        {
            _navigationStore = navigationStore;
            _selectedTodo = selectedTodo;
        }

        public override void Execute(object parameter)
        {
            TaskInputViewModel taskInputViewModel = _navigationStore.CurrentViewModel as TaskInputViewModel;

            // TODO: modify this

            Task newTask = new Task(
                taskInputViewModel.Title,
                taskInputViewModel.Description,
                taskInputViewModel.DueDate,
                taskInputViewModel.SelectedCategory,
                taskInputViewModel.Priority
                );

                _selectedTodo.AddTask(new TaskVM(newTask));

            _navigationStore.CloseModal();
        }
    }
}
