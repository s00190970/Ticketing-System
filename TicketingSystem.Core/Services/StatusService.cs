using System;
using System.Collections.Generic;
using System.Text;
using TicketingSystem.Core.DTOs;
using TicketingSystem.Core.Helpers;
using TicketingSystem.Database.Entities;
using TicketingSystem.Database.Repositories;

namespace TicketingSystem.Core.Services
{
    public class StatusService : ITicketPropertyService<StatusDto>
    {
        private readonly StatusRepository _repository;

        public StatusService(StatusRepository repository)
        {
            _repository = repository;
        }
        public void Dispose()
        {
            _repository.Dispose();
        }

        public List<StatusDto> GetAll()
        {
            return MapperHelper<Status, StatusDto>.ConvertToDtos(_repository.GetAll());
        }

        public StatusDto GetById(string id)
        {
            return MapperHelper<Status, StatusDto>.ConvertToDto(_repository.GetById(id));
        }

        public StatusDto GetByName(string name)
        {
            return MapperHelper<Status, StatusDto>.ConvertToDto(_repository.GetByName(name));
        }

        public dynamic Add(StatusDto item)
        {
            Status status = MapperHelper<Status, StatusDto>.ConvertToModel(item);
            var output = _repository.Add(status);

            output["Output"] = MapperHelper<Status, StatusDto>.ConvertToDto(output["Output"]);

            return output;

        }

        public void Save(dynamic context)
        {
            _repository.Save(context);
        }
    }
}
