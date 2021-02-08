using Covid19.ViewModels.Base;

namespace Covid19.ViewModels
{
    class MainWindowViewModel : ViewModel
    {
        #region Свойства

        #region Заголовок окна
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

        #endregion



    }
}
