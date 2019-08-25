using System;
using System.Collections.Generic;
using System.Text;
using TicketingSystem.Core.DTOs;
using TicketingSystem.Core.Helpers;
using TicketingSystem.Database.Entities;
using TicketingSystem.Database.Repositories;

namespace TicketingSystem.Core.Services
{
    public class SettingService : IDisposable
    {
        private readonly SettingRepository _repository;

        public SettingService(SettingRepository repository)
        {
            _repository = repository;
        }

        public void Dispose()
        {
            _repository.Dispose();
        }

        public List<SettingDto> GetAll()
        {
            return MapperHelper<Setting, SettingDto>.ConvertToDtos(_repository.GetAll());
        }

        public SettingDto GetById(string id)
        {
            return MapperHelper<Setting, SettingDto>.ConvertToDto(_repository.GetById(id));
        }

        public SettingDto Edit(SettingDto item)
        {
            Setting setting = MapperHelper<Setting, SettingDto>.ConvertToModel(item);
            SettingDto settingDto = MapperHelper<Setting, SettingDto>.ConvertToDto(_repository.Edit(setting));
            return settingDto;
        }

        public SettingDto Add(SettingDto item)
        {
            Setting setting = MapperHelper<Setting, SettingDto>.ConvertToModel(item);
            SettingDto settingDto = MapperHelper<Setting, SettingDto>.ConvertToDto(_repository.Add(setting));
            return settingDto;
        }

        public void Save()
        {
            _repository.Save();
        }
    }
}
