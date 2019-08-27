using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using TicketingSystem.Core.DTOs;
using TicketingSystem.Database.Context;
using TicketingSystem.Database.Entities;
using TicketingSystem.Database.Repositories;

namespace TicketingSystem.Core.Helpers
{
    public class TicketConverter
    {
        private readonly PriorityRepository _priorityRepository;
        private readonly ServiceTypeRepository _serviceTypeRepository;
        private readonly StatusRepository _statusRepository;
        private readonly TicketTypeRepository _ticketTypeRepository;
        private readonly UserRepository _userRepository;

        public TicketConverter(PriorityRepository priorityRepository, 
            ServiceTypeRepository serviceTypeRepository, 
            StatusRepository statusRepository, 
            TicketTypeRepository ticketTypeRepository,
            UserRepository userRepository)
        {
            _priorityRepository = priorityRepository;
            _serviceTypeRepository = serviceTypeRepository;
            _statusRepository = statusRepository;
            _ticketTypeRepository = ticketTypeRepository;
            _userRepository = userRepository;
        }

        public Ticket DtoToModel(TicketDto dto)
        {
            
            if (dto == null)
            {
                return null;
            }

            Ticket ticket = new Ticket
            {
                Id = dto.Id,
                OpenDateTime = DateTime.Parse(dto.OpenDateTime),
                CloseDateTime = dto.CloseDateTime == null ? (DateTime?)null : DateTime.Parse(dto.CloseDateTime),
                CustomerName = dto.CustomerName,
                Subject = dto.Subject,
                Description = dto.Description,
                User = _userRepository.GetByName(dto.UserName),
                Priority = _priorityRepository.GetByName(dto.PriorityName),
                ServiceType = _serviceTypeRepository.GetByName(dto.ServiceTypeName),
                Status = _statusRepository.GetByName(dto.StatusName),
                TicketType = _ticketTypeRepository.GetByName(dto.TicketTypeName)
            };
            return ticket;
        }

        public TicketDto ModelToDto(Ticket ticket)
        {
            if (ticket == null)
            {
                return null;
            }

            TicketDto dto = new TicketDto
            {
                Id = ticket.Id,
                OpenDateTime = ticket.OpenDateTime.ToString(CultureInfo.InvariantCulture),
                CloseDateTime = ticket.CloseDateTime.ToString(),
                CustomerName = ticket.CustomerName,
                Subject = ticket.Subject,
                Description = ticket.Description,
                PriorityName = ticket.Priority.Name,
                ServiceTypeName = ticket.ServiceType.Name,
                StatusName = ticket.Status.Name,
                TicketTypeName = ticket.TicketType.Name,
                UserName = ticket.User.UserName,
                UserEmail = ticket.User.Email
            };

            return dto;
        }

        public List<TicketDto> ModelsToDtos(List<Ticket> tickets)
        {
            List<TicketDto> dtos = new List<TicketDto>();
            foreach (var ticket in tickets)
            {
                dtos.Add(ModelToDto(ticket));
            }

            return dtos;
        }
    }
}
