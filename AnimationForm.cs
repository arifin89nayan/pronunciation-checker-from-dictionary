using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Windows.Forms;
using WindowsFormsApp1.Global;
using WindowsFormsApp1.Helper;
using WindowsFormsApp1.Models.Animation;
using WindowsFormsApp1.Models.Widgets;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Image = System.Drawing.Image;

namespace WindowsFormsApp1
{
    public partial class AnimationForm : Form
    {
        public string ReceivedValue { get; set; }
        private readonly string _filepath;
        private readonly string _outputDirectory;
        private int yOffset = 2;
        private Image selectedImage;
        private int selectedPanelIndex = -1;
        private readonly string _tempDirectory;
        private FontDialog _fontDlg;
        public FontDialog _AnifontDlg;
        private List<string> images = new List<string>();
        private int totalImagesProcessed = 0;
        private int timeInterval = 0;
        private StartupForm _form1;
        public Font AniFont;
        public Color Anicolor;
        public const string ContentNameFile = "CONTENTNAME.TXT";

        private Widget widget;
        private Line line;

        public AnimationForm(string filepath, string inputTitle)
        {
            InitializeComponent();
            btnNext.Enabled = false;
            _filepath = filepath;
            _outputDirectory = DirectoryHelper.GetOutputDirectory(inputTitle);
            _tempDirectory = DirectoryHelper.GetTempDirectory();
            CreateContentName(_outputDirectory, inputTitle);
            string sourceDirectory = Path.GetDirectoryName(_filepath);
            string destinationDirectory = _outputDirectory;

            // List of files to copy
            string[] fileNamesToCopy = { "TEXTDATA.txt", "CONTENTINFO.CSV" };

            CopyFilesByName(sourceDirectory, destinationDirectory, fileNamesToCopy);

            View();
        }

        public AnimationForm(Widget widget, Line line)
        {
            InitializeComponent();
            btnNext.Enabled = false;
            this.widget = widget;
            this.line = line;

            _outputDirectory = DirectoryHelper.GetOutputDirectory(GlobalProperties.OutputFolderName);

            View_V2(); 
        }

   

        static void CopyFilesByName(string sourceDir, string destDir, string[] fileNames)
        {
            foreach (string fileName in fileNames)
            {
                // Create the full file path for source and destination
                string sourceFilePath = Path.Combine(sourceDir, fileName);
                string destFilePath = Path.Combine(destDir, fileName);

                try
                {
                    // Check if the source file exists
                    if (File.Exists(sourceFilePath))
                    {
                        // Copy the file
                        File.Copy(sourceFilePath, destFilePath, true);
                        Console.WriteLine($"Copied {fileName} to {destDir}");
                    }
                    else
                    {
                        Console.WriteLine($"File {fileName} does not exist in the source directory.");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"An error occurred while copying {fileName}: {e.Message}");
                }
            }
        }
            private void CreateContentName(String Directory,string Title)
        {
            string generateDir = Directory;
            // Assuming 'textbox' is the name of your TextBox control
            string textboxValue = Title;

            //// Create the output directory if it doesn't exist
            //if (!Directory.Exists(generateDir))
            //{
            //    Directory.CreateDirectory(generateDir);
            //}

            // Define the full path to the content name file
            string filePath = Path.Combine(generateDir, ContentNameFile);

            // Convert the textbox value to a byte array
            byte[] tempbytearr = System.Text.UnicodeEncoding.Unicode.GetBytes(textboxValue.ToCharArray());

            // Write the byte array to the file
            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                fs.Write(tempbytearr, 0, tempbytearr.Length);
            }
        }

        private void View(int selectedImageFrameNum = 0, string switchTime = null, bool reset = false)
        {
            DirectoryHelper.ClearDirectory(_tempDirectory);

            if (switchTime == null)
            {
                var imageData = ImageExtrator.GetImageNames(_filepath);
                images = imageData.imageNames;
                totalImagesProcessed = imageData.totalImagesProcessed;
                timeInterval = imageData.timeInterval;
                
            }
            if (reset)
            {
                images.Clear();
            }

            //string folderPath = Path.GetDirectoryName(_filepath);
            string folderPath = GlobalProperties.PlayConfigPath;


            AddEmptyImages(images, selectedImageFrameNum);
            RemoveImages(images, selectedImageFrameNum);
            int panelIndex = 0;
            foreach (var imageName in images)
            {
               
                string imagePath = Path.Combine(folderPath, imageName);

                var photoPanel = new System.Windows.Forms.Panel();
                photoPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                photoPanel.Location = new System.Drawing.Point(3, 3);
                photoPanel.Name = "panel2";
                photoPanel.Size = new System.Drawing.Size(163, 168);
                photoPanel.TabIndex = 0;

                photoPanel.Tag = panelIndex;
                panelIndex++;

                // Set the PictureBox to fill the panel
                PictureBox pictureBox = new PictureBox
                {
                    Image = Image.FromFile(imagePath),
                    SizeMode = PictureBoxSizeMode.Zoom,
                    Dock = DockStyle.Fill  // Make PictureBox fill the parent panel
                };

                pictureBox.Click += new EventHandler((sender, e) =>
                {
                    selectedPanelIndex = (int)((PictureBox)sender).Parent.Tag;
                    selectedImage = pictureBox.Image;
                    CreateNewPictureBoxInstance(pictureBox.Image);
                });
                // Attach the double-click event handler
                pictureBox.DoubleClick += new EventHandler(PictureBox_DoubleClick);

                photoPanel.Controls.Add(pictureBox);

                flowLayoutPanel1.Controls.Add(photoPanel);
            }
            //if (selectedImageFrameNum == 0)
            //{
            //    comboBox1.SelectedIndexChanged -= new System.EventHandler(comboBox1_SelectedIndexChanged);
            //    comboBox1.SelectedIndex = totalImagesProcessed - 2;
            //    comboBox1.SelectedIndexChanged += new System.EventHandler(comboBox1_SelectedIndexChanged);
            //}


            if (selectedImageFrameNum == 0)
            {
                comboBox1.SelectedIndexChanged -= new System.EventHandler(comboBox1_SelectedIndexChanged);
                comboBox1.SelectedIndex = totalImagesProcessed - 2;
                comboBox1.SelectedIndexChanged += new System.EventHandler(comboBox1_SelectedIndexChanged);
            }
            else if (reset)
            {
                comboBox1.SelectedIndexChanged -= new System.EventHandler(comboBox1_SelectedIndexChanged);
                comboBox1.SelectedIndex = 0;
                comboBox1.SelectedIndexChanged += new System.EventHandler(comboBox1_SelectedIndexChanged);
            }
            txtBoxSwitchingTime.Text = switchTime == null ? timeInterval.ToString() : switchTime;
        }

        private void View_V2(int selectedImageFrameNum = 0, string switchTime = null, bool reset = false)
        { 
            if (switchTime == null)
            {
                var lineData = ImageExtrator.ExtractAnimationLine(line.Text);              
                images = lineData.Images;
                totalImagesProcessed = lineData.ImageCount;
                timeInterval = lineData.TimeInterval;

            }
            if (reset)
            {
                images.Clear();
            } 

            string folderPath = GlobalProperties.PlayConfigFolderPath;


            AddEmptyImages(images, selectedImageFrameNum);
            RemoveImages(images, selectedImageFrameNum);
            int panelIndex = 0;
            foreach (var imageName in images)
            {

                string imagePath = Path.Combine(folderPath, imageName);

                var photoPanel = new Panel();
                photoPanel.BorderStyle = BorderStyle.FixedSingle;
                photoPanel.Location = new Point(3, 3);
                photoPanel.Name = "panel2";
                photoPanel.Size = new Size(163, 168);
                photoPanel.TabIndex = 0;

                photoPanel.Tag = panelIndex;
                panelIndex++;

                // Set the PictureBox to fill the panel
                PictureBox pictureBox = new PictureBox
                {
                    Image = Image.FromFile(imagePath),
                    SizeMode = PictureBoxSizeMode.Zoom,
                    Dock = DockStyle.Fill  // Make PictureBox fill the parent panel
                };

                pictureBox.Click += new EventHandler((sender, e) =>
                {
                    selectedPanelIndex = (int)((PictureBox)sender).Parent.Tag;
                    selectedImage = pictureBox.Image;
                    CreateNewPictureBoxInstance(pictureBox.Image);
                });
                // Attach the double-click event handler
                pictureBox.DoubleClick += new EventHandler(PictureBox_DoubleClick);

                photoPanel.Controls.Add(pictureBox);

                flowLayoutPanel1.Controls.Add(photoPanel);
            }
            //if (selectedImageFrameNum == 0)
            //{
            //    comboBox1.SelectedIndexChanged -= new System.EventHandler(comboBox1_SelectedIndexChanged);
            //    comboBox1.SelectedIndex = totalImagesProcessed - 2;
            //    comboBox1.SelectedIndexChanged += new System.EventHandler(comboBox1_SelectedIndexChanged);
            //}


            if (selectedImageFrameNum == 0)
            {
                comboBox1.SelectedIndexChanged -= new System.EventHandler(comboBox1_SelectedIndexChanged);
                comboBox1.SelectedIndex = totalImagesProcessed - 2;
                comboBox1.SelectedIndexChanged += new System.EventHandler(comboBox1_SelectedIndexChanged);
            }
            else if (reset)
            {
                comboBox1.SelectedIndexChanged -= new System.EventHandler(comboBox1_SelectedIndexChanged);
                comboBox1.SelectedIndex = 0;
                comboBox1.SelectedIndexChanged += new System.EventHandler(comboBox1_SelectedIndexChanged);
            }
            txtBoxSwitchingTime.Text = switchTime == null ? timeInterval.ToString() : switchTime;
        }

        private void PictureBox_DoubleClick(object sender, EventArgs e)
        {
            // Cast the sender to PictureBox
            PictureBox pictureBox = sender as PictureBox;

            if (pictureBox != null)
            {
                // Create and configure the OpenFileDialog
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg|All Files|*.*";
                openFileDialog.Title = "Select a JPG Image";

                // Show the dialog and check if the user selected a file
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Get the selected file path
                    string selectedFilePath = openFileDialog.FileName;

                    // Load the new image from the file
                    Image newImage = Image.FromFile(selectedFilePath);
                    try
                    {
                        // Check the image size
                        if (newImage.Width == 240 && newImage.Height == 320)
                        {
                            pictureBox.Image = newImage;

                        }
                        else
                        {
                            MessageBox.Show("Image size does not match. The width must be 240 pixels  and the height must be 320 pixels.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        // Update the PictureBox with the new image
                        //pictureBox.Image = newImage;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"An error occurred while loading the image: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }


        private void CreateNewPictureBoxInstance(Image image)
        {
            
            PictureBox newPictureBox = new PictureBox();
            newPictureBox.Image = new Bitmap(image); 
            newPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            newPictureBox.Size = panel1.ClientSize; 
            newPictureBox.Location = new Point(0, 0);
            this.panel1.Controls.Clear(); 
            this.panel1.Controls.Add(newPictureBox);
        }

        private void AddImagesToPanel(Panel panel, List<string> imageFiles)
        {
            panel.Controls.Clear();

            int x = 10; // Initial x-coordinate
            int y = 10; // Initial y-coordinate
            int margin = 10; // Space between images

            foreach (string imageFile in imageFiles)
            {
                PictureBox pictureBox = new PictureBox
                {
                    Image = Image.FromFile(imageFile),
                    SizeMode = PictureBoxSizeMode.Zoom,
                    Size = new Size(240, 360), // Set size of the picture box
                    Location = new Point(x, y)
                };

                // Add the PictureBox to the Panel
                panel.Controls.Add(pictureBox);

                // Update x-coordinate for the next image
                x += pictureBox.Width + margin;

                // Move to the next row if x-coordinate exceeds Panel width
                if (x + pictureBox.Width > panel.Width)
                {
                    x = 10;
                    y += pictureBox.Height + margin;
                }
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            FontDialog fontDlg = new FontDialog();
            fontDlg.ShowColor = true;

            if (fontDlg.ShowDialog() == DialogResult.OK)
            {
                _fontDlg = fontDlg;

                DrawTextOnImage();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            StartupForm firstForm = new StartupForm();
            this.Hide();
            firstForm.ShowDialog();
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Clear();

            int selectedImageFrameNum = comboBox1.SelectedIndex + 2;
            View_V2(selectedImageFrameNum, txtBoxSwitchingTime.Text);
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            label2.Text = richTextBox1.Text;
        }
       

        private void btnGenerate_Click_V2(object sender, EventArgs e)
        {

         var items =   PatternHelper.GetItems(line.Text, @"<([^>]+)>");

            var random = new Random().Next(100,1000);
            var imageData = GetImagesFromPanel();
            string commaSeperatedImageNames = string.Empty;

            for (int i = 0; i < imageData.Count; i++)
            {
                var imageWithTitle = DrawTextOnImage(imageData[i]);
                string outputImageName = Path.Combine(GlobalProperties.OutputPath, $"{random + i}.JPG");

                commaSeperatedImageNames += $"{random + i}.JPG";

                if (i < imageData.Count - 1)
                {
                    commaSeperatedImageNames += ";";
                }

                try
                {
                    imageWithTitle.Save(outputImageName, System.Drawing.Imaging.ImageFormat.Jpeg);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error saving image: {ex.Message}");
                }
            }

            btnNext.Enabled = true;

            items[1] = $"<{commaSeperatedImageNames}>";
            items[6] = $"<{imageData.Count}>";
            items[7] = $"<{txtBoxSwitchingTime.Text}>";
            
            GlobalConfigPropreties.Instance.AddLine(string.Join("", items));

            MessageBox.Show("Files generated successfully.");
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            //Random random = new Random();
            int ImageNumber = 1000;
            var imageData = GetImagesFromPanel();
            string commaSeperatedImageNames = string.Empty;

            for (int i = 0; i < imageData.Count; i++)
            {
                var imageWithTitle = DrawTextOnImage(imageData[i]);
                //string outputImageName = Path.Combine(_outputDirectory, $"200{i}.JPG");
                string outputImageName = Path.Combine(GlobalProperties.OutputPath, $"{(ImageNumber) + i}.JPG");

                commaSeperatedImageNames += $"{(ImageNumber) + i}.JPG";

                if (i < imageData.Count - 1)
                {
                    commaSeperatedImageNames += ";";
                }

                try
                {
                    imageWithTitle.Save(outputImageName, System.Drawing.Imaging.ImageFormat.Jpeg);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error saving image: {ex.Message}");
                }
            }

            ConfigBuilder builder = new ConfigBuilder(_outputDirectory);
            builder.AddAnimationImages(commaSeperatedImageNames)
                   .AddAnimationImageNumber(comboBox1.Text)
                   .AddAnimationTime(txtBoxSwitchingTime.Text)
                   .Build();
            var dialogResult = MessageBox.Show("Files generated sucessfully.");

            if (dialogResult == DialogResult.OK)
            {
                DirectoryHelper.openDirectory(_outputDirectory);
            }
            btnNext.Enabled = true;

        }
        private void DeleteExistingImages(string directory)
        {
            try
            {
                var images = Directory.GetFiles(directory, "*.JPG");
                foreach (var image in images)
                {
                    File.Delete(image);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting existing images: {ex.Message}");
            }
        }
        private List<Image> GetImagesFromPanel()
        {
            var images = new List<Image>();

            foreach (Control c in flowLayoutPanel1.Controls)
            {
                if (c is PictureBox pictureBox && pictureBox.Image != null)
                {
                    images.Add(pictureBox.Image);
                }
                else
                {
                    foreach (PictureBox childControl in c.Controls)
                    {
                        if (childControl is PictureBox cPictureBox && cPictureBox.Image != null)
                        {
                            images.Add(childControl.Image);
                        }
                    }
                }
            }

            return images;
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            yOffset -= 10;
            
            DrawTextOnImage();
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            yOffset += 10;
            DrawTextOnImage();
        }

        private Bitmap DrawTextOnImage()
        {
            if (selectedImage == null)
            {
                MessageBox.Show("Please Select Animation image for Title Preview!");
                return null;
            }
            Bitmap imageWithText = new Bitmap(selectedImage); 

            using (Graphics graphics = Graphics.FromImage(imageWithText))
            {
                string text = richTextBox1.Text;

                string fontName = (Properties.Settings.Default.AniFontName).ToString();
                FontStyle fontStyle = (FontStyle)Enum.Parse(typeof(FontStyle), Properties.Settings.Default.AniFontStyle.ToString());
                float fontSize = Properties.Settings.Default.AniFontSize;
                string fontColor = (Properties.Settings.Default.AniFontColor).ToString();

                Font savedFont = new Font(fontName, fontSize, fontStyle);
                Color savedColor = ColorTranslator.FromHtml(fontColor);
                Font font = savedFont is null ? new Font("Arial", 12, FontStyle.Regular) : savedFont;
                Color color = savedColor;
                SolidBrush brush = new SolidBrush(color);
                float maxWidth = imageWithText.Width - 20;
                float lineHeight = font.GetHeight(graphics);
                StringFormat format = new StringFormat();
                format.Alignment = StringAlignment.Center; 
                string[] words = text.Split(' ');
                string line = string.Empty;
                float totalHeight = 0;
                foreach (string word in words)
                {
                    string testLine = line + (line.Length > 0 ? " " : string.Empty) + word;
                    SizeF size = graphics.MeasureString(testLine, font);

                    if (size.Width > maxWidth)
                    {
                        // Accumulate height of the current line
                        totalHeight += lineHeight;
                        line = word;
                    }
                    else
                    {
                        // Continue adding words to the current line
                        line = testLine;
                    }
                }

                // Add height of the last line
                totalHeight += lineHeight;

                // Calculate the starting point to center the text block vertically with offset
                PointF startPoint = new PointF(imageWithText.Width / 2, (imageWithText.Height - totalHeight) / 2 + yOffset);

                // Draw each line of the text
                line = string.Empty;
                float y = startPoint.Y;
                foreach (string word in words)
                {
                    string testLine = line + (line.Length > 0 ? " " : string.Empty) + word;
                    SizeF size = graphics.MeasureString(testLine, font);

                    if (size.Width > maxWidth)
                    {
                        // Draw the current line and start a new one
                        graphics.DrawString(line, font, brush, new PointF(startPoint.X, y), format);
                        y += lineHeight;
                        line = word;
                    }
                    else
                    {
                        // Continue adding words to the current line
                        line = testLine;
                    }
                }

                // Draw the remaining text
                if (line.Length > 0)
                {
                    graphics.DrawString(line, font, brush, new PointF(startPoint.X, y), format);
                }
            }

            CreateNewPictureBoxInstance(imageWithText);

            return imageWithText;
        }

        private Bitmap DrawTextOnImage(Image inputImage)
        {
            Bitmap imageWithText = new Bitmap(inputImage);

            using (Graphics graphics = Graphics.FromImage(imageWithText))
            {
                string text = richTextBox1.Text;

                string fontName = (Properties.Settings.Default.AniFontName).ToString();
                FontStyle fontStyle = (FontStyle)Enum.Parse(typeof(FontStyle), Properties.Settings.Default.AniFontStyle.ToString());
                float fontSize = Properties.Settings.Default.AniFontSize;
                string fontColor = (Properties.Settings.Default.AniFontColor).ToString();

                Font savedFont = new Font(fontName, fontSize, fontStyle);
                Color savedColor = ColorTranslator.FromHtml(fontColor);

                //Font font = _fontDlg is null ? new Font("Arial", 12, FontStyle.Regular) : _fontDlg.Font;
                // Color color = _fontDlg is null ? Color.Black : _fontDlg.Color;
                Font font = savedFont is null ? new Font("Arial", 12, FontStyle.Regular) : savedFont;
                Color color = savedColor;
                SolidBrush brush = new SolidBrush(color);
                float maxWidth = imageWithText.Width - 20;
                float lineHeight = font.GetHeight(graphics);
                StringFormat format = new StringFormat();
                format.Alignment = StringAlignment.Center;  // Center align text horizontally

                // Measure the size of the text to determine wrapping
                string[] words = text.Split(' ');
                string line = string.Empty;
                float totalHeight = 0;
                foreach (string word in words)
                {
                    string testLine = line + (line.Length > 0 ? " " : string.Empty) + word;
                    SizeF size = graphics.MeasureString(testLine, font);

                    if (size.Width > maxWidth)
                    {
                        // Accumulate height of the current line
                        totalHeight += lineHeight;
                        line = word;
                    }
                    else
                    {
                        // Continue adding words to the current line
                        line = testLine;
                    }
                }

                // Add height of the last line
                totalHeight += lineHeight;

                // Calculate the starting point to center the text block vertically with offset
                PointF startPoint = new PointF(imageWithText.Width / 2, (imageWithText.Height - totalHeight) / 2 + yOffset);

                // Draw each line of the text
                line = string.Empty;
                float y = startPoint.Y;
                foreach (string word in words)
                {
                    string testLine = line + (line.Length > 0 ? " " : string.Empty) + word;
                    SizeF size = graphics.MeasureString(testLine, font);

                    if (size.Width > maxWidth)
                    {
                        // Draw the current line and start a new one
                        graphics.DrawString(line, font, brush, new PointF(startPoint.X, y), format);
                        y += lineHeight;
                        line = word;
                    }
                    else
                    {
                        // Continue adding words to the current line
                        line = testLine;
                    }
                }

                // Draw the remaining text
                if (line.Length > 0)
                {
                    graphics.DrawString(line, font, brush, new PointF(startPoint.X, y), format);
                }
            }

            return imageWithText;
        }
        private string GetFolderPath()
        {
            using (var folderDialog = new FolderBrowserDialog())
            {
                folderDialog.Description = "Select folder to save image";
                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    return folderDialog.SelectedPath;
                }
            }
            return null; // Return null if user cancels the dialog
        }

        private void button2_Click(object sender, EventArgs e)
        {
            btnNext.Enabled = false;
            flowLayoutPanel1.Controls.Clear();
            panel1.Controls.Clear();

            View(2, txtBoxSwitchingTime.Text, true);

            // AddEmptyImages();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            btnNext.Enabled = false;
            flowLayoutPanel1.Controls.Clear();
            //AddImagesToPanel();
            View();
        }

        private void AddEmptyImages(List<string> imageNames, int selectedImageFrameNum)
        {
            if (selectedImageFrameNum != 0 && imageNames.Count < selectedImageFrameNum)
            {
                int numOfImageToAdd = selectedImageFrameNum - imageNames.Count;

                for (int i = 0; i < numOfImageToAdd; i++)
                {
                    imageNames.Add("empty.jpg");
                }
            }
        }

        private void RemoveImages(List<string> imageNames, int selectedImageFrameNum)
        {
            if (imageNames.Count > selectedImageFrameNum && selectedImageFrameNum != 0)
            {
                int removeItemStartIndex = selectedImageFrameNum;
                int removeItemEndIndex = imageNames.Count - selectedImageFrameNum;
                imageNames.RemoveRange(removeItemStartIndex, removeItemEndIndex);

                //Remove All images
                //imageNames.Clear();
                //for (int i = 0; i < selectedImageFrameNum; i++)
                //{
                //    imageNames.Add("empty.jpg");
                //}
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Form3 thirdForm = new Form3();
            //this.Hide();
            //thirdForm.ShowDialog();
            this.Close();
        } 
    }
}
