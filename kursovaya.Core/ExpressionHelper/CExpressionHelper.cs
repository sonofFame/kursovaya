using System;
using System.Linq.Expressions;
using System.Reflection;

namespace kursovaya.Core
{
    /// <summary>
    /// класс - помощник для работы с выражениями
    /// </summary>
    public static class СExpressionHelpers
    {
        /// <summary>
        /// Компилирует выражение и получает возвращаемое функцией значение
        /// </summary>
        /// <typeparam name="T">Тип получаемого значения</typeparam>
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
        public static void SetPropertyValue<T>(this Expression<Func<T>> lambda, T value)
        {
            // Переводит параметр lambda()=> some.Property, в some.Property
            var expression = lambda.Body as MemberExpression;
            var propertyInfo = (PropertyInfo)expression.Member;
            var target = Expression.Lambda(expression.Expression).Compile().DynamicInvoke();
            // Устанавливает значение свойства
            propertyInfo.SetValue(target, value);
        }
    }
}