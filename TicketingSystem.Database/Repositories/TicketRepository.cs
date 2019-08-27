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
            return _context.Tickets
                .Include(p => p.Priority)
                .Include(t => t.TicketType)
                .Include(s => s.ServiceType)
                .Include(s => s.Status)
                .Include(u => u.User)
                .ToList();
        }

        public Ticket GetById(string ticketId)
        {
            return _context.Tickets.Find(ticketId);
        }

        public Ticket Add(Ticket ticket)
        {
            _context.Tickets.Add(ticket);
            return ticket;
        }

        public Ticket Edit(Ticket ticket)
        {
            var edited = _context.Tickets.Find(ticket.Id);
            edited.CloseDateTime = ticket.CloseDateTime;
            edited.CustomerName = ticket.CustomerName;
            edited.Description = ticket.Description;
            edited.OpenDateTime = ticket.OpenDateTime;
            edited.Priority = ticket.Priority;
            edited.ServiceType = ticket.ServiceType;
            edited.Status = ticket.Status;
            edited.Subject = ticket.Subject;
            edited.TicketType = ticket.TicketType;
            edited.User = ticket.User;

            return edited;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public List<Ticket> GetByUserId(string userId)
        {
            return _context.Tickets.Where(t => t.User.Id == userId).ToList();
        }
    }
}
