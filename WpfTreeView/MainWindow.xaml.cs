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
            //    InitializeComponent();
            //    this.DataContext = new Class1();

             this.DataContext= new DirectoryStructureViewModel();
          //  var d = new DirectoryStructureViewModel();
          //var items1=  d.Items[0];
          //  d.Items[0].ExpandCommand.Execute(null);
          
        }
        #endregion



    }
}
