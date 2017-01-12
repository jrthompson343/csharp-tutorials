using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CSharpFundamentals.Examples.LINQ
{
    public class ExpressionRunner : IExampleRunner
    {
        public void RunExample()
        {
            Func<int, int, int> multiplyFunc = (x, y) => x*y;
            Expression<Func<int, int, int>> multiplyExpression = (x, y) => x*y;

            Func<int, int, int> mult = multiplyExpression.Compile();


            int product = multiplyFunc(3, 4);
            Console.WriteLine(product);

            product = mult(3, 3);
            Console.WriteLine( product);
        }
    }
}
