using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Exceptions
{
    internal class InvalidWidgetTypeException : Exception
    {
        public InvalidWidgetTypeException(string message = "Invalid widget type") : base(message)
        {
                
        }
    }
}
