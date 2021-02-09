using Covid19.Infrastructure.Commands;
using Covid19.Models;
using Covid19.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace Covid19.ViewModels
{
    class MainWindowViewModel : ViewModel
    {
        public MainWindowViewModel()
        {
            CloseApplicationCommand = new ActionCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);

            var data_points = new List<DataPoint>((int)(360 / 0.1));

            for (var x = 0d; x <= 360; x += 0.1)
            {
                const double to_rad = Math.PI / 180;
                var y = Math.Sin(x * to_rad);
                data_points.Add(new DataPoint { XValue = x, YValue = y });
            }

            TestDataPoints = data_points;
        }


        #region Свойства

        #region Заголовок окна

        /// <summary>Заголовок окна</summary>
        private string title = "Анализ статистики Covid-19";

        /// <summary>Заголовок окна</summary>
        public string Title
        {
            get => title;
            // Вот этот кусок кода:
            //set
            //{
            //    if (Equals(title, value)) return;
            //    title = value;
            //    OnPropertyChanged();
            //}
            // Можно заменить на:
            set => Set(ref title, value);
        }
        #endregion

        #region Статус программы

        /// <summary>Статус программы </summary>
        private string status = "Готов";

        /// <summary>Статус программы</summary>
        public string Status
        {
            get => status;
            set => Set(ref status, value);
        }
        #endregion

        #region Тестовый набор данных для визуализации графиков

        /// <summary>Тестовый набор данных для визуализации графиков</summary>
        private IEnumerable<DataPoint> testDataPoints;

        /// <summary>Тестовый набор данных для визуализации графиков</summary>
        public IEnumerable<DataPoint> TestDataPoints
        {
            get => testDataPoints;
            set => Set(ref testDataPoints, value);
        }
        #endregion

        #endregion

        #region Команды

        #region CloseApplicationCommand
        // Сама команда
        public ICommand CloseApplicationCommand { get; }

        // Доступна ли команда для выполнения
        private bool CanCloseApplicationCommandExecute(object p) => true;

        // Свойства, определяющие команду
        // В момент выполнения
        private void OnCloseApplicationCommandExecuted(object p)
        {
            Application.Current.Shutdown();
        }

       
        #endregion

        #endregion

    }
}
