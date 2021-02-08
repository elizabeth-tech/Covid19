using System;
using System.Windows.Input;

namespace Covid19.Infrastructure.Commands.Base
{
    internal abstract class Command : ICommand
    {
        // Событие, которое генерируется, когда CanExecute возвращает другое значение
        public event EventHandler CanExecuteChanged
        {
            // Явно реализуем методы add и remove
            // Передаем управление CommandManager
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        // Проверка, можно ли выполнить команду
        public abstract bool CanExecute(object parameter);

        // То, что должно быть выполнено командой
        public abstract void Execute(object parameter);
    }
}
