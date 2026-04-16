using System;

namespace WindowsFormsApp1.Exceptions
{
    internal class InvalidPlayConfigException : Exception
    {
        public InvalidPlayConfigException(string message = "Invalid playconfig!") : base(message)
        {
            
        }
    }
}
