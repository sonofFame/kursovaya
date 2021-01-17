using PropertyChanged;
using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Windows;

namespace MemeFinder.Core
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Событие, которое срабатывает, когда любое дочернее свойство изменяет свое значение
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };
        public void OnPropertyChanged(string name)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
        #region Command Helpers
        /// <summary>
        /// Запускает команду, если флажок обновления не установлен
        /// Если флажок true => указывает на то, что функция уже запущена, то действие не выполняется.
        /// После завершения действия, если оно было выполнено, флажок сбрасывается на false
        /// </summary>
        /// <param name="updatingFlag"> Флажок логического свойства, определяющий, выполняется ли команда уже </param>
        /// <param name="action"> Действие, выполняемое, если команда еще не запущена </param>
        /// <returns></returns>
        protected async Task RunCommand(Expression<Func<bool>> updatingFlag, Func<Task> action)
        {
            if (updatingFlag.GetPropertyValue())
                  return;
            // Установите флажок свойства в значение true, чтобы указать, что мы работаем
            updatingFlag.SetPropertyValue(true);
            try
            {
                // Приводит запуск в действие
                await action();
            }
            finally
            {
                // Установите флажок свойства обратно в false - теперь он закончен
                updatingFlag.SetPropertyValue(false);
            }

        }
        #endregion
    }
}
