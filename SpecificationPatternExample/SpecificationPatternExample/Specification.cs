using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SpecificationPatternExample
{
    public abstract class Specification<T>
    {
        public abstract Expression<Func<T, bool>> AsExpression();

        public bool IsSatisfiedBy(T entity)
        {
            Expression<Func<T,bool>> expression = AsExpression();


            Func<T, bool> predicate = expression.Compile();
            return predicate(entity);
        }

        public Specification<T> Or(Specification<T> specification)
        {
            return new OrSpecification<T>(this, specification);
        }
    }

    public class OrSpecification<T> : Specification<T>
    {
        private readonly Specification<T> _left;
        private readonly Specification<T> _right;

        public OrSpecification(Specification<T> left, Specification<T> right)
        {
            _right = right;
            _left = left;
        }

        public override Expression<Func<T, bool>> AsExpression()
        {
            ParameterExpression param = Expression.Parameter(typeof (T), "d");
            Expression<Func<T, bool>> leftExpression = _left.AsExpression();
            Expression<Func<T, bool>> rightExpression = _right.AsExpression();
            BinaryExpression orExpression = Expression.OrElse(leftExpression.Body, rightExpression.Body);

            return Expression.Lambda<Func<T, bool>>(orExpression, new [] { param });
        }
    }
}
