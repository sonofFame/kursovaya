using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace MemeFinder.Core
{
    /// <summary>
    /// Базовая класс, который запускает работу
    /// </summary>
    public class RelayParameterizedCommand : ICommand
    {
        #region Private Members

        /// <summary>
        /// Запуск действий
        /// </summary>
        private readonly Action<object> mAction;

        #endregion

        #region Public Events

        /// <summary>
        /// Событие, которое срабатывает, когда <see cref="CanExecute(object)"/> значение изменяется
        /// </summary>
        public event EventHandler CanExecuteChanged = (sender, e) => { };

        #endregion

        #region Constructor

        /// <summary>
        /// Стандартный конструктор
        /// </summary>
        public RelayParameterizedCommand(Action<object> action)
        {
            mAction = action;
        }

        #endregion

        #region Command Methods

        /// <summary>
        /// Команда ретрансляции всегда может быть выполнена
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Выполняет действие команды
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            mAction(parameter);
        }

        #endregion
    }
}
