using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TicketingSystem.Database.Context;
using TicketingSystem.Database.Entities;
using TicketingSystem.Database.IRepositories;

namespace TicketingSystem.Database.Repositories
{
    public class TicketTypeRepository : ITicketTypeRepository
    {
        private readonly DatabaseContext _context;
        public TicketTypeRepository(DatabaseContext context)
        {
            this._context = context;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public List<TicketType> GetAll()
        {
            return _context.TicketTypes.ToList();
        }

        public TicketType GetById(string id)
        {
            return _context.TicketTypes.Find(id);
        }

        public TicketType GetByName(string name)
        {
            return _context.TicketTypes.FirstOrDefault(t => t.Name == name);
        }

        public dynamic Add(TicketType item)
        {
            dynamic context = new Dictionary<string, object>();
            _context.TicketTypes.Add(item);
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
