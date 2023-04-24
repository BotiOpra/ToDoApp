using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ToDo_App.Commands;
using ToDo_App.NavigationStores;

namespace ToDo_App.ViewModels
{
	public class FindTaskDialogViewModel : ViewModelBase
	{
		private MainViewModel _mainViewModel;
		private ModalNavigationStore _navigationStore;

		private ObservableCollection<TaskVM> _foundTasks;
		public ObservableCollection<TaskVM> FoundTasks
		{
			get => _foundTasks;
			set
			{
				_foundTasks = value;
				OnPropertyChanged(nameof(FoundTasks));
			}
		}

		private string _enteredTaskName;
		public string EnteredTaskName
		{
			get => _enteredTaskName;
			set
			{
				_enteredTaskName = value;
				OnPropertyChanged(nameof(EnteredTaskName));
			}
		}

		private DateTime _enteredDate;
		public DateTime EnteredDate
		{
			get { return _enteredDate; }
			set
			{
				_enteredDate = value;
				OnPropertyChanged(nameof(EnteredDate));
			}
		}
		private bool _isSearchByName;
		public bool IsSearchByName
		{
			get { return _isSearchByName; }
			set
			{
				_isSearchByName = value;
				_foundTasks.Clear();
				OnPropertyChanged(nameof(IsSearchByName));
			}
		}

		public ICommand CloseCommand { get; }
		public ICommand FindTaskCommand { get; }

		public FindTaskDialogViewModel(MainViewModel mainViewModel, ModalNavigationStore navigationStore)
		{
			_mainViewModel = mainViewModel;
			_navigationStore = navigationStore;
			FoundTasks = new ObservableCollection<TaskVM>();
			IsSearchByName = true;

			EnteredDate = DateTime.Today;


			CloseCommand = new CloseDialogCommand(navigationStore);
			FindTaskCommand = new RelayCommand(FindTask);
		}

		public void FindTask()
		{
			if (IsSearchByName)
				FindByName();
			else
				FindByDate();
		}

		public void FindByName()
		{
			if (EnteredTaskName == null)
				return;

			_foundTasks.Clear();

			var eligibleTasks = _mainViewModel.GetAllTasks().Where(task => task.Title.Contains(EnteredTaskName));

			foreach (var task in eligibleTasks)
			{
				_foundTasks.Add(task);

			}
		}

		public void FindByDate()
		{
			if (EnteredDate == null)
				return;

			_foundTasks.Clear();

			var eligibleTasks = _mainViewModel.GetAllTasks().Where(task => task.DueDate.Date == EnteredDate.Date);

			foreach (var task in eligibleTasks)
			{
				_foundTasks.Add(task);

			}
		}
	}
}
