using System;
using System.Collections.Generic;
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
                OpenDateTime = dto.OpenDateTime,
                CloseDateTime = dto.CloseDateTime,
                CustomerName = dto.CustomerName,
                Subject = dto.Subject,
                Description = dto.Description,
                User = _userRepository.GetById(dto.UserId),
                Priority = _priorityRepository.GetById(dto.PriorityId),
                ServiceType = _serviceTypeRepository.GetById(dto.ServiceTypeId),
                Status = _statusRepository.GetById(dto.StatusId),
                TicketType = _ticketTypeRepository.GetById(dto.TicketTypeId)
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
                OpenDateTime = ticket.OpenDateTime,
                CloseDateTime = ticket.CloseDateTime,
                CustomerName = ticket.CustomerName,
                Subject = ticket.Subject,
                Description = ticket.Description,
                PriorityId = ticket.Priority.Id,
                ServiceTypeId = ticket.ServiceType.Id,
                StatusId = ticket.Status.Id,
                TicketTypeId = ticket.TicketType.Id
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
