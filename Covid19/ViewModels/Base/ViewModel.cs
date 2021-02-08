using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Covid19.ViewModels.Base
{
    internal abstract class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            // CallerMemberName - компилятор автоматически подставит имя метода, из которого вызывается OnPropertyChanged

            // Генерируем событие
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        ///  Обновляет значение свойства
        /// </summary>
        /// <param name="field"> Ссылка на поле свойства, в котором свойство хранит данные </param>
        /// <param name="value"> Новое значение, которое хотим установить </param>
        /// <param name="propertyName"> Имя свойства </param>
        /// <returns></returns>
        protected virtual bool Set<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            // Ели значение поля, которое хотим обновить, уже соответствует значению, которое передали,
            // то возвращем false
            // Флаг true или false позволяет определить, изменилось ли свойство и можем ли мы делать что то еще
            if (Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
