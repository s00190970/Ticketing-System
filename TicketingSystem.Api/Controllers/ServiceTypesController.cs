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
    public class ServiceTypesController : ControllerBase
    {
        private readonly ServiceTypeService _service;

        public ServiceTypesController(ServiceTypeService service)
        {
            _service = service;
        }

        // GET: api/ServiceTypes
        [HttpGet]
        public ActionResult<List<ServiceTypeDto>> GetServiceTypes()
        {
            return _service.GetAll();
        }

        // POST: api/ServiceTypes
        [HttpPost]
        public ActionResult PostServiceType([FromBody] ServiceTypeDto serviceTypeDto)
        {
            var response = _service.Add(serviceTypeDto);
            _service.Save(response);

            return new OkObjectResult(response["Output"]);
        }
    }
}
