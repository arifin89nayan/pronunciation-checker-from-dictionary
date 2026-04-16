using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Models
{
    public class DictionaryRowModel
    {
        public int RowNumber { get; set; }
        public string SerialNo { get; set; }
        public string Word { get; set; }
        public string Phoneme { get; set; }
        public string Status { get; set; } = "Read";
        public bool IsRemoved { get; set; } = false;
        public string RemovedReason { get; set; } = "";
    }
}
