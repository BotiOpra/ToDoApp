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
    /// Interaction logic for TaskEditDialog.xaml
    /// </summary>
    public partial class TaskEditDialog : UserControl
    {
        public TaskEditDialog()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TitleTextBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            DescriptionTextBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            DueDatePicker.GetBindingExpression(DatePicker.SelectedDateProperty).UpdateSource();
            CategoryCombo.GetBindingExpression(ComboBox.SelectedValueProperty).UpdateSource();
            PriorityCombo.GetBindingExpression(ComboBox.SelectedValueProperty).UpdateSource();
        }
    }
}
