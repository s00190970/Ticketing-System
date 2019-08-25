using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TicketingSystem.Database.Context;
using TicketingSystem.Database.Entities;
using TicketingSystem.Database.IRepositories;

namespace TicketingSystem.Database.Repositories
{
    public class StatusRepository : IStatusRepository
    {
        private readonly DatabaseContext _context;

        public StatusRepository(DatabaseContext context)
        {
            _context = context;
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        public List<Status> GetAll()
        {
            return _context.Statuses.ToList();
        }

        public Status GetById(string id)
        {
            return _context.Statuses.Find(id);
        }

        public dynamic Add(Status item)
        {
            dynamic context = new Dictionary<string, object>();
            context["Output"] = _context.Statuses.Add(item).Entity;

            return context;
        }

        public void Save(dynamic context)
        {
            try
            {
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                context["Output"] = e;
            }
        }
    }
}
