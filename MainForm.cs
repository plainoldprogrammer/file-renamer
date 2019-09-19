using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Security;
using System.Runtime.InteropServices;


namespace file_renamer
{
    public partial class MainForm : Form
    {
        FolderBrowserDialog folderBrowser;

        public MainForm()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            folderBrowser = new FolderBrowserDialog();
            
            if (folderBrowser.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBoxFolderPath.Text = folderBrowser.SelectedPath;
            }
            else
            {
                MessageBox.Show("Please select a valid folder");
            }
        }

        private void ButtonRename_Click(object sender, EventArgs e)
        {
            ProcessDirectory(folderBrowser.SelectedPath);
        }

        public static void ProcessDirectory(string path)
        {
            string[] fileEntries = Directory.GetFiles(path);
            Array.Sort(fileEntries, new NaturalStringComparer());
            int count = 0;

            foreach (string fileName in fileEntries)
            {
                string fileLocation = Path.GetDirectoryName(fileName);
                string fileExtension = Path.GetExtension(fileName);

                if (fileExtension.Equals(".ini"))
                {
                    Console.WriteLine(".ini file untouched");
                }
                else if (fileExtension.Equals(".png"))
                {
                    count++;
                    string newFileName = fileLocation + "\\" + count + fileExtension;
                    System.IO.File.Move(fileName, newFileName);
                }
            }
        }
    }

    [SuppressUnmanagedCodeSecurity]
    internal static class SafeNativeMethods
    {
        [DllImport("shlwapi.dll", CharSet = CharSet.Unicode)]
        public static extern int StrCmpLogicalW(string psz1, string psz2);
    }

    public sealed class NaturalStringComparer : IComparer<string>
    {
        public int Compare(string a, string b)
        {
            return SafeNativeMethods.StrCmpLogicalW(a, b);
        }
    }

    public sealed class NaturalFileInfoNameComparer : IComparer<FileInfo>
    {
        public int Compare(FileInfo a, FileInfo b)
        {
            return SafeNativeMethods.StrCmpLogicalW(a.Name, b.Name);
        }
    }
}
