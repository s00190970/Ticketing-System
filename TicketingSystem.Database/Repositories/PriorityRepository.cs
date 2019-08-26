using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TicketingSystem.Database.Context;
using TicketingSystem.Database.Entities;
using TicketingSystem.Database.IRepositories;

namespace TicketingSystem.Database.Repositories
{
    public class PriorityRepository : IPriorityRepository
    {
        private readonly DatabaseContext _context;

        public PriorityRepository(DatabaseContext context)
        {
            this._context = context;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public List<Priority> GetAll()
        {
            return _context.Priorities.ToList();
        }

        public Priority GetById(string id)
        {
            return _context.Priorities.Find(id);
        }

        public dynamic Add(Priority item)
        {
            dynamic context = new Dictionary<string, object>();
            _context.Priorities.Add(item);
            context["Output"] = item;

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
