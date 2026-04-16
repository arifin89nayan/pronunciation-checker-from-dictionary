using System;

namespace WindowsFormsApp1.Exceptions
{
    internal class UnprocessableLineException : Exception
    {
        public UnprocessableLineException(string message = "Could not process the line!") : base(message) 
        {
            
        }
    }
}
