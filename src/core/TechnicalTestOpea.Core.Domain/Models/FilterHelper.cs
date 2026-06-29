using System.Linq.Expressions;

namespace TechnicalTestOpea.Core.Domain.Models
{
    public class FilterHelper
    {
        public record GenericFilter<TEntity, TValue>(Expression<Func<TEntity, TValue>> PropertySelectior, TValue Value);

        public record BasePagedFilter(
            DateTime? InitialDate,
            DateTime? FinalDate,
            string? PropertyName,
            string? PropertyValue,
            string? OrderByColumn,
            bool? IsAcending,
            int Page = 1,
            int PageSize = 10)
        {
            public static implicit operator PagedRequest(BasePagedFilter filters)
            {
                return new PagedRequest
                {
                    InitialDate = filters.InitialDate,
                    FinalDate = filters.FinalDate,
                    PropertyName = filters.PropertyName,
                    PropertyValue = filters.PropertyValue,
                    OrderByColumn = filters.OrderByColumn,
                    IsAcending = filters.IsAcending,
                    Page = filters.Page,
                    PageSize = filters.PageSize
                };
            }
        }

        public record EntityProjections<T>(
            Expression<Func<T, T>> Full,
            Expression<Func<T, T>> Sumary,
            Expression<Func<T, T>> StatusOnly);

        public static Expression<Func<T, T>> SelectorProjection<T>(EntityProjections<T> entityProjections, string? selectType)
        {
            return selectType?.ToLower() switch
            {
                "summary" => entityProjections.Sumary,
                "status" => entityProjections.StatusOnly,
                _ => entityProjections.Full
            };
        }

        public static Expression<Func<TEntity, bool>> GetEqualExpression<TEntity>(Expression<Func<TEntity, object>> propertySelector, string value)
        {
            var parameter = propertySelector.Parameters[0];
            Expression body = propertySelector.Body;

            if (body is UnaryExpression { NodeType: ExpressionType.Convert } unary)
            { body = unary.Operand; }

            var targetType = body.Type;

            object convertedValue;

            if (targetType == typeof(Guid))
                convertedValue = Guid.Parse(value);
            else
                convertedValue = Convert.ChangeType(value, targetType);

            var constant = Expression.Constant(convertedValue, targetType);
            var equality = Expression.Equal(body, constant);

            return Expression.Lambda<Func<TEntity, bool>>(equality, parameter);
        }
    }
}
