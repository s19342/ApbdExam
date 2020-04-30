using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamTask.DTOs.Requests;
using ExamTask.DTOs.Responses;
using ExamTask.Models;

namespace ExamTask.Services
{
    public interface IDbService
    {
        public List<TaskResponse> ListAssigned(string index);
        public List<TaskResponse> ListCreated(string index);
        public int CheckTaskType(int index);
        public int InsertTaskType(TaskType tt);
        public int UpdateTask(TaskTypeDTO ttdto, string index);
    }
}
