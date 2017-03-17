using carnotify.Events;
using carnotify.Interfaces;
using carnotify.Services;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System.Collections.ObjectModel;
using System.Threading;
using System.Windows.Input;

namespace carnotify.ViewModels
{
    public class RegisterCarPageViewModel : BindableBase, INavigationAware, IDestructible
    {
        private readonly ICarNotifyService _carNotifyService;
        private readonly IEventAggregator _eventAggregator;
        private readonly IPageDialogService _pageDialogService;
        private SubscriptionToken _loadedEventToken;
        private string _selectedCountry;
        private string _selectedPlate;
        private bool _isLoading;

        public ObservableCollection<string> Countries { get; } = new ObservableCollection<string>();

        public RegisterCarPageViewModel(IEventAggregator eventAggregator, IPageDialogService pageDialogService, ICarNotifyService carNotifyService)
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

        public ICommand RegisterCommand => new DelegateCommand(RegisterCar, canExecuteMethod: () =>
        {
            return !string.IsNullOrEmpty(SelectedPlate) && !string.IsNullOrEmpty(SelectedCountry) && !IsLoading;
        })
        .ObservesProperty(() => SelectedCountry)
        .ObservesProperty(() => SelectedPlate)
        .ObservesProperty(() => IsLoading);

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
                _eventAggregator.GetEvent<RegisterPageLoadedEvent>().Unsubscribe(_loadedEventToken);
            }
        }

        private void OnInitialized()
        {
            _loadedEventToken = _eventAggregator.GetEvent<RegisterPageLoadedEvent>().Subscribe(() => { });
        }

        private async void RegisterCar()
        {

            IsLoading = true;

            CancellationTokenSource cts = new CancellationTokenSource();
            var response = await _carNotifyService.RegisterCar(new Models.RegisterCarRequestModel() { Country = SelectedCountry, Plate = SelectedPlate, RegistrationId = PushRegistrationService.InstallationId }, cts.Token);
            if (response != null)
            {
                string message = string.Empty;
                if (response.Success)
                {
                    message = "Your Plate has been registered successfully.";
                }
                else
                {
                    message = response.Error;
                }
                await _pageDialogService.DisplayAlertAsync($"Register Car {PushRegistrationService.InstallationId}", message, "OK");
            }

            IsLoading = false;
        }
    }
}
