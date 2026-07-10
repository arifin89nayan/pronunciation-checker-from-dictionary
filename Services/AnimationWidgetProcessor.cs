using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.Enums;
using WindowsFormsApp1.Global;
using WindowsFormsApp1.Helper;
using WindowsFormsApp1.Models.AutoConverters;
using WindowsFormsApp1.Models.Widgets;
using WindowsFormsApp1.Services.Abstraction;
using static System.Windows.Forms.LinkLabel;

namespace WindowsFormsApp1.Services
{
    public class AnimationWidgetProcessor : IAutoWidgetProcessor
    {
        private List<string> images = new List<string>();
        private int totalImagesProcessed = 0;
        private int timeInterval = 0;
        private GuideParsedModel fileData;
        private int yOffset = -60;
        private readonly FileService _fileService;
        
        public AnimationWidgetProcessor()
        {
            _fileService = new FileService();
           
        }
        public async Task ProcessWidges(Widget widget, WidgetParsedCommonModel item)
        {
            try
            {
                foreach (var line in widget.Lines)
                {
                    if (line.Type == LineTypeEnum.Animation)
                    {
                        fileData = item as GuideParsedModel;
                        //string text = item.GuideName;
                        var random = new Random().Next(100, 1000);

                        var items = PatternHelper.GetItems(line.Text, @"<([^>]+)>");

                        var lineData = ImageExtrator.ExtractAnimationLine(line.Text);
                        images = lineData.Images;
                        totalImagesProcessed = lineData.ImageCount;
                        timeInterval = lineData.TimeInterval;

                        string commaSeperatedImageNames = string.Empty;


                        for (int i = 0; i < images.Count; i++)
                        {
                            try
                            {
                                string outputImageName = Path.Combine(GlobalProperties.OutputPath, $"{random + i}.JPG");
                                //string outputImageName = Path.Combine(GlobalProperties.OutputPath, $"{random + i}.PNG");
                               

                                commaSeperatedImageNames += $"{random + i}.JPG";
                                //commaSeperatedImageNames += $"{random + i}.PNG";

                                if (i < images.Count - 1)
                                {
                                    commaSeperatedImageNames += ";";
                                }
                                Image image = Image.FromFile(Path.Combine(Path.GetDirectoryName(DirectoryHelper.SelectedPlayconfigFilePath), images[i]));

                                var imageWithTitle = DrawTextOnImage(image);
                                //imageWithTitle.Save(outputImageName, System.Drawing.Imaging.ImageFormat.Jpeg);


                                using (var encoderParams = new System.Drawing.Imaging.EncoderParameters(1))
                                {
                                    encoderParams.Param[0] = new System.Drawing.Imaging.EncoderParameter(
                                        System.Drawing.Imaging.Encoder.Quality, 100L); // 95% quality (range: 0-100)

                                    var jpegCodec = System.Drawing.Imaging.ImageCodecInfo.GetImageEncoders()
                                        .First(c => c.FormatID == System.Drawing.Imaging.ImageFormat.Jpeg.Guid);

                                    imageWithTitle.Save(outputImageName, jpegCodec, encoderParams);
                                }

                            }
                            catch (Exception ex)
                            {

                                MessageBox.Show($"Error saving imageName: {ex.Message}");
                            }
                        }
                        items[1] = $"<{commaSeperatedImageNames}>";
                        items[6] = $"<{images.Count}>";
                        items[7] = $"<{timeInterval}>";
                        GlobalConfigPropreties.Instance.AddLine(string.Join("", items));
                    }

                    else
                    {
                        GlobalConfigPropreties.Instance.AddLine(line.Text);
                        _fileService.SaveLineFiles(line);
                    }
                }

            }
            catch (Exception e)
            {

                MessageBox.Show(e.Message);
            } 
        }
        private Bitmap DrawTextOnImage(Image inputImage)
        {
            Bitmap imageWithText = new Bitmap(inputImage);
            string text = fileData.GuideName;

            string fontName = Properties.Settings.Default.AniFontName;
            fontName = ResolveFontForText(text, fontName);
            FontStyle fontStyle = (FontStyle)Enum.Parse(typeof(FontStyle), Properties.Settings.Default.AniFontStyle.ToString());

            float fontSize = Properties.Settings.Default.AniFontSize;
            if (!string.IsNullOrWhiteSpace(fileData.TitleFontSize) &&
            float.TryParse(fileData.TitleFontSize, NumberStyles.Float, CultureInfo.InvariantCulture, out float parsedSize))
            {
                fontSize = parsedSize;
            }
            //string fontColor = Properties.Settings.Default.AniFontColor;
            //if (!string.IsNullOrWhiteSpace(fileData.TitleFontColor))
            //{
            //    fontColor = fileData.TitleFontColor;
            //}
            System.Drawing.Color Fontcolor = ResolveColor(fileData.TitleFontColor, Properties.Settings.Default.AniFontColor);

            System.Drawing.Font font = new System.Drawing.Font(fontName, fontSize, fontStyle);
            System.Drawing.Color color = Fontcolor;    /*ColorTranslator.FromHtml(Fontcolor)*/
            SolidBrush brush = new SolidBrush(color);

            using (Graphics graphics = Graphics.FromImage(imageWithText))
            {
               // graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                //graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
                graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;

                int padding = 10;
                int maxWidth = imageWithText.Width - 2 * padding;

                SizeF textSize = graphics.MeasureString(text, font, maxWidth);
                float paddingTop = 20; // adjust as needed
                RectangleF textRect = new RectangleF(
                    padding,
                    paddingTop,
                    maxWidth,
                    textSize.Height
                );
                // Center align horizontally
                StringFormat sf = new StringFormat();
                sf.Alignment = StringAlignment.Center;
                sf.LineAlignment = StringAlignment.Near; // Top alignment in the rectangle

                graphics.DrawString(text, font, brush, textRect,sf);
            }

            return imageWithText;
        }
        static System.Drawing.Color ResolveColor(string excelValueOrHtml, string fallbackHtml)
        {
            // 1) If Excel cell is blank, use fallback html (e.g., "#000000" or "Black")
            if (string.IsNullOrWhiteSpace(excelValueOrHtml))
                return ColorTranslator.FromHtml(fallbackHtml);

            // 2) Try OLE integer from Excel (often looks like "6553600"; sometimes stored as double)
            if (double.TryParse(excelValueOrHtml, NumberStyles.Float, CultureInfo.InvariantCulture, out double dbl))
            {
                // Round to int (Excel numeric cells can be double)
                int ole = (int)System.Math.Round(dbl);
                return ColorTranslator.FromOle(ole); // handles BGR-packed OLE color
            }

            // 3) Try HTML-style (#RRGGBB, #AARRGGBB, named color like "Red")
            try
            {
                return ColorTranslator.FromHtml(excelValueOrHtml);
            }
            catch
            {
                // 4) Try "R,G,B" or "A,R,G,B"
                var parts = excelValueOrHtml.Split(',');
                if (parts.Length == 3 &&
                    byte.TryParse(parts[0].Trim(), out var r) &&
                    byte.TryParse(parts[1].Trim(), out var g) &&
                    byte.TryParse(parts[2].Trim(), out var b))
                {
                    return System.Drawing.Color.FromArgb(r, g, b);
                }
                if (parts.Length == 4 &&
                    byte.TryParse(parts[0].Trim(), out var a) &&
                    byte.TryParse(parts[1].Trim(), out r) &&
                    byte.TryParse(parts[2].Trim(), out g) &&
                    byte.TryParse(parts[3].Trim(), out b))
                {
                    return System.Drawing.Color.FromArgb(a, r, g, b);
                }
            }

            // 5) Final fallback
            return ColorTranslator.FromHtml(fallbackHtml);
        }

        private static string ResolveFontForText(string text, string defaultFontName)
        {
            bool hasCjk = text.Any(c =>
                (c >= 0x4E00 && c <= 0x9FFF) ||   // CJK Unified Ideographs (Chinese/Japanese kanji)
                (c >= 0x3400 && c <= 0x4DBF) ||   // CJK Extension A
                (c >= 0x3040 && c <= 0x30FF) ||   // Hiragana + Katakana
                (c >= 0xAC00 && c <= 0xD7AF));    // Hangul

            if (!hasCjk)
                return defaultFontName;

            // Try CJK fonts in order of preference (Traditional Chinese first for Taiwan)
            string[] cjkFonts = { "Microsoft JhengHei", "Microsoft YaHei", "Yu Gothic UI", "Meiryo", "MS Gothic" };

            using (var installed = new System.Drawing.Text.InstalledFontCollection())
            {
                var names = installed.Families.Select(f => f.Name).ToHashSet(StringComparer.OrdinalIgnoreCase);
                foreach (var f in cjkFonts)
                    if (names.Contains(f)) return f;
            }
            return defaultFontName;
        }
    }
}
