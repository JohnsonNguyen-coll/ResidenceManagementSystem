using Assignment_PRN.Models;
using Microsoft.Win32;
using System;
using System.Windows;

namespace Assignment_PRN
{
    public partial class UploadDocumentsWindow : Window
    {
        public string FilePath { get; private set; } = "";
        private int _userId;
        public UploadDocumentsWindow(int userId)
        {
            InitializeComponent();
            _userId = userId;
        }

        private void SelectFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Select Document",
                Filter = "All Files|*.*"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                FilePath = openFileDialog.FileName;
                txtFileName.Text = FilePath;
            }
        }

        private void SaveFilePath_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(FilePath))
            {
                MessageBox.Show("Please select a file before saving.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            DialogResult = true;
            Close();
        }

        private void CloseWindow_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
