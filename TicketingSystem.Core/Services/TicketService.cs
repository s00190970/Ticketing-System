﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using AutoMapper;
using TicketingSystem.Core.DTOs;
using TicketingSystem.Core.Helpers;
using TicketingSystem.Database.Entities;
using TicketingSystem.Database.Repositories;

namespace TicketingSystem.Core.Services
{
    public class TicketService
    {
        private readonly TicketRepository _repository;
        private readonly TicketConverter _converter;
        public TicketService(TicketRepository repository, TicketConverter converter)
        {
            _repository = repository;
            _converter = converter;
        }

        public void Dispose()
        {
            _repository.Dispose();
        }

        public void Save()
        {
            _repository.Save();
        }

        public List<TicketDto> GetAll()
        {
            return _converter.ModelsToDtos(_repository.GetAll());
        }

        public TicketDto GetById(string ticketId)
        {
            return _converter.ModelToDto(_repository.GetById(ticketId));
        }

        public TicketDto Add(TicketDto item)
        {
            item.OpenDateTime = DateTime.Now.ToString();
            Ticket ticket = _converter.DtoToModel(item);
            TicketDto ticketDto = _converter.ModelToDto(_repository.Add(ticket));
            return ticketDto;
        }

        public TicketDto Edit(TicketDto item)
        {
            Ticket ticket = _converter.DtoToModel(item);
            TicketDto ticketDto = _converter.ModelToDto(_repository.Edit(ticket));
            return ticketDto;
        }

        public List<TicketDto> GetByUserId(string userId)
        {
            return _converter.ModelsToDtos(_repository.GetByUserId(userId));
        }

    }
}
