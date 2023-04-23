using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private MainViewModel _mainViewModel;

        public TaskVM CurrentTask => _mainViewModel.SelectedTask;

        public TodoListVM CurrentTodo => _mainViewModel.SelectedTodo;
        public string Title
        {
            get => CurrentTask.Title;
            set
            {
                CurrentTask.Title = value;
                OnPropertyChanged(nameof(Title));
            }
        }

        public string Description
        {
            get => CurrentTask.Description;
            set
            {
                CurrentTask.Description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        public DateTime DueDate
        {
            get { return CurrentTask.DueDate; }
            set
            {
                CurrentTask.DueDate = value;
                OnPropertyChanged(nameof(DueDate));
            }
        }

        public ObservableCollection<Category> TaskCategories
        {
            get
            {
                return _mainViewModel.TaskCategories;
            }
            //set
            //{
            //    _category = value;
            //    OnPropertyChanged(nameof(Category));
            //}
        }
        public Category SelectedCategory
        {
            get => CurrentTask.Category;
            set
            {
                CurrentTask.Category = value;
                OnPropertyChanged(nameof(SelectedCategory));
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
                    OnPropertyChanged(nameof(Priority));
                }
            }
        }

        public ICommand CloseCommand { get; }

        public ICommand EditTaskCommand { get; }

        public TaskEditViewModel(MainViewModel mainViewModel, ModalNavigationStore navigationStore)
        {
            _mainViewModel = mainViewModel;
            SelectedCategory = CurrentTask.Category;

            CloseCommand = new CloseDialogCommand(navigationStore);
            EditTaskCommand = new EditTaskCommand(navigationStore, CurrentTodo, CurrentTask);
        }
    }
}
