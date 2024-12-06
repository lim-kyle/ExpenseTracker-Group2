
using System;

namespace ASI.Basecode.Data.Exceptions
{
    public class ExpenseNotFound : Exception
    {
        public ExpenseNotFound()
        {
        }

        public ExpenseNotFound(string message)
            : base(message)
        {
        }

        public ExpenseNotFound(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}