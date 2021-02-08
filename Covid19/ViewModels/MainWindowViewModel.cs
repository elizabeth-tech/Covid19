using Covid19.Infrastructure.Commands;
using Covid19.ViewModels.Base;
using System.Windows;
using System.Windows.Input;

namespace Covid19.ViewModels
{
    class MainWindowViewModel : ViewModel
    {
        public MainWindowViewModel()
        {
            CloseApplicationCommand = new ActionCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);
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
