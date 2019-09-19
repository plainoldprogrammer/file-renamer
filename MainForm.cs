using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Security;
using System.Runtime.InteropServices;


namespace file_renamer
{
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
            Array.Sort(fileEntries, new NaturalStringComparer());
            

            foreach (string fileName in fileEntries)
            {
                Console.WriteLine(fileName);
            }

            Console.WriteLine(fileEntries.Length);
        }
    }
}
