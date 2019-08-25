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
    public class SettingServiceTest
    {
        private SettingService _service;
        private DbContextMock<DatabaseContext> _dbContextMock;

        private DbContextOptions<DatabaseContext> DummyOptions { get; } =
            new DbContextOptionsBuilder<DatabaseContext>().Options;

        [SetUp]
        public void Initialize()
        {
            var settings = GetFakeData();

            _dbContextMock = new DbContextMock<DatabaseContext>(DummyOptions);
            var dbSetMock = _dbContextMock.CreateDbSetMock(x => x.Settings, settings);

            var repository = new SettingRepository(_dbContextMock.Object);
            _service = new SettingService(repository);
        }

        [Test]
        public void GetAllSettings()
        {
            var result = _service.GetAll();
            var count = result.Count();

            Assert.AreEqual(2, count);
        }

        [Test]
        public void GetSetting()
        {
            var result = _service.GetById("1");
            var actual = GetFakeData()[0];

            Assert.AreEqual(result.Name, actual.Name);
        }

        [Test]
        public void AddSetting()
        {
            _service.Add(new SettingDto { Id = "3", Name = "name3", Enabled = true});
            _service.Save();

            var result = _service.GetAll();
            var count = result.Count();

            Assert.AreEqual(3, count);
        }

        [Test]
        public void EditSetting()
        {
            _service.Edit(new SettingDto {Id = "1", Name = "name1", Enabled = false});
            _service.Save();

            var result = _service.GetById("1");

            Assert.AreEqual(result.Enabled, false);
        }

        private Setting[] GetFakeData()
        {
            return new[]
            {
                new Setting {Id = "1", Name = "name1", Enabled = true},
                new Setting { Id = "2", Name = "name2", Enabled = false}
            };
        }
    }
}
