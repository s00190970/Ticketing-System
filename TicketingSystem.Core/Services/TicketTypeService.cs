using System;
using System.Collections.Generic;
using System.Text;
using TicketingSystem.Core.DTOs;
using TicketingSystem.Core.Helpers;
using TicketingSystem.Database.Entities;
using TicketingSystem.Database.Repositories;

namespace TicketingSystem.Core.Services
{
    public class TicketTypeService : ITicketPropertyService<TicketTypeDto>
    {
        private readonly TicketTypeRepository _repository;
        public TicketTypeService(TicketTypeRepository repository)
        {
            _repository = repository;
        }

        public void Dispose()
        {
            _repository.Dispose();
        }

        public List<TicketTypeDto> GetAll()
        {
            return MapperHelper<TicketType, TicketTypeDto>.ConvertToDtos(_repository.GetAll());
        }

        public TicketTypeDto GetById(string id)
        {
            return MapperHelper<TicketType, TicketTypeDto>.ConvertToDto(_repository.GetById(id));
        }

        public dynamic Add(TicketTypeDto item)
        {
            TicketType ticketType = MapperHelper<TicketType, TicketTypeDto>.ConvertToModel(item);
            var output = _repository.Add(ticketType);

            output["Output"] = MapperHelper<TicketType, TicketTypeDto>.ConvertToDto(output["Output"]);
            return output;
        }

        public void Save(dynamic context)
        {
            _repository.Save(context);
        }
    }
}
