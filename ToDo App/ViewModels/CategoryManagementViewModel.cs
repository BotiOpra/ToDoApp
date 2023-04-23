using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ToDo_App.Commands;
using ToDo_App.Models;
using ToDo_App.NavigationStores;

namespace ToDo_App.ViewModels
{
    public class CategoryManagementViewModel : ViewModelBase
    {
        private ObservableCollection<Category> _categories;
        private ModalNavigationStore _navigationStore;

        public ObservableCollection<Category> TaskCategories
        {
            get
            {
                return _categories;
            }
            //set
            //{
            //    _category = value;
            //    OnPropertyChanged(nameof(Category));
            //}
        }

        private Category _selectedCategory;
        public Category SelectedCategory
        {
            get { return _selectedCategory; }
            set
            {
                _selectedCategory = value;
                OnPropertyChanged(nameof(SelectedCategory));
            }
        }

        private string _enteredName;
        public string EnteredName
        {
            get { return _enteredName; }
            set
            {
                _enteredName = value;
                OnPropertyChanged(nameof(EnteredName));
                AddCategoryCommand.RaiseCanExecuteChanged();
            }
        }

        public ICommand CloseCommand { get; }

        public RelayCommand AddCategoryCommand { get; }
        public ICommand RemoveCategoryCommand { get; }

        public CategoryManagementViewModel(ObservableCollection<Category> categories, ModalNavigationStore navigationStore)
        {
            _categories = categories;
            _navigationStore = navigationStore;

            CloseCommand = new CloseDialogCommand(navigationStore);
            AddCategoryCommand = new RelayCommand(ExecuteAddCategory, CanExecuteAddCategory);
            RemoveCategoryCommand = new RelayCommand(ExecuteRemoveCategory);
        }

		public void ExecuteAddCategory()
        {
            Category category = new Category(EnteredName);
            _categories.Add(category);

            EnteredName = string.Empty;
        }

        public void ExecuteRemoveCategory()
        {
            _categories.Remove(SelectedCategory);
        }

        public bool CanExecuteAddCategory()
        {
            return !string.IsNullOrEmpty(EnteredName);
        }
    }
}
