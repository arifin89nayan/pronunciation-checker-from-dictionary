using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp1.Models;

namespace WindowsFormsApp1.Services
{
    public class ConfirmationListStore
    {
        public List<ReviewItem> Items { get; } = new List<ReviewItem>();

        public bool IsEmpty => !Items.Any(i =>
            i.State == ReviewState.Pending || i.State == ReviewState.Editing);

        public void AddFromTerm(TtsTerm t)
        {
            if (Items.Any(i => i.Word == t.Word)) return;
            Items.Add(new ReviewItem
            {
                Word = t.Word,
                ApiReading = t.ModelSuggestedReading ?? t.Hiragana,
                CorrectReading = t.Hiragana,
                SourceSentence = t.SourceSentence,
                Category = t.Category ?? "general_word",
                State = ReviewState.Pending
            });
        }

        public void Clear() => Items.Clear();

        public void ExportCsv(string path)
        {
            var sb = new StringBuilder();
            sb.AppendLine("word,api_reading,correct_reading,category,save_type,state,source_sentence");
            foreach (var i in Items)
                sb.AppendLine(string.Join(",",
                    Csv(i.Word), Csv(i.ApiReading), Csv(i.CorrectReading),
                    Csv(i.Category), Csv(i.SaveType), i.State, Csv(i.SourceSentence)));
            File.WriteAllText(path, sb.ToString(), new UTF8Encoding(true));
        }

        private static string Csv(string v) =>
            string.IsNullOrEmpty(v) ? "" : "\"" + v.Replace("\"", "\"\"") + "\"";
    }
}
