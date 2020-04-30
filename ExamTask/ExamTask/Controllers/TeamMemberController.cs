using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamTask.DTOs.Responses;
using ExamTask.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExamTask.Controllers
{
    [Route("api/team-members")]
    [ApiController]
    public class TeamMemberController : ControllerBase
    {
        private readonly IDbService _dbService;

        public TeamMemberController(IDbService dbService)
        {
            _dbService = dbService;
        }

        [HttpGet("{index}/{tasktype}")]
        public IActionResult GetTasks(string index, string tasktype)
        {
            List <TaskResponse> listOfTasks = null;

            if (tasktype == "assigned")
            {
                listOfTasks =  _dbService.ListAssigned(index);
            }else if (tasktype == "creator")
            {
                listOfTasks = _dbService.ListCreated(index);
            }else
            {
                return BadRequest();
            }

            if(listOfTasks == null)
            {
                return NotFound("Team member with that id does not exist)");
            }

            return Ok(listOfTasks);
        }
    }
}