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
    
    [Route("api/[controller]")]
    [ApiController]
    public class SettingsController : ControllerBase
    {
        private readonly SettingService _service;

        public SettingsController(SettingService service)
        {
            _service = service;
        }

        // GET: api/Settings
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,User")]
        [HttpGet]
        public ActionResult<List<SettingDto>> GetSettings()
        {
            return _service.GetAll();
        }

        // PUT: api/Settings/5
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        [HttpPut("{id}")]
        public ActionResult<SettingDto> PutSetting(string id, SettingDto settingDto)
        {
            if (id != settingDto.Id)
            {
                return BadRequest();
            }

            try
            {
                settingDto = _service.Edit(settingDto);
                _service.Save();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return settingDto;
        }

        // POST: api/Settings
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        [HttpPost]
        public ActionResult<SettingDto> PostSettings(SettingDto settingDto)
        {
            settingDto = _service.Add(settingDto);
            _service.Save();

            return settingDto;
        }
    }
}
