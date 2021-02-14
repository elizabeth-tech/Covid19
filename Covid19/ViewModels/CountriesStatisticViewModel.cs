using Covid19.Infrastructure.Commands;
using Covid19.Models;
using Covid19.Services;
using Covid19.ViewModels.Base;
using System.Collections.Generic;
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

        #endregion

        public CountriesStatisticViewModel(MainWindowViewModel mainModel)
        {
            this.mainModel = mainModel;
            dataService = new DataService();

            // Получаем данные по странам
            RefreshDataCommand = new ActionCommand(OnRefreshDataCommandExecuted);
        }

        #region Команды

        #region RefreshDataCommand

        public ICommand RefreshDataCommand { get; }

        private void OnRefreshDataCommandExecuted(object p) => Countries = dataService.GetData();

        #endregion

        #endregion
    }
}
