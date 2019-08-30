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
    [Authorize(Roles = "Admin,User")]
    [Route("api/[controller]")]
    [ApiController]
    public class StatusesController : ControllerBase
    {
        private readonly StatusService _service;

        public StatusesController(StatusService service)
        {
            _service = service;
        }

        // GET: api/Statuses
        [HttpGet]
        public ActionResult<List<StatusDto>> GetStatuses()
        {
            return _service.GetAll();
        }

        // POST: api/Statuses
        [HttpPost]
        public ActionResult PostStatus([FromBody] StatusDto statusDto)
        {
            var response = _service.Add(statusDto);
            _service.Save(response);

            return new OkObjectResult(response["Output"]);
        }
    }
}
