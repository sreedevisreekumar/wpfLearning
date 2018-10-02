using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Path = System.IO.Path;

namespace WpfTreeView
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }
        #endregion

        #region OnLoaded
        /// <summary>
        /// When the application loads
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Get every logical drive in the machine
            foreach(var drive in Directory.GetLogicalDrives())
            {
                //create a new item for each drive
                var item = new TreeViewItem()
                {
                    //set header
                    Header = drive,
                    //set fullpath
                    Tag = drive
                };

                //Add a dummy item
                item.Items.Add(null);

                //Listen out for item being expanded
                item.Expanded += Folder_Expanded;

                //Add to treeview
                FolderView.Items.Add(item);
            }
        }
        #endregion

        #region Folder_Expanded
        /// <summary>
        /// Fires when folder gets expanded
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Folder_Expanded(object sender, RoutedEventArgs e)
        {
            #region initial check
            var item = (TreeViewItem)sender;

            //if the item only contains the dummy data
            if (item.Items.Count != 1 || item.Items[0] != null)
                return;

            //clear dummy data
            item.Items.Clear();

            //Get full path
            var fullPath = (string)item.Tag;
            #endregion

            #region Get folders 
            //Create a blank list for directories
            var directories = new List<string>();
            //Try and get directories from the folder ignoring any issues doing so
            try
            {
                var dirs = Directory.GetDirectories(fullPath);
                if (dirs.Length > 0)
                    directories.AddRange(dirs);               
            }
            catch(Exception ex)
            {

            }

            //For each directory---
            directories.ForEach(directorypath =>
            {
                //Create directory item
                var subItem = new TreeViewItem()
                {
                    //set header as a folder name
                    Header = GetFileFolderName(directorypath),
                    //tag as a full path
                    Tag = directorypath
                };
                //Add dummy item so we can expand folder
                subItem.Items.Add(null);

                //Handle expanding recursion
                subItem.Expanded += Folder_Expanded;

                //Add this item to the parent
                item.Items.Add(subItem);
            });
            #endregion

            #region files
            //Create a blank list for directories
            var files = new List<string>();
            //Try and get files from the folder ignoring any issues doing so
            try
            {
                var fs = Directory.GetFiles(fullPath);
                if (fs.Length > 0)
                    files.AddRange(fs);
            }
            catch (Exception ex)
            {

            }

            //For each file---
            files.ForEach(filepath =>
            {
                //Create file item
                var subItem = new TreeViewItem()
                {
                    //set header as a file name
                    Header = GetFileFolderName(filepath),
                    //tag as a full path
                    Tag =filepath
                };
               

                //Add this item to the parent
                item.Items.Add(subItem);
            });
            #endregion
        }
        #endregion

        #region Helpers
        /// <summary>
        /// Find the file or folder name from a full path
        /// </summary>
        /// <param name="path"> The full path</param>
        /// <returns></returns>
        public static string GetFileFolderName(string path)
        {
            //C:\Something\a folder
            //C:\Something\a file
            //a file

            //If we have no path,return empty
            if (string.IsNullOrEmpty(path))
                return string.Empty;


            //Make all slashes back slashes
            var normalizedPath = path.Replace('/', '\\');

            //Find the last backslash in the path
            var lastIndex = normalizedPath.LastIndexOf('\\');

            //If we dont find a backslash return the path itself.
            if (lastIndex <= 0)
                return path;

            return path.Substring(lastIndex + 1);
        }
        #endregion





    }
}
