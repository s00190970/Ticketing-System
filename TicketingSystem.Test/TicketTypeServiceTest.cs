using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityFrameworkCoreMock;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using TicketingSystem.Core.DTOs;
using TicketingSystem.Core.Services;
using TicketingSystem.Database.Context;
using TicketingSystem.Database.Entities;
using TicketingSystem.Database.Repositories;

namespace TicketingSystem.Test
{
    [TestFixture]
    public class TicketTypeServiceTest
    {
        private TicketTypeService _service;
        private DbContextMock<DatabaseContext> _dbContextMock;

        private DbContextOptions<DatabaseContext> DummyOptions { get; } =
            new DbContextOptionsBuilder<DatabaseContext>().Options;

        [SetUp]
        public void Initialize()
        {
            var ticketTypes = GetFakeData();

            _dbContextMock = new DbContextMock<DatabaseContext>(DummyOptions);
            var dbSetMock = _dbContextMock.CreateDbSetMock(x => x.TicketTypes, ticketTypes);

            var repository = new TicketTypeRepository(_dbContextMock.Object);
            _service = new TicketTypeService(repository);
        }

        [Test]
        public void GetAllTicketTypes()
        {
            var result = _service.GetAll();
            var count = result.Count();

            Assert.AreEqual(2, count);
        }

        [Test]
        public void GetTicketType()
        {
            var result = _service.GetById("1");
            var actual = GetFakeData()[0];

            Assert.AreEqual(result.Name, actual.Name);
        }

        [Test]
        public void AddTicketType()
        {
            _service.Add(new TicketTypeDto { Id = "3", Name = "name3" });
            _service.Save(_dbContextMock);

            var result = _service.GetAll();
            var count = result.Count();

            Assert.AreEqual(3, count);
        }

        private TicketType[] GetFakeData()
        {
            return new[]
            {
                new TicketType {Id = "1", Name = "name1"},
                new TicketType { Id = "2", Name = "name2" }
            };
        }
    }
}
