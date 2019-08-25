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
    public class TicketRepository : ITicketRepository
    {
        private readonly DatabaseContext _context;
        public TicketRepository(DatabaseContext context)
        {
            this._context = context;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public List<Ticket> GetAll()
        {
            return _context.Tickets.ToList();
        }

        public Ticket GetById(string ticketId)
        {
            return _context.Tickets.Find(ticketId);
        }

        public Ticket Add(Ticket ticket)
        {
            return _context.Tickets.Add(ticket).Entity;
        }

        public Ticket Edit(Ticket ticket)
        {
            _context.Entry(ticket).State = EntityState.Modified;
            return _context.Tickets.Find(ticket.Id);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
