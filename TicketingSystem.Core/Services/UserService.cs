using System;
using System.Collections.Generic;
using System.Text;
using TicketingSystem.Core.DTOs;
using TicketingSystem.Core.Helpers;
using TicketingSystem.Database.Entities;
using TicketingSystem.Database.Repositories;

namespace TicketingSystem.Core.Services
{
    public class UserService
    {
        private readonly UserRepository _repository;

        public UserService(UserRepository repository)
        {
            _repository = repository;
        }

        public void Dispose()
        {
            _repository.Dispose();
        }

        public void Save(dynamic context)
        {
            _repository.Save(context);
        }

        public List<UserDto> GetAll()
        {
            return MapperHelper<User, UserDto>.ConvertToDtos(_repository.GetAll());
        }

        public UserDto GetById(string id)
        {
            return MapperHelper<User, UserDto>.ConvertToDto(_repository.GetById(id));
        }

        public dynamic Add(UserDto item)
        {
            User user = MapperHelper<User, UserDto>.ConvertToModel(item);
            var output = _repository.Add(user);

            output["Output"] = MapperHelper<User, UserDto>.ConvertToDto(output["Output"]);
            return output;
        }

        public dynamic Edit(UserDto item)
        {
            User user = MapperHelper<User, UserDto>.ConvertToModel(item);
            UserDto userDto = MapperHelper<User, UserDto>.ConvertToDto(_repository.Edit(user));
            return userDto;
        }
    }
}
