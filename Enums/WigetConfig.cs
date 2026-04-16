using System.Collections.Generic;
using System.IO;
using WindowsFormsApp1.Helper;

namespace WindowsFormsApp1.Enums
{
    public class WigetConfig : WigetImage
    {
        public static List<string> GetImages() => new List<string>
        {
            Image_1,
            Image_2,
            Image_3,
        };

        public static string GetImagePath()
        {
            string path = Path.Combine(DirectoryHelper.GetCurrentDirectory(), "Images");

            return path;
        }
        public static string GetBaseImageName() => BaseImage;
    }
    public class WigetImage
    {
        protected const string Image_1 = "541B.JPG";
        protected const string Image_2 = "6DC3.JPG";
        protected const string Image_3 = "7571.JPG";
        protected const string BaseImage = "2000";
    }
}
