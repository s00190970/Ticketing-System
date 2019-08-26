using System;
using System.Collections.Generic;
using System.Text;
using TicketingSystem.Database.Entities;

namespace TicketingSystem.Database.IRepositories
{
    public interface IUserRepository : IDisposable
    {
        List<User> GetAll();
        User GetById(string id);
        dynamic Add(User user);
        dynamic Edit(User user);
        void Save(dynamic context);
    }
}
