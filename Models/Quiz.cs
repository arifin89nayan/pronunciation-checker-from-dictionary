using System.Collections.Generic;
using System.Linq;

namespace WindowsFormsApp1.Models
{
    public class Quiz
    {
        public string Key { get; set; } = "EDOTKF1059C30324B00000063";
        public int CurrentOffset { get; set; }
        public string Question { get; set; }
        public List<Option> Options { get; set; } = new List<Option>();
        public int OptionsCount { get => Options.Count;}
        public string MediaFile { get; set; }
        public string CorrectAns { get => Options.FirstOrDefault(c => c.IsCorrectAns).Text; }
        public string CorrectAnsDetails { get; set; }

    }

    public class Option
    {
        public string Text { get; set; }
        //public string Value { get; set; } 
        public int Order { get; set; }
        public bool IsCorrectAns { get; set; }
    }
}
