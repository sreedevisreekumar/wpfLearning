using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfTreeView
{
    /// <summary>
    /// The view model for the applications main directory view
    /// </summary>
    class DirectoryStructureViewModel:BaseViewModel
    {
        #region Public properties
        /// <summary>
        /// A list of all directories in the machine
        /// </summary>
        public ObservableCollection<DirectoryItemViewModel> Items { get; set; }
        #endregion

        /// <summary>
        /// Default constructor
        /// </summary>
        public DirectoryStructureViewModel()
        {
            //Get all logical drives
            var children = DirectoryStructure.GetLogicalDrives(); 

            //Get view models from the data
            this.Items=new ObservableCollection<DirectoryItemViewModel>(children.Select(drive => new DirectoryItemViewModel(drive.FullPath, DirectoryItemType.Drive)));
        }
    }
}
