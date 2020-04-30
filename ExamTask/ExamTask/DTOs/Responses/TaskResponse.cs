using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamTask.DTOs.Responses
{
    public class TaskResponse
    {
        public string TaskName { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
        public string TaskType { get; set; }
    }
}
