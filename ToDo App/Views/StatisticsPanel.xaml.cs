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
    /// Interaction logic for StatisticsPanel.xaml
    /// </summary>
    public partial class StatisticsPanel : UserControl
    {
        public StatisticsPanel()
        {
            InitializeComponent();
        }

        public int NrDueTodayTasks
        {
            get { return (int)GetValue(NrDueTodayTasksProperty); }
            set { SetValue(NrDueTodayTasksProperty, value); }
        }   

        public static readonly DependencyProperty NrDueTodayTasksProperty =
            DependencyProperty.Register("NrDueTodayTasks", typeof(int), typeof(StatisticsPanel), new PropertyMetadata(0));

        public int NrDueTomorrowTasks
        {
            get { return (int)GetValue(NrDueTomorrowTasksProperty); }
            set { SetValue(NrDueTomorrowTasksProperty, value); }
        }

        public static readonly DependencyProperty NrDueTomorrowTasksProperty =
            DependencyProperty.Register("NrDueTomorrowTasks", typeof(int), typeof(StatisticsPanel), new PropertyMetadata(0));

        public int NrOverDueTasks
        {
            get { return (int)GetValue(NrOverDueTasksProperty); }
            set { SetValue(NrOverDueTasksProperty, value); }
        }

        public static readonly DependencyProperty NrOverDueTasksProperty =
            DependencyProperty.Register("NrOverDueTasks", typeof(int), typeof(StatisticsPanel), new PropertyMetadata(0));

        public int NrDoneTasks
        {
            get { return (int)GetValue(NrDoneTasksProperty); }
            set { SetValue(NrDoneTasksProperty, value); }
        }

        public static readonly DependencyProperty NrDoneTasksProperty =
            DependencyProperty.Register("NrDoneTasks", typeof(int), typeof(StatisticsPanel), new PropertyMetadata(0));

        public int NrTasksLeft
        {
            get { return (int)GetValue(NrTasksLeftProperty); }
            set { SetValue(NrTasksLeftProperty, value); }
        }

        public static readonly DependencyProperty NrTasksLeftProperty =
            DependencyProperty.Register("NrTasksLeft", typeof(int), typeof(StatisticsPanel), new PropertyMetadata(0));

    }
}
