using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        private string _selectedCategory;
        public string SelectedCategory
        {
            get { return _selectedCategory; }
            set
            {
                _selectedCategory = value;
                OnPropertyChanged(nameof(SelectedCategory));
            }
        }

        public ICommand CloseCommand { get; }

        public ICommand AddCategoryCommand { get; }
        public ICommand DeleteCategoryCommand { get; }

        public CategoryManagementViewModel(ObservableCollection<Category> categories, ModalNavigationStore navigationStore)
        {
            _categories = categories;
            _navigationStore = navigationStore;

            CloseCommand = new CloseDialogCommand(navigationStore);
        }
    }
}
