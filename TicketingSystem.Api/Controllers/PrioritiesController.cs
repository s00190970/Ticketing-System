using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketingSystem.Core.DTOs;
using TicketingSystem.Core.Services;
using TicketingSystem.Database.Context;
using TicketingSystem.Database.Entities;

namespace TicketingSystem.Api.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,User")]
    [Route("api/[controller]")]
    [ApiController]
    public class PrioritiesController : ControllerBase
    {
        private readonly PriorityService _service;

        public PrioritiesController(PriorityService service)
        {
            _service = service;
        }

        // GET: api/Priorities
        [HttpGet]
        public ActionResult<List<PriorityDto>> GetPriorities()
        {
            return _service.GetAll();
        }

        // POST: api/Priorities
        [HttpPost]
        public ActionResult PostPriority([FromBody]PriorityDto priorityDto)
        {
            var response = _service.Add(priorityDto);
            _service.Save(response);

            return new OkObjectResult(response["Output"]);
        }
    }
}
