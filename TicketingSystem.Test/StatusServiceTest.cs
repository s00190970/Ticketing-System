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
    public class StatusServiceTest
    {
        private StatusService _service;
        private DbContextMock<DatabaseContext> _dbContextMock;

        private DbContextOptions<DatabaseContext> DummyOptions { get; } =
            new DbContextOptionsBuilder<DatabaseContext>().Options;

        [SetUp]
        public void Initialize()
        {
            var statuses = GetFakeData();

            _dbContextMock = new DbContextMock<DatabaseContext>(DummyOptions);
            var dbSetMock = _dbContextMock.CreateDbSetMock(x => x.Statuses, statuses);

            var repository = new StatusRepository(_dbContextMock.Object);
            _service = new StatusService(repository);
        }

        [Test]
        public void GetAllStatuses()
        {
            var result = _service.GetAll();
            var count = result.Count();

            Assert.AreEqual(2, count);
        }

        [Test]
        public void GetStatus()
        {
            var result = _service.GetById("1");
            var actual = GetFakeData()[0];

            Assert.AreEqual(result.Name, actual.Name);
        }

        [Test]
        public void AddStatus()
        {
            _service.Add(new StatusDto { Id = "3", Name = "name3" });
            _service.Save(_dbContextMock);

            var result = _service.GetAll();
            var count = result.Count();

            Assert.AreEqual(3, count);
        }

        private Status[] GetFakeData()
        {
            return new[]
            {
                new Status {Id = "1", Name = "name1"},
                new Status { Id = "2", Name = "name2" }
            };
        }
    }
}
