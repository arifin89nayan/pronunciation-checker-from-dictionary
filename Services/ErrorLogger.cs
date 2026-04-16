using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Services
{
    public static class ErrorLogger
    {
       
        public static event Action<string> ErrorLogged;
        private static List<string> _errors = new List<string>();
        public static void LogError(string message)
        {
            string timeStampedMessage= $"[{DateTime.Now:HH:mm:ss}] {message}";
            _errors.Add(timeStampedMessage);
            ErrorLogged?.Invoke(timeStampedMessage); 
        }
        public static string GetAllErrors()
        {
            return string.Join(Environment.NewLine, _errors);
        }
    }

}
