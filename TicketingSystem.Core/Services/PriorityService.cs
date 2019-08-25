using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using TicketingSystem.Core.DTOs;
using TicketingSystem.Core.Helpers;
using TicketingSystem.Database.Entities;
using TicketingSystem.Database.Repositories;

namespace TicketingSystem.Core.Services
{
    public class PriorityService : ITicketPropertyService<PriorityDto>
    {
        private readonly PriorityRepository _repository;
        public PriorityService(PriorityRepository repository)
        {
            _repository = repository;
        }

        public void Dispose()
        {
            _repository.Dispose();;
        }

        public List<PriorityDto> GetAll()
        {
            return MapperHelper<Priority, PriorityDto>.ConvertToDtos(_repository.GetAll());
        }

        public PriorityDto GetById(string id)
        {
            return MapperHelper<Priority, PriorityDto>.ConvertToDto(_repository.GetById(id));
        }

        public dynamic Add(PriorityDto item)
        {
            Priority priority = MapperHelper<Priority, PriorityDto>.ConvertToModel(item);
            var output = _repository.Add(priority);

            output["Output"] = MapperHelper<Priority, PriorityDto>.ConvertToDto(output["Output"]);
            return output;
        }

        public void Save(dynamic context)
        {
            _repository.Save(context);
        }
    }
}
