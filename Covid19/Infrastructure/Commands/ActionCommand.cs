using Covid19.Infrastructure.Commands.Base;
using System;

namespace Covid19.Infrastructure.Commands
{
    internal class ActionCommand : Command
    {
        private readonly Action<object> _execute;
        private readonly Func<object, bool> _canExecute;

        /// <summary>
        /// Конструктор, которому указываем действия, которые может выполнять команда
        /// </summary>
        /// <param name="execute">Параметры, получаемые командой из разметки</param>
        /// <param name="canExecute"></param>
        public ActionCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(Execute));
            _canExecute = canExecute;
        }

        public ActionCommand(Action<object> execute) : this(execute, _ => true) { }

        // Если нет делегата, то считаем, что команду можно выполнить, т.е true
        public override bool CanExecute(object parameter) => _canExecute?.Invoke(parameter) ?? true;

        public override void Execute(object parameter) => _execute(parameter);
    }
}
