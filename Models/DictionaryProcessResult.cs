using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Models
{
    public class DictionaryProcessResult
    {
      
        public int TotalRows { get; set; }
        public int ValidRows { get; set; }
        public int InvalidRows { get; set; }
        public int RemovedDuplicateRows { get; set; }
        public int RemovedContainmentRows { get; set; }
        public int FinalRows { get; set; }

        public string LogFilePath { get; set; }
        public string CleanedExcelPath { get; set; }
        public string XmlFilePath { get; set; }
    }
}
