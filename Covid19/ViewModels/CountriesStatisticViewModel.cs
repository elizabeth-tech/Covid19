using Covid19.Infrastructure.Commands;
using Covid19.Models;
using Covid19.Services;
using Covid19.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Covid19.ViewModels
{
    internal class CountriesStatisticViewModel : ViewModel
    {
        #region Свойства

        private DataService dataService; // сервис, который извлекает данные

        private MainWindowViewModel mainModel { get; }

        #region Информация по странам

        /// <summary>Статистика по странам</summary>
        private IEnumerable<CountryInfo> countries;

        /// <summary>Статистика по странам</summary>
        public IEnumerable<CountryInfo> Countries
        {
            get => countries;
            private set => Set(ref countries, value);
        }

        #endregion

        #region Выбранная страна

        /// <summary>Выбранная страна</summary>
        private CountryInfo selectedCountry;

        /// <summary>Выбранная страна</summary>
        public CountryInfo SelectedCountry
        {
            get => selectedCountry;
            private set => Set(ref selectedCountry, value);
        }

        #endregion

        #endregion

        public CountriesStatisticViewModel(MainWindowViewModel mainModel)
        {
            this.mainModel = mainModel;
            dataService = new DataService();

            // Получаем данные по странам
            RefreshDataCommand = new ActionCommand(OnRefreshDataCommandExecuted);
        }


        //// Отладочный конструктор, используемый в процессе разработки в визуальном дизайнере
        //public CountriesStatisticViewModel() : this(null)
        //{
        //    Countries = Enumerable.Range(1, 10).Select(i => new CountryInfo
        //    {
        //        Name = $"Country i",
        //        ProvinceCounts = Enumerable.Range(1, 10).Select(j => new PlaceInfo
        //        {
        //            Name = $"Province {i}",
        //            Location = new Point(i, j),
        //            Counts = Enumerable.Range(1, 10).Select(k => new ConfirmedCount
        //            {
        //                Date = DateTime.Now.Subtract(TimeSpan.FromDays(100 - k)),
        //                Count = k
        //            }).ToArray()
        //        }).ToArray()
        //    }).ToArray();
        //}

        #region Команды

        #region RefreshDataCommand

        public ICommand RefreshDataCommand { get; }

        private void OnRefreshDataCommandExecuted(object p) => Countries = dataService.GetData();

        #endregion

        #endregion
    }
}
