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

    public class Category
    {
        public Category(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
        

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

        public DateTime CreationDate { get; set; }

        public StatusType Status { get; set; }
        public Category Category { get; set; }
        public PriorityType Priority { get; set; }


        public Task(string title, string description, DateTime dueDate, Category category, PriorityType priority)
        {
            Id = RandomIdGenerator.GenerateId();
            Title = title;
            Description = description;
            DueDate = dueDate;
            CreationDate = DateTime.Today;
            Category = category;
            Priority = priority;
            Status = StatusType.Created;
        }

        public Task(string title, string description, DateTime dueDate, StatusType status, Category category, PriorityType priority)
        {
            Title = title;
            Description = description;
            DueDate = dueDate;
            CreationDate = DateTime.Today;
            Status = status;
            Category = category;
            Priority = priority;
        }
    }
}
