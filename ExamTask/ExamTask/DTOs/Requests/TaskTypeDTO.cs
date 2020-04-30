using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamTask.Models;

namespace ExamTask.DTOs.Requests
{
    public class TaskTypeDTO
    {

        public string Name { get; set; }
        public string Description { get; set; }
        public string Deadline { get; set; }
        public int IdProject { get; set; }
        public int IdAssignedTo { get; set; }
        public int IdCreator { get; set; }
        public TaskType TaskTypeGiven { get; set; }
    }
}
