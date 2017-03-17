using carnotify.Constants;
using carnotify.Events;
using carnotify.Interfaces;
using carnotify.Utils;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace carnotify.ViewModels
{
    public class NotifyCarPageViewModel : BindableBase, INavigationAware, IDestructible
    {
        private readonly ICarNotifyService _carNotifyService;
        private readonly IEventAggregator _eventAggregator;
        private readonly IPageDialogService _pageDialogService;
        private SubscriptionToken _loadedEventToken;
        private string _selectedCountry;
        private string _selectedPlate;
        private bool _isLoading;
        public ObservableCollection<string> Countries { get; } = new ObservableCollection<string>();

        public NotifyCarPageViewModel(IEventAggregator eventAggregator, IPageDialogService pageDialogService, ICarNotifyService carNotifyService)
        {
            _eventAggregator = eventAggregator;
            _pageDialogService = pageDialogService;
            _carNotifyService = carNotifyService;
            Countries.Add("DE");
            Countries.Add("NL");
            OnInitialized();
        }

        public string SelectedCountry
        {
            get { return _selectedCountry; }
            set { SetProperty(ref _selectedCountry, value); }
        }

        public string SelectedPlate
        {
            get { return _selectedPlate; }
            set { SetProperty(ref _selectedPlate, value); }
        }

        public bool IsLoading
        {
            get { return _isLoading; }
            set { SetProperty(ref _isLoading, value); }
        }

        public ICommand NotifyCommand => new DelegateCommand(NotifyCar, canExecuteMethod: () => {
            return !string.IsNullOrEmpty(SelectedPlate) && !string.IsNullOrEmpty(SelectedCountry) && !IsLoading;
        })
        .ObservesProperty(() => SelectedCountry)
        .ObservesProperty(() => SelectedPlate)
        .ObservesProperty(() => IsLoading);

        public ICommand TakePictureCommand => new DelegateCommand(TakePicture);

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
        }

        public void Destroy()
        {
            if (_loadedEventToken != null)
            {
                _eventAggregator.GetEvent<NotifyPageLoadedEvent>().Unsubscribe(_loadedEventToken);
            }
        }

        private void OnInitialized()
        {
            _loadedEventToken = _eventAggregator.GetEvent<NotifyPageLoadedEvent>().Subscribe(() => { });
        }

        private async void NotifyCar()
        {
            IsLoading = true;
            var notifyWithMessageCommand = new DelegateCommand<string>(NotifyWithTemplate);
            var actionSheetButtons = new List<IActionSheetButton>();
            foreach (var message in TemplateConstants.TemplateMessageToName.Keys)
            {
                actionSheetButtons.Add(ActionSheetButton.CreateButton(message, notifyWithMessageCommand));
            }
            await _pageDialogService.DisplayActionSheetAsync("What happend?", actionSheetButtons.ToArray());
            IsLoading = false;
        }

        private async void NotifyWithTemplate(string message)
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            var template = string.Empty;
            if(TemplateConstants.TemplateMessageToName.TryGetValue(message, out template)) 
            //if (TemplateConstants.TemplateMessageToName.TryGetValue(message, out var template))
            {
                var response = await _carNotifyService.NotifyCar(new Models.NotifyCarRequestModel() { Country = SelectedCountry, Plate = SelectedPlate, MessageId = template }, cts.Token);
                if (response != null)
                {
                    string errorMessage = string.Empty;
                    if (response.Success)
                    {
                        errorMessage = "The Car Owner has been notified successfully.";
                    }
                    else
                    {
                        errorMessage = response.Error;
                    }
                    await _pageDialogService.DisplayAlertAsync("Notify Car", errorMessage, "OK");
                }
            }
        }

        private async void TakePicture()
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            var text = await TakePictureUtility.TakePictureTextAsync();
            if(!string.IsNullOrWhiteSpace(text))
            {
                SelectedPlate = text;
            }
        }
    }
}
