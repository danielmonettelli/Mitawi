using Mitawi.Models;
using Mitawi.Services;
using MvvmHelpers.Commands;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;

namespace Mitawi.ViewModels
{
    public class HomeViewModel : MyBaseViewModel
    {
        private readonly IWeatherDataService _weatherDataService;
        private readonly INavigationService _navigationService;

        private List<Hourly> _hourlies;
        public List<Hourly> Hourlies
        {
            get => _hourlies;
            set
            { _hourlies = value; OnPropertyChanged(); }
        }

        private Hourly _myHourly;
        public Hourly MyHourly
        {
            get => _myHourly;
            set
            {
                _myHourly = value;
                OnPropertyChanged();
            }
        }

        private List<Daily> _days;
        public List<Daily> Days
        {
            get => _days;
            set
            { _days = value; OnPropertyChanged(); }
        }

        private Placemark _myPlacemark;
        public Placemark MyPlacemark
        {
            get => _myPlacemark;
            set
            {
                _myPlacemark = value;
                OnPropertyChanged();
            }
        }

        public HomeViewModel(IWeatherDataService weatherDataService, INavigationService navigationService)
        {
            _weatherDataService = weatherDataService;
            _navigationService = navigationService;

            OnGetWeatherData();

            DailyForecast7DaysCommand = new AsyncCommand(OnDailyForecast7DaysCommand);
            SelectedHourlyCommand = new Command<Hourly>(OnSelectedHourlyCommand);
        }

        private async void OnGetWeatherData()
        {
            MyPlacemark = await _weatherDataService.GetPlacemarkAsync(false);
            Days = await _weatherDataService.GetDaysAsync(false);
            Hourlies = await _weatherDataService.GetHourliesAsync(false);

            // Get current time schedule
            MyHourly = Hourlies.ElementAt(0);
        }

        public ICommand SelectedHourlyCommand { get; }
        private void OnSelectedHourlyCommand(Hourly selectedHourly)
        {
            if (selectedHourly is not null)
            {
                MyHourly = selectedHourly;
            }
        }

        public ICommand DailyForecast7DaysCommand { get; }
        private async Task OnDailyForecast7DaysCommand()
        {
            await Task.Delay(150).ConfigureAwait(true);
            _navigationService.NavigateTo("HomeDetailPage", Days);
        }

    }
}
