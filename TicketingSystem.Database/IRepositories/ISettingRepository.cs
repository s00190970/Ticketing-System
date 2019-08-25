using System;
using System.Collections.Generic;
using System.Text;
using TicketingSystem.Database.Entities;

namespace TicketingSystem.Database.IRepositories
{
    public interface ISettingRepository : IDisposable
    {
        List<Setting> GetAll();
        Setting GetById(string id);
        Setting Edit(Setting setting);
        Setting Add(Setting item);
        void Save();
    }
}
