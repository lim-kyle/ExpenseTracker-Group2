// ExpenseNotFound.cs
using System;

namespace ASI.Basecode.Data.Exceptions
{
    public class CategoryEmpty : Exception
    {
        public CategoryEmpty()
        {
        }

        public CategoryEmpty(string message)
            : base(message)
        {
        }

        public CategoryEmpty(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}