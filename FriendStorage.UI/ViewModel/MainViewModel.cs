using FriendStorage.UI.Command;
using FriendStorage.UI.Events;
using FriendStorage.UI.View.Services;
using Microsoft.Practices.Prism.PubSubEvents;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace FriendStorage.UI.ViewModel
{
    public class MainViewModel : Observable
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IMessageDialogService _messageDialogService;
        private IFriendEditViewModel _selectedFriendEditViewModel;
        private Func<IFriendEditViewModel> _friendEditViewModelCreator;

        public MainViewModel(IEventAggregator eventAggregator,
                             IMessageDialogService messageDialogService,
                             INavigationViewModel navigationViewModel,
                             Func<IFriendEditViewModel> friendEditViewModelCreator)
        {
            _eventAggregator = eventAggregator;
            _messageDialogService = messageDialogService;
            _eventAggregator.GetEvent<OpenFriendEditViewEvent>().Subscribe(OnOpenFriendTab);
            _eventAggregator.GetEvent<FriendDeletedEvent>().Subscribe(OnFriendDeleted);

            NavigationViewModel = navigationViewModel;
            _friendEditViewModelCreator = friendEditViewModelCreator;
            FriendEditViewModels = new ObservableCollection<IFriendEditViewModel>();
            CloseFriendTabCommand = new DelegateCommand(OnCloseFriendTabExecute);
            AddFriendCommand = new DelegateCommand(OnAddFriendExecute);
        }

        public void Load()
        {
            NavigationViewModel.Load();
        }

        public ICommand CloseFriendTabCommand { get; private set; }

        public ICommand AddFriendCommand { get; set; }

        public INavigationViewModel NavigationViewModel { get; private set; }

        // Those ViewModels represent the Tab-Pages in the UI
        public ObservableCollection<IFriendEditViewModel> FriendEditViewModels { get; private set; }

        public IFriendEditViewModel SelectedFriendEditViewModel
        {
            get { return _selectedFriendEditViewModel; }
            set
            {
                _selectedFriendEditViewModel = value;
                OnPropertyChanged();
            }
        }

        private void OnAddFriendExecute(object obj)
        {
            IFriendEditViewModel friendEditVm = _friendEditViewModelCreator();
            FriendEditViewModels.Add(friendEditVm);
            friendEditVm.Load();
            SelectedFriendEditViewModel = friendEditVm;
        }

        private void OnOpenFriendTab(int friendId)
        {
            IFriendEditViewModel friendEditVm =
                FriendEditViewModels.SingleOrDefault(vm => vm.Friend.Id == friendId);
            if (friendEditVm == null)
            {
                friendEditVm = _friendEditViewModelCreator();
                FriendEditViewModels.Add(friendEditVm);
                friendEditVm.Load(friendId);
            }

            SelectedFriendEditViewModel = friendEditVm;
        }

        private void OnCloseFriendTabExecute(object parameter)
        {
            if (parameter is IFriendEditViewModel friendEditVmToClose)
            {
                if (friendEditVmToClose.Friend.IsChanged)
                {
                    var result = _messageDialogService.ShowYesNoDialog("Close tab?", "You'll lose your changes if you close this tab. Close it?", MessageDialogResult.No);
                    if (result == MessageDialogResult.No)
                    {
                        return;
                    }
                }

                FriendEditViewModels.Remove(friendEditVmToClose);
            }
        }

        private void OnFriendDeleted(int friendId)
        {
            IFriendEditViewModel friendDetailVmToClose
                = FriendEditViewModels.SingleOrDefault(f => f.Friend.Id == friendId);
            if (friendDetailVmToClose != null)
            {
                FriendEditViewModels.Remove(friendDetailVmToClose);
            }
        }

        public void OnClosing(CancelEventArgs cancelEventArgs)
        {
            if (FriendEditViewModels.Any(x => x.Friend.IsChanged))
            {
                var result = _messageDialogService.ShowYesNoDialog("Close application?", "You'll lose your changes if you close the application. Close it?", MessageDialogResult.No);
                cancelEventArgs.Cancel = result == MessageDialogResult.No;
            }
        }
    }
}