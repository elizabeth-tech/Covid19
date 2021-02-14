using Covid19.Services;
using Covid19.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Covid19.ViewModels
{
    internal class CountriesStatisticViewModel : ViewModel
    {
        private DataService dataService; // сервис, который извлекает данные

        private  MainWindowViewModel mainModel { get; }

        public CountriesStatisticViewModel(MainWindowViewModel mainModel)
        {
            this.mainModel = mainModel;
            dataService = new DataService();
        }
    }
}
