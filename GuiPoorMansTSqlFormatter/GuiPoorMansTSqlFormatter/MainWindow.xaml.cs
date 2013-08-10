using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace GuiPoorMansTSqlFormatter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        delegate void UpdateProgressBarDelegate(DependencyProperty dp, object value);

        public MainWindow()
        {
            InitializeComponent();

            ProgressBarFilesFormat.Visibility = Visibility.Hidden;

            FolderEntryFolderPath.ButtonSelectFolder.Click += (sender, args) =>
                {
                    if (FolderEntryFolderPath.IsFolderSelected == true)
                    {
                        ProgressBarFilesFormat.Visibility = Visibility.Visible;
                        FolderEntryFolderPath.Visibility = Visibility.Hidden;

                        new FormatSqlViewModel(FolderEntryFolderPath.Text, 
                            new UpdateProgressBarDelegate(ProgressBarFilesFormat.SetValue));
                     
                    }
                };
        }
    }
}
