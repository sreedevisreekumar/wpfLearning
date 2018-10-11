using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace WpfTreeView
{
    /// <summary>
    /// A helper class to query Information about directories
    /// </summary>
   public static class DirectoryStructure
    {
        /// <summary>
        /// Get logical drives in the system
        /// </summary>
        /// <returns></returns>
        public static List<DirectoryItem> GetLogicalDrives()
        {
            //Get every logical drive in the machine
          return  System.IO.Directory.GetLogicalDrives().Select(drive => new DirectoryItem { FullPath = drive, Type = DirectoryItemType.Drive }).ToList();
            

        }

        /// <summary>
        /// Get file or folder
        /// </summary>
        /// <param name="fullpath"></param>
        /// <returns></returns>
        public static List<DirectoryItem>GetDirectoryContents(string fullPath)
        {
            //Create empty list
            var items = new List<DirectoryItem>();

            #region Get folders 
            
            //Try and get directories from the folder ignoring any issues doing so
            try
            {
                var dirs = System.IO.Directory.GetDirectories(fullPath);
                if (dirs.Length > 0)
                    items.AddRange(dirs.Select(dir => new DirectoryItem { FullPath = dir, Type = DirectoryItemType.Folder }));
            }
            catch (Exception ex)
            {

            }
            #endregion


            #region files

            //Try and get files from the folder ignoring any issues doing so
            try
            {
                var fs =System.IO.Directory.GetFiles(fullPath);
                if (fs.Length > 0)
                    items.AddRange(fs.Select(file=>new DirectoryItem {FullPath=file,Type=DirectoryItemType.File}));
            }
            catch (Exception ex)
            {

            }

            return items;
            #endregion
        }

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
