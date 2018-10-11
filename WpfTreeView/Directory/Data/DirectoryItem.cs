using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfTreeView
{
    /// <summary>
    /// Information about a directory item such as a drive,file or folder
    /// </summary>
  public class DirectoryItem
    {
        /// <summary>
        /// The type drive,file,folder
        /// </summary>
        public DirectoryItemType Type { get; set; }
        /// <summary>
        /// The absolute path
        /// </summary>
        public string FullPath { get; set; }
        /// <summary>
        /// The name of this directory item
        /// </summary>
        public string Name
        {
            get
            {
                return this.Type ==DirectoryItemType.Drive?this.FullPath: DirectoryStructure.GetFileFolderName(this.FullPath);
            }
        }
    }
}
