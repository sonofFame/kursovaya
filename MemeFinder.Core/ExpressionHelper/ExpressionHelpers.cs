using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace MemeFinder.Core
{
    /// <summary>
    ///  A helper for expressions
    /// </summary>
    public static class ExpressionHelpers
    {
        /// <summary>
        /// Compiles an expression and gets the functions return value
        /// </summary>
        /// <typeparam name="T">Type of value to get</typeparam>
        /// <param name="lambda">An expression</param>
        /// <returns></returns>
        public static T GetPropertyValue<T>(this Expression<Func<T>> lambda)
        {
            return lambda.Compile().Invoke();
        }
        /// <summary>
        /// Sets the underlying properties value to the given value
        /// from an expression that contains the property
        /// </summary>
        /// <typeparam name="T">Type of value to set</typeparam>
        /// <param name="lambda">An expression</param>
        public static void SetPropertyValue<T>(this Expression<Func<T>> lambda,T value)
        {
            // Converts a lambda ()=> some.Property, to some.Property
            var expression = lambda.Body as MemberExpression;
            var propertyInfo = (PropertyInfo)expression.Member;
            var target = Expression.Lambda(expression.Expression).Compile().DynamicInvoke();
            // Set the property value
            propertyInfo.SetValue(target, value);


        }
    }
}
