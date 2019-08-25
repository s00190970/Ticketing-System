using System;
using System.Collections.Generic;
using System.Text;

namespace TicketingSystem.Core.Services
{
    public interface ITicketPropertyService<T> : IDisposable
    {
        List<T> GetAll();
        T GetById(string id);
        dynamic Add(T item);
        void Save(dynamic context);
    }
}
