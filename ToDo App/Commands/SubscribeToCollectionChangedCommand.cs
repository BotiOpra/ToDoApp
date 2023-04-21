using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo_App.ViewModels;

namespace ToDo_App.Commands
{
    public class SubscribeToCollectionChangedCommand
    {
        private readonly ObservableCollection<TodoListVM> _collection;

        public SubscribeToCollectionChangedCommand(ObservableCollection<TodoListVM> collection)
        {
            _collection = collection;
            _collection.CollectionChanged += OnCollectionChanged;
        }

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter)
        {
            // Do nothing.
        }

        public event EventHandler CanExecuteChanged;

        protected void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            // Raise a PropertyChanged event for the collection property of each item in the collection.
            foreach (var item in _collection)
            {
                item.OnPropertyChanged(nameof(item.Todos));
            }
        }
    }
}
