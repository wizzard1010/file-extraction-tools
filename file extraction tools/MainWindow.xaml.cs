using System;
using System.IO;
using System.IO.Compression;
using System.Windows;
using Microsoft.Win32;
using System.Windows.Forms;
using System.Windows.Controls;

namespace file_extraction
{
    public partial class MainWindow : Window
    {
        private System.Windows.Controls.TextBox txtExtractFilePathTextBox;
        private System.Windows.Controls.TextBox txtZipFilePathTextBox;

        public MainWindow()
        {
            InitializeComponent();
        }

        //Event handler for the "Find" button to locate the zip file
        private void btnFind_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "ZIP FILES (*.zip)|*.zip|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                txtZipFilePath.Text = openFileDialog.FileName;
            }
        }
        //Event handle for the "Extract" handler
        private void btnExtract_Click(object sender, RoutedEventArgs e)
        {
            string zipFilePath = txtZipFilePath.Text;
            string extractPath = txtExtractFilePath.Text;

            if (string.IsNullOrEmpty(zipFilePath) || !File.Exists(zipFilePath))
            {
                System.Windows.MessageBox.Show("Please enter a valid ZIP file path or use the 'Find' button to locate the file.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                //Create the extraction directory if it doesn't exist
                Directory.CreateDirectory(extractPath);

                //Extract the ZIP file
                ZipFile.ExtractToDirectory(zipFilePath, extractPath);

                System.Windows.MessageBox.Show("Extraction completed successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Extraction failed: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        //Event handler for the "Browse" button to select the extraction path.
        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            var folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = folderBrowserDialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                txtExtractFilePath.Text = folderBrowserDialog.SelectedPath;
            }
        }

        // Event handler for the "Cancel" button.
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            // Close the application.
            this.Close();
        }
    }
}