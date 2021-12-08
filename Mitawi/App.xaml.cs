﻿using Mitawi.Models;
using Mitawi.Services;
using Mitawi.Utility;
using Mitawi.Views;
using Xamarin.Forms;

namespace Mitawi
{
    public partial class App : Application
    {
        public static NavigationService NavigationService { get; } = new NavigationService();
        public static IWeatherDataService WeatherDataServie { get; set; } = new WeatherDataService(new WeatherDataRepository());

        public App()
        {
            InitializeComponent();

            NavigationService.Configure(ViewNames.HomePage, typeof(HomePage));
            NavigationService.Configure(ViewNames.HomeDetailPage, typeof(HomeDetailPage));

            MainPage = new NavigationPage(new HomePage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
