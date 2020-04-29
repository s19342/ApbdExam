using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamTask.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExamTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IDbService _dbService;

        public TestController(IDbService dbService)
        {
            _dbService = dbService;
        }
    }
}