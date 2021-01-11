using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

{
    /// <summary>
    /// Базовая класс, который запускает работу
    /// </summary>
    public class CRelayParametrizedCommands : ICommand
    {
        /// <summary>
        /// Запуск действий
        /// </summary>
        private readonly Action<object> mAction;

        /// <summary>
        /// Событие, которое срабатывает, когда <see cref="CanExecute(object)"/> значение изменилось
        /// </summary>
        public event EventHandler CanExecuteChanged = (sender, e) => { };

        /// <summary>
        /// стандартный конструктор
        /// </summary>
        public CRelayParametrizedCommands(Action<object> action)
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
            mAction(parameter);
        }
    }
}
