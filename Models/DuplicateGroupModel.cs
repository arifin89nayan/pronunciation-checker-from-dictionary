using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Models
{
    public class DuplicateGroupModel
    {
        public string Word { get; set; }
        public List<DictionaryRowModel> Rows { get; set; } = new List<DictionaryRowModel>();
        public bool HasConflict { get; set; }
    }
}
