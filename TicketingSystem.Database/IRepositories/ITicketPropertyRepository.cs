using System;
using System.Collections.Generic;
using System.Text;

namespace TicketingSystem.Database.IRepositories
{
    public interface ITicketPropertyRepository<T> : IDisposable
    {
        List<T> GetAll();
        T GetById(string id);
        T GetByName(string name);
        dynamic Add(T item);
        void Save(dynamic context);
    }
}
