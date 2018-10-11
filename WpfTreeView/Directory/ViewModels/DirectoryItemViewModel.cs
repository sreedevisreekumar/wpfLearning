using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;


namespace WpfTreeView
{
    /// <summary>
    /// A view model for each directory item
    /// </summary>
    public class DirectoryItemViewModel:BaseViewModel
    {
        #region PublicProperties
        /// <summary>
        /// The type drive,file,folder
        /// </summary>
        public DirectoryItemType Type { get; set; }
        /// <summary>
        /// The image name as per type
        /// </summary>
        public string ImageName => Type == DirectoryItemType.Drive ? "drive" : (Type == DirectoryItemType.File ? "file" : (IsExpanded ? "folder-open" : "folder-closed"));

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
                return this.Type == DirectoryItemType.Drive ? this.FullPath : DirectoryStructure.GetFileFolderName(this.FullPath);
            }
        }
        /// <summary>
        /// A list of children inside this item
        /// </summary>
        public ObservableCollection<DirectoryItemViewModel> Children { get; set; }

        /// <summary>
        /// Indicates if this item can be expanded
        /// </summary>
        public bool CanExpand { get { return this.Type != DirectoryItemType.File; } }

        /// <summary>
        /// Indicates if the current item is expanded or not
        /// </summary>
        public bool IsExpanded
        {
            get
            {
                return this.Children?.Count(f => f != null) > 0;
            }
            set
            {
                //If the UI tells us to expand..
                if (value == true)
                    //Find all children
                    Expand();
                //If UI tells us to close
                else
                    this.ClearChildren();
            }
        }
        #endregion

        #region Public Commands
        /// <summary>
        /// The command to expand this item
        /// </summary>
        public ICommand ExpandCommand { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        public DirectoryItemViewModel(string FullPath,DirectoryItemType type)
        {
            //create commands
            this.ExpandCommand = new RelayCommand(Expand);

            //set full path and type
            this.FullPath = FullPath;
            this.Type = type;

            //set up the children as needed
            this.ClearChildren();

           
        }
        #endregion

        #region Helper Methods
        /// <summary>
        /// Removes all children from the list,adding a dummy item to show the expand icon if required.
        /// </summary>
        private void ClearChildren()
        {
            //Clear items
            this.Children = new ObservableCollection<DirectoryItemViewModel>();
            //Show the expand arrow if we are not file
            if (this.Type != DirectoryItemType.File)
                this.Children.Add(null);
        }
        #endregion

        /// <summary>
        /// Expands the directory and finds all the children
        /// </summary>
        private void Expand()
        {
            //we cannot expand a file
            if (this.Type == DirectoryItemType.File)
                return;

            //Find all children
            var children = DirectoryStructure.GetDirectoryContents(this.FullPath);
            this.Children = new ObservableCollection<DirectoryItemViewModel>(children.Select(content => new DirectoryItemViewModel(content.FullPath, content.Type)));
        }
    }
}
