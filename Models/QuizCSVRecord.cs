using CsvHelper.Configuration;

namespace WindowsFormsApp1.Models
{
    public class QuizCSVRecord
    {
        //public string Id { get; set; }
        //public string Question { get; set; }
        //public string CorrectAnswer { get; set; }
        //public string Column1 { get; set; }
        //public string Column2 { get; set; }
        //public string Column3 { get; set; }
        //public string Column4 { get; set; }
        //public string Column5 { get; set; }
        //public string SelectionNumber { get; set; }
        //public string CorrectAnswerSelection { get; set; }
        public string Id { get; set; }
        public string Question { get; set; }
        public string CorrectAnswer { get; set; }
        public string CorrectAnswerSelection { get; set; }
        public string AnsNumber2 { get; set; }
        public string AnsNumber3 { get; set; }
        public string AnsNumber4 { get; set; }
    }
    public class RecordMap : ClassMap<QuizCSVRecord>
    {
        public RecordMap()
        {
            //Map(m => m.Id).Index(0);
            //Map(m => m.Question).Index(1);
            //Map(m => m.CorrectAnswer).Index(2);
            ////Map(m => m.Column1).Index(3);
            ////Map(m => m.Column2).Index(4);
            //Map(m => m.Column3).Index(5);
            //Map(m => m.Column4).Index(6);
            //Map(m => m.Column5).Index(7);
            //Map(m => m.SelectionNumber).Index(3);
            //Map(m => m.CorrectAnswerSelection).Index(4);
            Map(m => m.Id).Index(0);
            Map(m => m.Question).Index(1);
            Map(m => m.CorrectAnswer).Index(2);
            Map(m => m.CorrectAnswerSelection).Index(3);
            Map(m => m.AnsNumber2).Index(4);
            Map(m => m.AnsNumber3).Index(5);
            Map(m => m.AnsNumber4).Index(6);
        }
    }
}
