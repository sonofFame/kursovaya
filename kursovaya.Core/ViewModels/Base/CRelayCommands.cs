using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace kursovaya.Core
{
    /// <summary>
    /// Базовая команда, которая запускает действие
    /// </summary>
    public class CRelayCommands : ICommand
    {
        /// <summary>
        /// Действие для запуска
        /// </summary>
        private readonly Action mAction;

        /// <summary>
        /// Событие, которое срабатывает, когда <see cref="CanExecute(object)"/> значение изменяется
        /// </summary>
        public event EventHandler CanExecuteChanged = (sender, e) => { };

        /// <summary>
        /// Стандартный конструктор
        /// </summary>
        public CRelayCommands(Action action)
        {
            mAction = action;
        }

        /// <summary>
        /// Команда ретрансляции всегда может быть выполнена
        /// </summary>
        public bool CanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Выполняет действие команды
        /// </summary>
        public void Execute(object parameter)
        {
            mAction();
        }
    }
}
