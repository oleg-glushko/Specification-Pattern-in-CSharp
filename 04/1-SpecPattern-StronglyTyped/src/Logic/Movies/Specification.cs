using System.Linq.Expressions;

namespace Logic.Movies;

internal sealed class IdentitySpecification<T> : Specification<T>
{
    public override Expression<Func<T, bool>> ToExpression()
    {
        return x => true;
    }
}

public abstract class Specification<T>
{
    public static readonly Specification<T> All = new IdentitySpecification<T>();

    public bool IsSatisfiedBy(T entity)
    {
        Func<T, bool> predicate = ToExpression().Compile();
        return predicate(entity);
    }

    public abstract Expression<Func<T, bool>> ToExpression();

    public Specification<T> And(Specification<T> specification)
    {
        if (this == All)
            return specification;
        if (specification == All)
            return this;

        return new AndSpecification<T>(this, specification);
    }

    public Specification<T> Or(Specification<T> specification)
    {
        if (this == All || specification == All)
            return All;

        return new OrSpecification<T>(this, specification);
    }

    public Specification<T> Not()
    {
        return new NotSpecification<T>(this);
    }
}

internal sealed class AndSpecification<T> : Specification<T>
{
    private readonly Specification<T> _left;
    private readonly Specification<T> _right;
    private readonly SubstExpressionVisitor visitor;

    public AndSpecification(Specification<T> left, Specification<T> right)
    {
        _left = left;
        _right = right;
        visitor = new SubstExpressionVisitor();
    }

    public override Expression<Func<T, bool>> ToExpression()
    {
        Expression<Func<T, bool>> leftExpression = _left.ToExpression();
        Expression<Func<T, bool>> rightExpression = _right.ToExpression();

        // Necessary for EF Core
        ParameterExpression param = leftExpression.Parameters.Single();
        visitor.subst[rightExpression.Parameters.Single()] = param;
        var rightExpressionBody = visitor.Visit(rightExpression.Body);

        BinaryExpression andExpression = Expression.AndAlso(leftExpression.Body, rightExpressionBody);

        return Expression.Lambda<Func<T, bool>>(andExpression, leftExpression.Parameters.Single());
    }
}

internal sealed class OrSpecification<T> : Specification<T>
{
    private readonly Specification<T> _left;
    private readonly Specification<T> _right;
    private readonly SubstExpressionVisitor visitor;

    public OrSpecification(Specification<T> left, Specification<T> right)
    {
        _left = left;
        _right = right;
        visitor = new SubstExpressionVisitor();
    }

    public override Expression<Func<T, bool>> ToExpression()
    {
        Expression<Func<T, bool>> leftExpression = _left.ToExpression();
        Expression<Func<T, bool>> rightExpression = _right.ToExpression();

        // Necessary for EF Core
        ParameterExpression param = leftExpression.Parameters.Single();
        visitor.subst[rightExpression.Parameters.Single()] = param;
        var rightExpressionBody = visitor.Visit(rightExpression.Body);

        BinaryExpression orExpression = Expression.OrElse(leftExpression.Body, rightExpressionBody);

        return Expression.Lambda<Func<T, bool>>(orExpression, leftExpression.Parameters.Single());
    }
}

internal sealed class NotSpecification<T> : Specification<T>
{
    private readonly Specification<T> _specification;

    public NotSpecification(Specification<T> specification)
    {
        _specification = specification;
    }

    public override Expression<Func<T, bool>> ToExpression()
    {
        Expression<Func<T, bool>> expression = _specification.ToExpression();
        UnaryExpression notExpression = Expression.Not(expression.Body);

        return Expression.Lambda<Func<T, bool>>(notExpression, expression.Parameters.Single());
    }
}

internal class SubstExpressionVisitor : ExpressionVisitor
{
    public Dictionary<Expression, Expression> subst = new Dictionary<Expression, Expression>();

    protected override Expression VisitParameter(ParameterExpression node)
    {
        Expression newValue;
        return subst.TryGetValue(node, out newValue!) ? newValue : node;
    }
}

public sealed class MovieForKidsSpecification : Specification<Movie>
{
    public override Expression<Func<Movie, bool>> ToExpression()
    {
        return movie => movie.MpaaRating <= MpaaRating.PG;
    }
}

public sealed class AvailableOnCDSpecification : Specification<Movie>
{
    private const int MonthsBeforeDVDIsOut = 6;

    public override Expression<Func<Movie, bool>> ToExpression()
    {
        return movie => movie.ReleaseDate <= DateTime.Now.AddMonths(-MonthsBeforeDVDIsOut);
    }
}
