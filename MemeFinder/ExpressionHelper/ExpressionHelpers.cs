using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace MemeFinder.Core
{
    /// <summary>
    /// класс - помощник для работы с выражениями
    /// </summary>
    public static class ExpressionHelpers
    {
        /// <summary>
        /// Компилирует выражение и получает возвращаемое функцией значение
        /// </summary>
        /// <typeparam name="T">Тип получаемого значенияt</typeparam>
        /// <param name="lambda">Само выражение</param>
        /// <returns></returns>
        public static T GetPropertyValue<T>(this Expression<Func<T>> lambda)
        {
            return lambda.Compile().Invoke();
        }

        /// <summary>
        /// Устанавливает базовое значение свойств на заданное значение из выражения, содержащего это свойство
        /// </summary>
        /// <typeparam name="T">Тип устанавливаемого значения</typeparam>
        /// <param name="lambda">Само выражение</param>
        public static void SetPropertyValue<T>(this Expression<Func<T>> lambda,T value)
        {
            // Converts a lambda ()=> some.Property, to some.Property
            var expression = lambda.Body as MemberExpression;
            var propertyInfo = (PropertyInfo)expression.Member;
            var target = Expression.Lambda(expression.Expression).Compile().DynamicInvoke();
            // Устанавливает значение свойства
            propertyInfo.SetValue(target, value);


        }
    }
}
