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
    public class ServiceTypeServiceTest
    {
        private ServiceTypeService _service;
        private DbContextMock<DatabaseContext> _dbContextMock;

        private DbContextOptions<DatabaseContext> DummyOptions { get; } =
            new DbContextOptionsBuilder<DatabaseContext>().Options;

        [SetUp]
        public void Initialize()
        {
            var serviceTypes = GetFakeData();

            _dbContextMock = new DbContextMock<DatabaseContext>(DummyOptions);
            var dbSetMock = _dbContextMock.CreateDbSetMock(x => x.ServiceTypes, serviceTypes);

            var repository = new ServiceTypeRepository(_dbContextMock.Object);
            _service = new ServiceTypeService(repository);
        }

        [Test]
        public void GetAllServiceTypes()
        {
            var result = _service.GetAll();
            var count = result.Count();

            Assert.AreEqual(2, count);
        }

        [Test]
        public void GetServiceType()
        {
            var result = _service.GetById("1");
            var actual = GetFakeData()[0];

            Assert.AreEqual(result.Name, actual.Name);
        }

        [Test]
        public void AddServiceType()
        {
            _service.Add(new ServiceTypeDto { Id = "3", Name = "name3" });
            _service.Save(_dbContextMock);

            var result = _service.GetAll();
            var count = result.Count();

            Assert.AreEqual(3, count);
        }

        private ServiceType[] GetFakeData()
        {
            return new[]
            {
                new ServiceType {Id = "1", Name = "name1"},
                new ServiceType { Id = "2", Name = "name2" }
            };
        }
    }
}
