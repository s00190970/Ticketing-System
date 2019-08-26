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
    public class TicketsController : ControllerBase
    {
        private readonly TicketService _service;

        public TicketsController(TicketService service)
        {
            _service = service;
        }

        // GET: api/Tickets
        [HttpGet]
        public ActionResult<List<TicketDto>> GetTickets()
        {
            return _service.GetAll();
        }

        // GET: api/Tickets/5
        [HttpGet("{id}")]
        public ActionResult<TicketDto> GetTicket(string id)
        {
            var ticket = _service.GetById(id);

            if (ticket == null)
            {
                return NotFound();
            }

            return ticket;
        }

        // PUT: api/Tickets/5
        [HttpPut("{id}")]
        public ActionResult<TicketDto> PutTicket(string id, [FromBody] TicketDto ticketDto)
        {
            if (id != ticketDto.Id)
            {
                return BadRequest();
            }

            try
            {
                ticketDto = _service.Edit(ticketDto);
                _service.Save();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return ticketDto;
        }

        // POST: api/Tickets
        [HttpPost]
        public ActionResult<TicketDto> PostTicket([FromBody]TicketDto ticketDto)
        {
            ticketDto = _service.Add(ticketDto);
            _service.Save();

            return ticketDto;
        }
    }
}
