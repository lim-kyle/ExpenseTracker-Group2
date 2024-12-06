// ExpenseNotFound.cs
using System;

namespace ASI.Basecode.Data.Exceptions
{
    public class ExpenseTitleEmpty : Exception
    {
        public ExpenseTitleEmpty()
        {
        }

        public ExpenseTitleEmpty(string message)
            : base(message)
        {
        }

        public ExpenseTitleEmpty(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}