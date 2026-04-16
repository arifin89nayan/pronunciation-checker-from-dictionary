using WindowsFormsApp1.Models;

namespace WindowsFormsApp1
{
    internal class QuizProperties
    {
        private static Quiz _instance;
        private static readonly object _lock = new object();
       
        public static Quiz Instance
        {
            get
            { 
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new Quiz();
                    }
                }
                return _instance;
            }
        }

        public int GetQuestionLength()
        {
            return _instance.Question.Length;
        }
    }
}
