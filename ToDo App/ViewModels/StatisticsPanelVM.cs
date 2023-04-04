using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo_App.ViewModels
{
    public class StatisticsPanelVM
    {
        public int NrDueTodayTasks { get; set; }
        public int NrDueTomorrowTasks { get; set; }
        public int NrOverdueTasks { get; set; }
        public int NrDoneTasks { get; set; }
        public int NrTasksLeft { get; set; }

        public StatisticsPanelVM() 
        {
        }

    }
}
