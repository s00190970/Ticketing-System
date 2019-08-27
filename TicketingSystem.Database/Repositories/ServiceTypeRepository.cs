using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TicketingSystem.Database.Context;
using TicketingSystem.Database.Entities;
using TicketingSystem.Database.IRepositories;

namespace TicketingSystem.Database.Repositories
{
    public class ServiceTypeRepository : IServiceTypeRepository
    {
        private readonly DatabaseContext _context;
        public ServiceTypeRepository(DatabaseContext context)
        {
            _context = context;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public List<ServiceType> GetAll()
        {
            return _context.ServiceTypes.ToList();
        }

        public ServiceType GetById(string id)
        {
            return _context.ServiceTypes.Find(id);
        }

        public ServiceType GetByName(string name)
        {
            return _context.ServiceTypes.FirstOrDefault(s => s.Name == name);
        }

        public dynamic Add(ServiceType item)
        {
            dynamic context = new Dictionary<string, object>();
            _context.ServiceTypes.Add(item);
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
