using System;

using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using WindowsFormsApp1.Global;

using System.Collections.Generic;
namespace WindowsFormsApp1.Helper
{
    public class imageService
    {
        //string imageNumber = "1000";
        private static int imageCounter = 1;

        
        public struct PARAFORMAT2
        {
            public int cbSize;
            public uint dwMask;
            public short wNumbering;
            public short wReserved;
            public int dxStartIndent;
            public int dxRightIndent;
            public int dxOffset;
            public short wAlignment;
            public short cTabCount;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public int[] rgxTabs;
            public int dySpaceBefore;
            public int dySpaceAfter;
            public int dyLineSpacing;
            public short sStyle;
            public byte bLineSpacingRule;
            public byte bOutlineLevel;
            public short wShadingWeight;
            public short wShadingStyle;
            public short wNumberingStart;
            public short wNumberingStyle;
            public short wNumberingTab;
            public short wBorderSpace;
            public short wBorderWidth;
            public short wBorders;
        }

        private const int PFM_ALIGNMENT = 0x00000008;
        private const int PFM_LINESPACING = 0x00000100;
        private const int SCF_ALL = 0x0004;
        private const int EM_SETPARAFORMAT = 0x0447;
        private const short PFA_JUSTIFY = 4;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(
            IntPtr hWnd,
            int msg,
            int wParam,
            ref PARAFORMAT2 lParam
        );


        // Justify text (as before)
        public static void FormatRichTextBox(RichTextBox rtb, float lineSpacingFactor = 1f)
        {
            var fmt = new PARAFORMAT2
            {
                cbSize = Marshal.SizeOf<PARAFORMAT2>(),
                dwMask = PFM_ALIGNMENT | PFM_LINESPACING,
                wAlignment = PFA_JUSTIFY,
                bLineSpacingRule = 4,  // “exactly”
                dyLineSpacing = (int)(rtb.Font.SizeInPoints * 20f * lineSpacingFactor)
            };

            SendMessage(rtb.Handle, EM_SETPARAFORMAT, SCF_ALL, ref fmt);
        }
        private string GetFontForLanguage(string language)
        {
            if (string.IsNullOrEmpty(language))
                return Properties.Settings.Default.CapFontName;

            string lang = language.Trim().ToLowerInvariant();

            if (lang.Contains("zh") || lang.Contains("chin") || lang.Contains("中"))
                return "Microsoft YaHei";          // Simplified Chinese
            if (lang.Contains("ja") || lang.Contains("jap") || lang.Contains("日"))
                return "Meiryo";                   // or MS Gothic

            return Properties.Settings.Default.CapFontName;
        }

        public string GenerateImage(TxtToImage model)
        {
            // 1) Load settings
            string text = model.InputText;
            string fontName = GetFontForLanguage(model.Language);
            //string fontName = Properties.Settings.Default.CapFontName; 
            float fontSize = Properties.Settings.Default.CapFontSize;
            Color fg = ColorTranslator.FromHtml(Properties.Settings.Default.CapFontColor);
            Color bg = ColorTranslator.FromHtml(Properties.Settings.Default.CapBgColor);
            FontStyle fs = FontStyle.Bold;

            int contentWidth = 240; // Always 240px content
            
            int minHeight = 109;

            // 2) File path logic
            int baseNumber = (imageCounter == 1 ? 1000 : imageCounter * 10000);
            string fileName = $"{baseNumber}.jpg";
            string directory = DirectoryHelper.GetResDirectory();
            string outputPath = Path.Combine(directory, fileName);
            if (File.Exists(outputPath)) File.Delete(outputPath);

            // 3) Calculate required height using Method 3 - Most reliable approach
            int requiredHeight = CalculateHeightWithActualRTB(text, fontName, fontSize, fs, contentWidth);
            int finalHeight = Math.Max(requiredHeight, minHeight);            
           

            Directory.CreateDirectory(Path.GetDirectoryName(outputPath));

            using (var panel = new Panel())
            using (var rtb = new RichTextBox())
            {
                panel.BackColor = bg;
                panel.Width = contentWidth;
                panel.Height = finalHeight;

                // RichTextBox setup
                rtb.Text = text;
                rtb.Font = new System.Drawing.Font(fontName, fontSize, fs, GraphicsUnit.Pixel);
                rtb.ForeColor = fg;
                rtb.BackColor = bg;
                rtb.BorderStyle = BorderStyle.None;
                rtb.ReadOnly = true;
                rtb.ScrollBars = RichTextBoxScrollBars.None;
              
                rtb.Margin = new Padding(0);
                //rtb.DetectUrls = false;
                rtb.TabStop = false;
                rtb.WordWrap = true;
                rtb.Multiline = true;

                // Position and size to match exact content
                rtb.Location = new System.Drawing.Point(0, 0); 
                rtb.Width = contentWidth;
                rtb.Height = finalHeight;
                // IMPORTANT: Set these properties to eliminate blank space
                rtb.SelectionIndent = 0;
                rtb.SelectionRightIndent = 0;
                rtb.SelectionHangingIndent = 0;
                rtb.RightMargin = contentWidth; // Minimal right margin


                // Set line spacing
                //FormatRichTextBox(rtb, 1.5f); // Or your preferred line spacing

                // Force proper text formatting for full width
                rtb.SelectAll();
                rtb.SelectionAlignment = HorizontalAlignment.Left;
                rtb.Select(0, 0); // Deselect

                panel.Controls.Add(rtb);
                using (var tempForm = new Form())
                {
                    tempForm.WindowState = FormWindowState.Minimized;
                    tempForm.ShowInTaskbar = false;
                    tempForm.Controls.Add(panel);
                    tempForm.Show();
                    Application.DoEvents(); // Force layout update
                    tempForm.Hide();

                    using (var bmp = new Bitmap(contentWidth, finalHeight))
                    {
                        panel.DrawToBitmap(bmp, new Rectangle(0, 0, bmp.Width, bmp.Height));
                        bmp.Save(outputPath, System.Drawing.Imaging.ImageFormat.Jpeg);
                    }
                }

            }

            // 6) Postprocessing (slice, etc.)
            SliceImage(outputPath, GlobalProperties.OutputPath, baseNumber);
            imageCounter++;
            return outputPath;
        }

        // Helper method for accurate height calculation
        private int CalculateHeightWithActualRTB(string text, string fontName, float fontSize, FontStyle fs, int width)
        {
            using (var tempForm = new Form())
            using (var rtb = new RichTextBox())
            {
                // Create a temporary form to host the RichTextBox
                tempForm.WindowState = FormWindowState.Minimized;
                tempForm.ShowInTaskbar = false;
                tempForm.Size = new Size(width + 100, 5000);

                rtb.Parent = tempForm;
                rtb.Location = new Point(0, 0);
                rtb.Width = width;
                rtb.Height = 5000;
                rtb.Font = new System.Drawing.Font(fontName, fontSize, fs, GraphicsUnit.Pixel);
                rtb.WordWrap = true;
                rtb.Multiline = true;
                rtb.ScrollBars = RichTextBoxScrollBars.None;
                rtb.ReadOnly = true;
                rtb.Text = text;
                rtb.BorderStyle = BorderStyle.None;
                rtb.Margin = new Padding(0);
                rtb.TabStop = false;
                // Apply same formatting as main method
                rtb.SelectionIndent = 0;
                rtb.SelectionRightIndent = 0;
                rtb.SelectionHangingIndent = 0;
                rtb.RightMargin = width;

                // Apply the same line spacing as your main method
                //FormatRichTextBox(rtb, 1.5f);
                rtb.SelectAll();
                rtb.SelectionAlignment = HorizontalAlignment.Left;
                rtb.Select(0, 0);

                // Force the control to calculate its layout
                tempForm.Show();
                Application.DoEvents();
                tempForm.Refresh();
                rtb.Refresh();
                tempForm.Hide();
                // Get exact height where text ends
                if (string.IsNullOrEmpty(text))
                {
                    return (int)rtb.Font.GetHeight() + 5;
                }

                // Find the actual bottom of the text
                int lastCharIndex = rtb.TextLength - 1;
                Point lastCharPos = rtb.GetPositionFromCharIndex(lastCharIndex);
                int lineHeight = (int)(rtb.Font.GetHeight() * 1.2f); // Account for line spacing

                // The actual height is the Y position of last character + one line height
                int actualHeight = lastCharPos.Y + lineHeight;

                // Add minimal padding - only 5 pixels
                return actualHeight + 5;

                //// Get the actual height needed
                //int calculatedHeight = 0;

                //if (rtb.TextLength > 0)
                //{
                //    // Method 1: Use line count
                //    int lineCount = rtb.GetLineFromCharIndex(rtb.TextLength - 1) + 1;
                //    int lineHeight = (int)rtb.Font.GetHeight();
                //    int heightFromLines = (int)(lineCount * lineHeight * 1.5f); // Account for line spacing

                //    // Method 2: Use last character position
                //    System.Drawing.Point lastPos = rtb.GetPositionFromCharIndex(rtb.TextLength - 1);
                //    int heightFromPosition = lastPos.Y + lineHeight;

                //    // Use the larger of the two methods
                //    calculatedHeight = Math.Max(heightFromLines, heightFromPosition);
                //}
                //else
                //{
                //    calculatedHeight = (int)rtb.Font.GetHeight();
                //}

                //// Add extra padding for safety (important for line spacing)
                //return calculatedHeight + 20;
            }
        }

        public string GenerateImageee(TxtToImage model)
        {
            string text = model.InputText;
            string fontName = Properties.Settings.Default.CapFontName;
            FontStyle fontStyle = (FontStyle)Enum.Parse(typeof(FontStyle), Properties.Settings.Default.CapFontStyle);
            float fontSize = Properties.Settings.Default.CapFontSize;
            string fontColor = Properties.Settings.Default.CapFontColor;
            string bgColor = Properties.Settings.Default.CapBgColor;

            int maxWidth = 240;
            int padding = 5;
            int minHeight = 109;
            int baseNumber = imageCounter * 10000;
            if (imageCounter == 1) baseNumber = 1000;
            string mainImageName = $"{baseNumber}.jpg";
            string directory1 = DirectoryHelper.GetResDirectory();
            string outputPath = Path.Combine(directory1, mainImageName);
            if (File.Exists(outputPath))
                File.Delete(outputPath);

            using (Panel panel = new Panel())
            using (RichTextBox rtb = new RichTextBox())
            {
                rtb.Text = text;
                rtb.Font = new System.Drawing.Font(fontName, fontSize, fontStyle);
                rtb.ForeColor = ColorTranslator.FromHtml(fontColor);
                rtb.BackColor = ColorTranslator.FromHtml(bgColor);
                rtb.BorderStyle = BorderStyle.None;
                rtb.ReadOnly = true;
                rtb.ScrollBars = RichTextBoxScrollBars.None;
                rtb.DetectUrls = false;
                rtb.TabStop = false;
                rtb.WordWrap = true;
                rtb.Location = new System.Drawing.Point(padding, padding);
                rtb.Width = maxWidth-5;

                // Apply justified alignment
                //JustifyRichTextBox(rtb);
                //FormatRichTextBox(rtb, 1);
                // Estimate needed height for the content
                int neededHeight;
                {
                    rtb.Height = 10; // Set very small to force measurement
                    rtb.Height = rtb.GetPositionFromCharIndex(rtb.TextLength - 1).Y + (int)rtb.Font.GetHeight() + padding;
                    neededHeight = Math.Max(rtb.Height + 2 * padding, minHeight);
                }

                // Set panel and rtb height to fit text

                rtb.Height = neededHeight - 2 * padding;

                // Set up panel
                panel.Width = maxWidth;
                panel.Height = neededHeight;
                panel.BackColor = ColorTranslator.FromHtml(bgColor);
                panel.Controls.Add(rtb);

                using (Bitmap bmp = new Bitmap(panel.Width, panel.Height))
                {
                    panel.DrawToBitmap(bmp, new Rectangle(0, 0, bmp.Width, bmp.Height));
                    Directory.CreateDirectory(Path.GetDirectoryName(outputPath));
                    bmp.Save(outputPath, System.Drawing.Imaging.ImageFormat.Jpeg);
                }
            }

            // Continue your pipeline...
            SliceImage(outputPath, GlobalProperties.OutputPath, baseNumber);
            imageCounter++;
            return outputPath;
        }

        public void SliceImage2(string imagePath, string outputDirectory, int baseNumber)
        {
            using (Image image = Image.FromFile(imagePath))
            {
                int chunkHeight = 360;
                int chunkWidth = image.Width; // Always 240 in your pipeline

                int numChunks = (int)Math.Ceiling((double)image.Height / chunkHeight);

                for (int i = 0; i < numChunks; i++)
                {
                    int startY = i * chunkHeight;
                    int currentChunkHeight = Math.Min(chunkHeight, image.Height - startY);

                    if (currentChunkHeight <= 0) continue; // Safety: skip zero-height

                    using (Bitmap chunk = new Bitmap(chunkWidth, currentChunkHeight))
                    {
                        using (Graphics graphics = Graphics.FromImage(chunk))
                        {
                            graphics.CompositingQuality = CompositingQuality.HighQuality;
                            graphics.SmoothingMode = SmoothingMode.HighQuality;
                            graphics.InterpolationMode = InterpolationMode.HighQualityBilinear;
                            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                            graphics.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;

                            graphics.DrawImage(
                                image,
                                new Rectangle(0, 0, chunkWidth, currentChunkHeight), // Target
                                new Rectangle(0, startY, chunkWidth, currentChunkHeight), // Source
                                GraphicsUnit.Pixel);
                        }

                        string chunkPath = Path.Combine(outputDirectory, $"{baseNumber * 10 + (i + 1)}.JPG");
                        chunk.Save(chunkPath, System.Drawing.Imaging.ImageFormat.Jpeg);
                    }
                }
            }
        }

        public void SliceImage(string imagePath, string outputDirectory, int baseNumber)
        {
            // Load the image
            using (Image image = Image.FromFile(imagePath))
            {
                int chunkHeight = 360;
                int chunkWidth = 240;
                //int chunkWidth = image.Width; // Use original width!

                // Calculate the number of chunks needed
                int numChunks = (int)Math.Ceiling((double)image.Height / chunkHeight);

                for (int i = 0; i < numChunks; i++)
                {
                    int currentChunkHeight = (i == numChunks - 1)
                        ? image.Height - (i * chunkHeight)
                        : chunkHeight;

                    // Create fixed-size bitmap (240 width)
                    using (Bitmap chunk = new Bitmap(chunkWidth, currentChunkHeight))
                    {
                        using (Graphics graphics = Graphics.FromImage(chunk))
                        {
                            graphics.CompositingQuality = CompositingQuality.HighQuality;
                            graphics.SmoothingMode = SmoothingMode.HighQuality;
                            graphics.InterpolationMode = InterpolationMode.HighQualityBilinear;
                            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                            graphics.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;

                            // Draw original slice into resized width (240)
                            graphics.DrawImage(
                                image,
                                new Rectangle(0, 0, chunkWidth, currentChunkHeight), // Target
                                new Rectangle(0, i * chunkHeight, image.Width, currentChunkHeight), // Source
                                GraphicsUnit.Pixel);
                        }

                        string chunkPath = Path.Combine(outputDirectory, $"{baseNumber*10 + (i + 1)}.JPG");
                        chunk.Save(chunkPath, System.Drawing.Imaging.ImageFormat.Jpeg);
                        //chunk.Save(chunkPath, ImageFormat.Png);
                    }
                }
            }
        }

    }


}
