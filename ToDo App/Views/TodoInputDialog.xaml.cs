using Microsoft.Win32;
using System;
using System.Collections.Generic;
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

namespace ToDo_App.Views
{
    /// <summary>
    /// Interaction logic for TodoInputDialog.xaml
    /// </summary>
    public partial class TodoInputDialog : UserControl
    {
        public static readonly DependencyProperty DestinationPathProperty = DependencyProperty.Register(
            "DestinationPath",
            typeof(string),
            typeof(TodoInputDialog),
            new PropertyMetadata(null));

        public string DestinationPath
        {
            get => (string)GetValue(DestinationPathProperty);
            set => SetValue(DestinationPathProperty, value);
        }
        public TodoInputDialog()
        {
            InitializeComponent();
        }
        private void Browse_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == true)
            {
                DestinationPath = dialog.FileName;
            }
        }
    }
}
