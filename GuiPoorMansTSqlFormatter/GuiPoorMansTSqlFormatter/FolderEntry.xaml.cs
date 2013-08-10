using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UserControl = System.Windows.Controls.UserControl;

namespace GuiPoorMansTSqlFormatter
{
    /// <summary>
    /// Interaction logic for FolderEntry.xaml
    /// </summary>
    public partial class FolderEntry : UserControl
    {
        public static DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(FolderEntry), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        public static DependencyProperty DescriptionProperty = DependencyProperty.Register("Description", typeof(string), typeof(FolderEntry), new PropertyMetadata(null));
        public static DependencyProperty IsFolderSelectedProperty = DependencyProperty.Register("IsFolderSelected", typeof(bool?), typeof(FolderEntry), new PropertyMetadata(null));


        public string Text { get { return GetValue(TextProperty) as string; } set { SetValue(TextProperty, value); } }
        public string Description { get { return GetValue(DescriptionProperty) as string; } set { SetValue(DescriptionProperty, value); } }
        public bool? IsFolderSelected { get { return GetValue(IsFolderSelectedProperty) as bool?; } set { SetValue(IsFolderSelectedProperty, value); } }

        public FolderEntry() { InitializeComponent();
            ButtonSelectFolder.Content = "   ...   ";
        }

        private void BrowseFolder(object sender, RoutedEventArgs e)
        {
            using (var dlg = new FolderBrowserDialog())
            {
                dlg.Description = Description;
                dlg.SelectedPath = Text;
                dlg.ShowNewFolderButton = true;
                var result = dlg.ShowDialog();
                if (result == DialogResult.OK)
                {
                    Text = dlg.SelectedPath;
                    IsFolderSelected = true;
                    var be = GetBindingExpression(TextProperty);
                    if (be != null)
                        be.UpdateSource();
                }
            }
        }
    }
}
