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
    public class ServiceTypeService : ITicketPropertyService<ServiceTypeDto>
    {
        private readonly ServiceTypeRepository _repository;

        public ServiceTypeService(ServiceTypeRepository repository)
        {
            _repository = repository;
        }

        public void Dispose()
        {
            _repository.Dispose();
        }

        public List<ServiceTypeDto> GetAll()
        {
            return MapperHelper<ServiceType, ServiceTypeDto>.ConvertToDtos(_repository.GetAll());
        }

        public ServiceTypeDto GetById(string id)
        {
            return MapperHelper<ServiceType, ServiceTypeDto>.ConvertToDto(_repository.GetById(id));
        }

        public ServiceTypeDto GetByName(string name)
        {
            return MapperHelper<ServiceType, ServiceTypeDto>.ConvertToDto(_repository.GetByName(name));
        }

        public dynamic Add(ServiceTypeDto item)
        {
            ServiceType serviceType = MapperHelper<ServiceType, ServiceTypeDto>.ConvertToModel(item);
            var output = _repository.Add(serviceType);

            output["Output"] = MapperHelper<ServiceType, ServiceTypeDto>.ConvertToDto(output["Output"]);
            return output;
        }

        public void Save(dynamic context)
        {
            _repository.Save(context);
        }
    }
}
