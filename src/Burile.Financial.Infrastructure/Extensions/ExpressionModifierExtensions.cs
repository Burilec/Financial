using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using Burile.Financial.Infrastructure.Filters;

namespace Burile.Financial.Infrastructure.Extensions;

public static class ExpressionModifierExtensions
{
    public static Expression<Func<T, bool>> Modify<T>(this Expression firstExpression, Expression secondExpression)
    {
        var bodyIdentifier = new ExpressionBodyIdentifier();
        var body = bodyIdentifier.Identify(firstExpression);
        var parameterIdentifier = new ExpressionParameterIdentifier();
        var parameter = (ParameterExpression)parameterIdentifier.Identify(firstExpression);
        var body2 = bodyIdentifier.Identify(secondExpression);
        var parameter2 = (ParameterExpression)parameterIdentifier.Identify(secondExpression);

        var treeModifier = new ExpressionReplacer(parameter2, body);
        return Expression.Lambda<Func<T, bool>>(treeModifier.Visit(body2), parameter);
    }

    internal static Expression ReplaceBinary(this Expression exp, ExpressionType from, ExpressionType to)
        => new BinaryReplacer(from, to).Visit(exp);

    internal static Expression<Func<T, bool>> GenerateBinary<T, TProperty>(this Filter<T, TProperty> filter,
                                                                           ExpressionType binaryOperation,
                                                                           TProperty? value)
    {
        var body = new ExpressionBodyIdentifier().Identify(filter.PropertyExpression);
        var parameter = (ParameterExpression)new ExpressionParameterIdentifier().Identify(filter.PropertyExpression);

        var binaryExpression = Expression.MakeBinary(binaryOperation, body,
                                                     Expression.Convert(Expression.Constant(value), body.Type));

        return Expression.Lambda<Func<T, bool>>(binaryExpression, parameter);
    }

    internal static Expression<Func<T, bool>> GenerateBinary<T, TProperty>(this Filter<T, TProperty> filter,
                                                                           ExpressionType binaryOperation)
    {
        var body = new ExpressionBodyIdentifier().Identify(filter.PropertyExpression);
        var parameter = (ParameterExpression)new ExpressionParameterIdentifier().Identify(filter.PropertyExpression);

        var binaryExpression = Expression.MakeBinary(binaryOperation, body,
                                                     Expression.Convert(Expression.Constant(null), body.Type));

        return Expression.Lambda<Func<T, bool>>(binaryExpression, parameter);
    }

    public static Expression<Func<T, TU>> ChangeExpressionReturnType<T, TU>(this Expression expression)
    {
        var bodyIdentifier = new ExpressionBodyIdentifier();
        var body = bodyIdentifier.Identify(expression);
        var parameterIdentifier = new ExpressionParameterIdentifier();
        var parameter = (ParameterExpression)parameterIdentifier.Identify(expression);

        if (body.Type is TU)
        {
            // Expression already has the right type.
            return Expression.Lambda<Func<T, TU>>(body, parameter);
        }

        // Change parameter.
        var converted = Expression.Convert(body, typeof(TU));
        return Expression.Lambda<Func<T, TU>>(converted, parameter);
    }

    private sealed class ExpressionReplacer(Expression from, Expression to) : ExpressionVisitor
    {
        [return: NotNullIfNotNull(nameof(node))]
        public override Expression? Visit(Expression? node)
            => node == from ? to : base.Visit(node);
    }

    private sealed class ExpressionBodyIdentifier : ExpressionVisitor
    {
        public Expression Identify(Expression node)
            => base.Visit(node);

        protected override Expression VisitLambda<T>(Expression<T> node)
            => node.Body;
    }

    private sealed class ExpressionParameterIdentifier : ExpressionVisitor
    {
        public Expression Identify(Expression node)
            => base.Visit(node);

        protected override Expression VisitLambda<T>(Expression<T> node)
            => node.Parameters[0];
    }

    private sealed class BinaryReplacer(ExpressionType from, ExpressionType to) : ExpressionVisitor
    {
        protected override Expression VisitBinary(BinaryExpression node)
            => node.NodeType == from ? Expression.MakeBinary(to, node.Left, node.Right) : base.VisitBinary(node);
    }
}