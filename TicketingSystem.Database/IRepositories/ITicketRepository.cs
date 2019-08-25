using System;
using System.Collections.Generic;
using System.Text;
using TicketingSystem.Database.Entities;

namespace TicketingSystem.Database.IRepositories
{
    public interface ITicketRepository : IDisposable
    {
        List<Ticket> GetAll();
        Ticket GetById(string ticketId);
        Ticket Add(Ticket ticket);
        Ticket Edit(Ticket ticket);
        void Save();
    }
}
