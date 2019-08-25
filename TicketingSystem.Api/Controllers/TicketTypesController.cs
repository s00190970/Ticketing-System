using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketingSystem.Core.DTOs;
using TicketingSystem.Core.Services;
using TicketingSystem.Database.Context;
using TicketingSystem.Database.Entities;

namespace TicketingSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketTypesController : ControllerBase
    {
        private readonly TicketTypeService _service;

        public TicketTypesController(TicketTypeService service)
        {
            _service = service;
        }

        // GET: api/TicketTypes
        [HttpGet]
        public ActionResult<List<TicketTypeDto>> GetTicketTypes()
        {
            return _service.GetAll();
        }

        // POST: api/TicketTypes
        [HttpPost]
        public ActionResult PostTicketType([FromBody] TicketTypeDto ticketTypeDto)
        {
            var response = _service.Add(ticketTypeDto);
            _service.Save(response);

            return new OkObjectResult(response["Output"]);
        }
    }
}
