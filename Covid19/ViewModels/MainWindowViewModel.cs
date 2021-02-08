using Covid19.ViewModels.Base;

namespace Covid19.ViewModels
{
    class MainWindowViewModel : ViewModel
    {
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

        #endregion



    }
}
