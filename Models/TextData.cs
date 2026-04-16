using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Models
{
    public class TextData
    {
        private int _numberOfOptions;

        public string Key { get; set; }
        public string Question { get; set; }
        public string Option1 { get; set; }
        public string Option2 { get; set; }
        public string Option3 { get; set; }
        public string Option4 { get; set; }
        public string CorrectAnswer { get; set; }
        public string Description { get; set; }
        public int NumberOfOptions
        { 
            get
            {
                _numberOfOptions = 0;
                if (!string.IsNullOrWhiteSpace(Option1))
                {
                    _numberOfOptions += 1;
                }
                if (!string.IsNullOrWhiteSpace(Option2))
                {
                    _numberOfOptions += 1;
                }
                if (!string.IsNullOrWhiteSpace(Option3))
                {
                    _numberOfOptions += 1;
                }
                if (!string.IsNullOrWhiteSpace(Option4))
                {
                    _numberOfOptions += 1;
                }
                return _numberOfOptions;
            }
        }
    }
    public class QuestionData
    {
        public string Key { get; set; } = "EDOTKF1059C30324B00000063";
        public string Question { get; set; }
       
        
    }
}
