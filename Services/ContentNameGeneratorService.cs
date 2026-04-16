using System;
using System.IO;
using System.Text;
using WindowsFormsApp1.Global;
using WindowsFormsApp1.Models;

namespace WindowsFormsApp1.Services
{
    public class ContentNameGeneratorService
    {
        public bool GenerateContentNameFile(string content, string filePath)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(content))
                {
                    return false;
                }

                if (File.Exists(filePath))
                {
                    File.Create(filePath).Close();
                }
                
          
                byte[] tempbytearr1 = UTF8Encoding.Unicode.GetBytes(content);
                File.WriteAllBytes(filePath, tempbytearr1);
                 

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to generate CONTENTNAME.TXT");
                return false;
            }
        }
    }
}
