using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ToDo_App.Commands;
using ToDo_App.Models;
using ToDo_App.NavigationStores;

namespace ToDo_App.ViewModels
{
    public class TaskInputViewModel : ViewModelBase
    {
        private string _title;
        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged(nameof(Title));
            }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        private DateTime _dueDate = DateTime.Now;
        public DateTime DueDate
        {
            get { return _dueDate; }
            set
            {
                _dueDate = value;
                OnPropertyChanged(nameof(DueDate));
            }
        }

        private CategoryType _category;
        public CategoryType Category
        {
            get { return _category; }
            set
            {
                _category = value;
                OnPropertyChanged(nameof(Category));
            }
        }

        private PriorityType _priority;
        public PriorityType Priority
        {
            get { return _priority; }
            set
            {
                if (_priority != value)
                {
                    _priority = value;
                }
                OnPropertyChanged(nameof(Priority));
            }
        }

        public ICommand CloseCommand { get; }

        public ICommand AddTaskCommand { get; }

        public TaskInputViewModel(TodoListVM selectedTodo, ModalNavigationStore navigationStore)
        {
            CloseCommand = new CloseTaskDialogCommand(navigationStore);
            AddTaskCommand = new AddTaskCommand(navigationStore, selectedTodo);
        }
    }
}
