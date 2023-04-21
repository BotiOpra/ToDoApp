using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using ToDo_App.Commands;
using ToDo_App.Models;
using ToDo_App.NavigationStores;

namespace ToDo_App.ViewModels
{
    public class TaskEditViewModel : ViewModelBase
    {
        public TaskVM CurrentTask { get; }
        public string Title
        {
            get => CurrentTask.Title;
            set
            {
                CurrentTask.Title = value;
            }
        }

        public string Description
        {
            get => CurrentTask.Description;
            set
            {
                CurrentTask.Description = value;
            }
        }

        public DateTime DueDate
        {
            get { return CurrentTask.DueDate; }
            set
            {
                CurrentTask.DueDate = value;
            }
        }

        public CategoryType Category
        {
            get { return CurrentTask.Category; }
            set
            {
                CurrentTask.Category = value;
            }
        }

        public PriorityType Priority
        {
            get { return CurrentTask.Priority; }
            set
            {
                if (CurrentTask.Priority != value)
                {
                    CurrentTask.Priority = value;
                }
            }
        }

        public ICommand CloseCommand { get; }

        public ICommand EditTaskCommand { get; }

        public TaskEditViewModel(TodoListVM selectedTodo, TaskVM selectedTask, ModalNavigationStore navigationStore)
        {
            CurrentTask = selectedTask;

            CloseCommand = new CloseTaskDialogCommand(navigationStore);
            EditTaskCommand = new EditTaskCommand(navigationStore, selectedTodo, selectedTask);
        }
    }
}
