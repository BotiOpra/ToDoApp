using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using ToDo_App.Models;

namespace ToDo_App.ViewModels
{
    public class TaskVM : ViewModelBase
    {
        private readonly Task _task;
        public Task TaskModel => _task;
        public TaskVM(Task task)
        {
            _task = task;
        }

        public string Title
        {
            get
            {
                return _task.Title;
            }
            set
            {
                _task.Title = value;
            }
        }

        public string Description
        {
            get
            {
                return _task.Description;
            }
            set
            {
                _task.Description = value;
            }
        }

        public CategoryType Category
        {
            get => _task.Category;
            set
            {
                _task.Category = value;
            }
        }

        public StatusType Status
        {
            get { return _task.Status; }
            set
            {
                _task.Status = value;
                OnPropertyChanged(nameof(Status));
                OnPropertyChanged(nameof(TaskItemColor));
            }
        }

        public PriorityType Priority
        {
            get
            {
                return _task.Priority;
            }
            set
            {
                _task.Priority = value;
            }
        }

        public DateTime DueDate
        {
            get => _task.DueDate;
            set
            {
                _task.DueDate = value;
            }
        }

        public Brush TaskItemColor => Status == StatusType.Done ? Brushes.Green : Brushes.Black;
    }
}
