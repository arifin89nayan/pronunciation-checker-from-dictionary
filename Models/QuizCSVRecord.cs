using CsvHelper.Configuration;

namespace WindowsFormsApp1.Models
{

    public class QuizCSVRecord
    {
        
        public string Id { get; set; }
        public string Question { get; set; }
        public string CorrectAnswer { get; set; }
        public string CorrectAnswerSelection { get; set; }
        public string AnsNumber1 { get; set; }
        public string AnsNumber2 { get; set; }
        public string AnsNumber3 { get; set; }
        public string AnsNumber4 { get; set; }
    }
    public class RecordMap : ClassMap<QuizCSVRecord>
    {
        public RecordMap()
        {
            
            Map(m => m.Id).Index(0);
            Map(m => m.Question).Index(1);
            Map(m => m.CorrectAnswer).Index(2);
            Map(m => m.CorrectAnswerSelection).Index(3);
            Map(m => m.AnsNumber1).Index(4);
            Map(m => m.AnsNumber2).Index(5);
            Map(m => m.AnsNumber3).Index(6);
            Map(m => m.AnsNumber4).Index(7);
        }
    }
}
