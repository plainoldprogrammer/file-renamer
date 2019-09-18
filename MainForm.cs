using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace file_renamer
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
            
            if (folderBrowser.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBoxFolderPath.Text = folderBrowser.SelectedPath;
                ProcessDirectory(folderBrowser.SelectedPath);
            }
            else
            {
                MessageBox.Show("Please select a valid folder");
            }
        }

        public static void ProcessDirectory(string path)
        {
            string[] fileEntries = Directory.GetFiles(path);

            foreach (string fileName in fileEntries)
            {
                Console.WriteLine(fileName);
            }
        }
    }
}
