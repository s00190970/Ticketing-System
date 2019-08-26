using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using TicketingSystem.Database.Context;
using TicketingSystem.Database.Entities;
using TicketingSystem.Database.IRepositories;

namespace TicketingSystem.Database.Repositories
{
    public class SettingRepository : ISettingRepository
    {
        private readonly DatabaseContext _context;
        public SettingRepository(DatabaseContext context)
        {
            this._context = context;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public List<Setting> GetAll()
        {
            return _context.Settings.ToList();
        }

        public Setting GetById(string id)
        {
            return _context.Settings.Find(id);
        }

        public Setting Edit(Setting setting)
        {
            var edited = _context.Settings.Find(setting.Id);
            edited.Enabled = setting.Enabled;
            edited.Name = setting.Name;
            return edited;
        }

        public Setting Add(Setting item)
        {
            _context.Settings.Add(item);
            return item;
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
