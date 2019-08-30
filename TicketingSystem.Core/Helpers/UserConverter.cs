using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TicketingSystem.Core.DTOs;
using TicketingSystem.Database.Entities;

namespace TicketingSystem.Core.Helpers
{
    public class UserConverter
    {
        private readonly TicketConverter _ticketConverter;

        public UserConverter(TicketConverter ticketConverter)
        {
            _ticketConverter = ticketConverter;
        }

        public UserDto ModelToDto(User user)
        {
            if (user == null)
            {
                return null;
            }

            UserDto userDto = new UserDto
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Password = null,
                Token = null,
                Tickets = _ticketConverter.ModelsToDtos(user.Tickets.ToList())
            };

            return userDto;
        }

        public List<UserDto> ModelsToDtos(List<User> users)
        {
            List<UserDto> dtos = new List<UserDto>();
            foreach (var user in users)
            {
                dtos.Add(ModelToDto(user));
            }

            return dtos;
        }
    }
}
