// ExpenseNotFound.cs
using System;

namespace ASI.Basecode.Data.Exceptions
{
    public class InvalidAmount : Exception
    {
        public InvalidAmount()
        {
        }

        public InvalidAmount(string message)
            : base(message)
        {
        }

        public InvalidAmount(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}