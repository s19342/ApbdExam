using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamTask.DTOs.Requests;
using ExamTask.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExamTask.Controllers
{
    [Route("api/tasks")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly IDbService _dbService;
        public TasksController(IDbService dbService)
        {
            _dbService = dbService;
        }

        [HttpPut("{index}")]
        public IActionResult updateTasks(string index, TaskTypeDTO ttdto)
        { 

            int checkedTask = 0;

            checkedTask = _dbService.CheckTaskType(ttdto.TaskTypeGiven.IdTaskType);

            if (checkedTask == 0)
            {
                int givenType = 0;
                givenType = _dbService.InsertTaskType(ttdto.TaskTypeGiven);
                if (givenType == 0)
                {
                    return StatusCode(500, "Internal Server Error while inserting");
                }
            }

            int updateTask = 0;

            updateTask = _dbService.UpdateTask(ttdto, index);

            if(updateTask == 0)
            {
                return StatusCode(500, "Internal Server Error while updating");
            }

            return Ok();
        }
    }
}