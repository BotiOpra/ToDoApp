using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo_App.Utilities;

namespace ToDo_App.Models
{
    public enum PriorityType
    {
        [Description("High")]
        High,
        [Description("Medium")]
        Medium,
        [Description("Low")]
        Low
    }

    public enum CategoryType
    {
        [Description("School")]
        School,
        [Description("Shopping")]
        Shopping,
        [Description("Other")]
        Other,
    }

    public enum StatusType
    {
        Created,
        InProgress,
        Done
    }

    public class Task
    { 
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public DateTime DueDate { get; set; }

        public StatusType Status { get; set; }
        public CategoryType Category { get; set; }
        public PriorityType Priority { get; set; }


        public Task(string title, string description, DateTime dueDate, CategoryType category, PriorityType priority)
        {
            Id = RandomIdGenerator.GenerateId();
            Title = title;
            Description = description;
            DueDate = dueDate;
            Category = category;
            Priority = priority;
            Status = StatusType.Created;
        }

        public Task(string title, string description, DateTime dueDate, StatusType status, CategoryType category, PriorityType priority)
        {
            Title = title;
            Description = description;
            DueDate = dueDate;
            Status = status;
            Category = category;
            Priority = priority;
        }
    }
}
