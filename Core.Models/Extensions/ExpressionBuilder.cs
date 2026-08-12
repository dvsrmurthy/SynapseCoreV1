using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using Core.Models.Enums;


namespace Core.Models.Extensions
{
    public static class ExpressionBuilder
    {
        private static MethodInfo containsMethod = typeof(string).GetMethod("Contains");
        private static MethodInfo startsWithMethod = typeof(string).GetMethod("StartsWith", new Type[] { typeof(string) });
        private static MethodInfo endsWithMethod = typeof(string).GetMethod("EndsWith", new Type[] { typeof(string) });

        public static Expression<Func<T, bool>> GetExpression<T>(IList<Filter> filters)
        {
            if (filters.Count == 0)
                return null;

            var param = Expression.Parameter(typeof(T), "t");
            Expression exp = null;

            switch (filters.Count)
            {
                case 1:
                    exp = GetExpression<T>(param, filters[0]);
                    break;
                case 2:
                    exp = GetExpression<T>(param, filters[0], filters[1]);
                    break;
                default:
                    while (filters.Count > 0)
                    {
                        var f1 = filters[0];
                        var f2 = filters[1];

                        exp = exp == null
                            ? GetExpression<T>(param, filters[0], filters[1])
                            : Expression.AndAlso(exp, GetExpression<T>(param, filters[0], filters[1]));

                        filters.Remove(f1);
                        filters.Remove(f2);

                        if (filters.Count == 1)
                        {
                            exp = Expression.AndAlso(exp, GetExpression<T>(param, filters[0]));
                            filters.RemoveAt(0);
                        }
                    }
                    break;
            }

            return Expression.Lambda<Func<T, bool>>(exp, param);
        }

        private static Expression GetExpression(ParameterExpression param, ExtensionFilter filter)
        {
            var member = Expression.Property(param, filter.PropertyName);
            var constant = Expression.Constant(filter.Value);
            return Expression.Equal(member, constant);
        }

        private static Expression GetExpression<T>(ParameterExpression param, Filter filter)
        {
            var member = Expression.Property(param, filter.Property);
            var constant = Expression.Constant(filter.Value);
            return Expression.Equal(member, constant);
        }

        private static Expression GetExpression<T>(ParameterExpression param, ExtensionFilter filter)
        {
            var member = Expression.Property(param, filter.PropertyName);
            var constant = Expression.Constant(filter.Value);

            switch (filter.Operation)
            {
                case Op.Equals:
                    return Expression.Equal(member, constant);

                case Op.GreaterThan:
                    return Expression.GreaterThan(member, constant);

                case Op.GreaterThanOrEqual:
                    return Expression.GreaterThanOrEqual(member, constant);

                case Op.LessThan:
                    return Expression.LessThan(member, constant);

                case Op.LessThanOrEqual:
                    return Expression.LessThanOrEqual(member, constant);

                case Op.Contains:
                    return Expression.Call(member, containsMethod, constant);

                case Op.StartsWith:
                    return Expression.Call(member, startsWithMethod, constant);

                case Op.EndsWith:
                    return Expression.Call(member, endsWithMethod, constant);
            }

            return null;
        }

        private static BinaryExpression GetExpression(ParameterExpression param, ExtensionFilter filter1, ExtensionFilter filter2)
        {
            var bin1 = GetExpression(param, filter1);
            var bin2 = GetExpression(param, filter2);

            return Expression.AndAlso(bin1, bin2);
        }

        private static BinaryExpression GetExpression<T>(ParameterExpression param, Filter filter1, Filter filter2)
        {
            var bin1 = GetExpression<T>(param, filter1);
            var bin2 = GetExpression<T>(param, filter2);

            return Expression.AndAlso(bin1, bin2);
        }
    }
}
