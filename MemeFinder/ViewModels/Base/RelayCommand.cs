using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace MemeFinder.Core
{
    /// <summary>
    /// Базовая команда, которая запускает действие
    /// </summary>
    public class RelayCommand : ICommand
    {
        #region Private Members

        /// <summary>
        /// Действие для запуска
        /// </summary>
        private readonly Action mAction;

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
        public RelayCommand(Action action)
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
            mAction();
        }

        #endregion
    }
}
