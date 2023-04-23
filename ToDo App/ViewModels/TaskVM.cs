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
                OnPropertyChanged(nameof(Title));
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
                OnPropertyChanged(nameof(Description));
            }
        }

        public Category Category
        {
            get => _task.Category;
            set
            {
                if(value == null)
                    _task.Category = Category.DefaultCategory;
                else
                    _task.Category = value;
                OnPropertyChanged(nameof(Category));
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
                OnPropertyChanged(nameof(Priority));
            }
        }

        public DateTime DueDate
        {
            get => _task.DueDate;
            set
            {
                _task.DueDate = value;
                OnPropertyChanged(nameof(DueDate));
            }
        }

        public DateTime CreationDate
        {
            get => _task.CreationDate;
            set
            {
                _task.CreationDate = value;
            }
        }

        public Brush TaskItemColor => Status == StatusType.Done ? Brushes.Green : Brushes.Black;
    }
}
