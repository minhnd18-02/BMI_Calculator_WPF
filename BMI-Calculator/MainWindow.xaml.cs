using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WinForms = System.Windows.Forms;

namespace BMI_Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnCalculate_Click(object sender, RoutedEventArgs e)
        {
            float height = float.Parse(txtHeight.Text);
            float weight = float.Parse(txtWeight.Text);
            if (height > 0 && weight > 0) 
            {
                float mheight = height / 100;
                float bmi = weight / (mheight * mheight);
                txtBMI.Text = MathF.Round(bmi, 1).ToString();
                if (bmi < 18.5)
                {
                    txtStatus.Text = "Thieu can";
                    txtBMI.Background = Brushes.Blue;
                }
                else if (18.5 < bmi & bmi < 24.9)
                {
                    txtStatus.Text = "Can doi";
                    txtBMI.Background = Brushes.Green;
                }
                else if (25 < bmi & bmi < 29.9)
                {
                    txtStatus.Text = "Thua can";
                    txtBMI.Background = Brushes.Orange;
                }
                else if (30 < bmi & bmi < 34.9)
                {
                    txtStatus.Text = "Beo phi";
                    txtBMI.Background= Brushes.Red;
                }
                else
                {
                    txtStatus.Text = "Beo phi nguy hiem";
                    txtStatus.Background = Brushes.Purple;
                }
            } else
            {
                MessageBox.Show("Vui long nhap so duong");
            }
        }

        public void Clear()
        {
            txtHeight.Text = "";
            txtWeight.Text = "";
            txtStatus.Text = "";
            txtBMI.Text = "";
            txtBMI.Background = Brushes.Transparent;
        }
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            Clear();
        }

        private void btnBrowser_Click(object sender, RoutedEventArgs e)
        {
            WinForms.FolderBrowserDialog dialog = new WinForms.FolderBrowserDialog();
            dialog.SelectedPath = @"";
            WinForms.DialogResult result = dialog.ShowDialog();

            if(result == WinForms.DialogResult.OK)
            {
                string sPath = dialog.SelectedPath;
                string[] entries = Directory.GetFileSystemEntries(sPath);
                var fileInfos = entries.Select(entry => new
                {
                    Type = GetFileType(entry),
                    Name = Path.GetFileName(entry),
                    Path = entry,
                    Icon = GetFileIcon(entry)
                });
                lvFolder.ItemsSource = fileInfos;
            }
        }
        private string GetFileType(string path)
        {
            return File.GetAttributes(path).HasFlag(FileAttributes.Directory) ? "Folder" : Path.GetExtension(path);
        }
        private BitmapImage GetFileIcon(string path)
        {
            // Determine whether the entry is a folder or file
            bool isDirectory = File.GetAttributes(path).HasFlag(FileAttributes.Directory);

            // Load the appropriate icon based on the type
            string iconPath = isDirectory
                ? @"D:\Documents\Repositories\FileManager\Icon\folder.png"
                : @"D:\Documents\Repositories\FileManager\Icon\docs.png";

            BitmapImage icon = new BitmapImage(new Uri(iconPath));
            return icon;
        }
    }
}
