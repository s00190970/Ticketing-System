using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketingSystem.Core.DTOs;
using TicketingSystem.Core.Services;
using TicketingSystem.Database.Entities;

namespace TicketingSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserService _service;

        public UsersController(UserService service)
        {
            _service = service;
        }

        // GET: api/Users
        [HttpGet]
        public ActionResult<List<UserDto>> GetUsers()
        {
            return _service.GetAll();
        }

        // GET: api/Users/5
        [HttpGet]
        public ActionResult<UserDto> GetUser(string id)
        {
            var user = _service.GetById(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public ActionResult PutUser(string id, [FromBody] UserDto userDto)
        {
            if (id != userDto.Id)
            {
                return BadRequest();
            }
            var response = _service.Edit(userDto);
            _service.Save(response);

            return new OkObjectResult(response["Output"]);
        }

        // POST: api/Users
        [HttpPost]
        public ActionResult PostUser([FromBody] UserDto userDto)
        {

            var response = _service.Add(userDto);
            _service.Save(response);

            return new OkObjectResult(response["Output"]);
        }
    }
}