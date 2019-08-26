using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityFrameworkCoreMock;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using TicketingSystem.Core.DTOs;
using TicketingSystem.Core.Services;
using TicketingSystem.Database.Context;
using TicketingSystem.Database.Entities;
using TicketingSystem.Database.Repositories;

namespace TicketingSystem.Test
{
    [TestFixture]
    public class PriorityServiceTest
    {
        private PriorityService _service;
        private DbContextMock<DatabaseContext> _dbContextMock;

        private DbContextOptions<DatabaseContext> DummyOptions { get; } =
            new DbContextOptionsBuilder<DatabaseContext>().Options;

        [SetUp]
        public void Initialize()
        {
            var priorities = GetFakeData();

            _dbContextMock = new DbContextMock<DatabaseContext>(DummyOptions);
            var dbSetMock = _dbContextMock.CreateDbSetMock(x => x.Priorities, priorities);

            var repository = new PriorityRepository(_dbContextMock.Object);
            _service = new PriorityService(repository);
        }

        [Test]
        public void GetAllPriorities()
        {
            var result = _service.GetAll();
            var count = result.Count();

            Assert.AreEqual(2, count);
        }

        [Test]
        public void GetPriority()
        {
            var result = _service.GetById("1");
            var actual = GetFakeData()[0];

            Assert.AreEqual(result.Name, actual.Name);
        }

        [Test]
        public void AddPriority()
        {
            _service.Add(new PriorityDto {Id = "3", Name = "name3" });
            _service.Save(_dbContextMock);

            var result = _service.GetAll();
            var count = result.Count();

            Assert.AreEqual(3, count);

            var addedEntity = result.Find(x => x.Name == "name3");
            Assert.NotNull(addedEntity);
        }

        private Priority[] GetFakeData()
        {
            return new[]
            {
                new Priority {Id = "1", Name = "name1"},
                new Priority { Id = "2", Name = "name2" }
            };
        }
    }
}
