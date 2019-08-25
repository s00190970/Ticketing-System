using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityFrameworkCoreMock;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using TicketingSystem.Core.DTOs;
using TicketingSystem.Core.Helpers;
using TicketingSystem.Core.Services;
using TicketingSystem.Database.Context;
using TicketingSystem.Database.Entities;
using TicketingSystem.Database.Repositories;

namespace TicketingSystem.Test
{
    [TestFixture]
    public class TicketServiceTest
    {
        private TicketService _service;
        private DbContextMock<DatabaseContext> _dbContextMock;

        private DbContextOptions<DatabaseContext> DummyOptions { get; } =
            new DbContextOptionsBuilder<DatabaseContext>().Options;

        [SetUp]
        public void Initialize()
        {
            var tickets = GetFakeData();

            _dbContextMock = new DbContextMock<DatabaseContext>(DummyOptions);
            var dbSetMock = _dbContextMock.CreateDbSetMock(x => x.Tickets, tickets);

            var repository = new TicketRepository(_dbContextMock.Object);
            var convertor = new TicketConverter(new PriorityRepository(_dbContextMock.Object),
                new ServiceTypeRepository(_dbContextMock.Object),
                new StatusRepository(_dbContextMock.Object),
                new TicketTypeRepository(_dbContextMock.Object));

            _service = new TicketService(repository, convertor);
        }

        [Test]
        public void GetAllTickets()
        {
            var result = _service.GetAll();
            var count = result.Count();

            Assert.AreEqual(2, count);
        }

        [Test]
        public void GetTicket()
        {
            var result = _service.GetById("1");
            var actual = GetFakeData()[0];

            Assert.AreEqual(result.Subject, actual.Subject);
        }

        [Test]
        public void AddTicket()
        {
            _service.Add(new TicketDto { Id = "3", Subject = "name3", ServiceTypeId = "123" });
            _service.Save();

            var result = _service.GetAll();
            var count = result.Count();

            Assert.AreEqual(3, count);
        }

        [Test]
        public void EditSetting()
        {
            _service.Edit(new TicketDto { Id = "1", Subject = "name1", ServiceTypeId = "345" });
            _service.Save();

            var result = _service.GetById("1");

            Assert.AreEqual(result.ServiceTypeId, "345");
        }

        private Ticket[] GetFakeData()
        {
            return new[]
            {
                new Ticket {Id = "1", Subject = "name1",
                    ServiceType = new ServiceType{Id = "service1", Name = "service name 1"}},
                new Ticket { Id = "2", Subject = "name2",
                    ServiceType = new ServiceType {Id = "service2", Name = "service name 2"}}
            };
        }
    }
}
