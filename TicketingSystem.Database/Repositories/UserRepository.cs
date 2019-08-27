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
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseContext _context;

        public UserRepository(DatabaseContext context)
        {
            this._context = context;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public List<User> GetAll()
        {
            return _context.Users.ToList();
        }

        public User GetById(string id)
        {
            return _context.Users.Find(id);
        }

        public User GetByName(string name)
        {
            return _context.Users.FirstOrDefault(u => u.UserName == name);
        }

        public dynamic Add(User user)
        {
            dynamic context = new Dictionary<string, object>();
            _context.Users.Add(user);
            context["Output"] = user;

            return context;
        }

        public dynamic Edit(User user)
        {
            dynamic context = new Dictionary<string, object>();
            _context.Entry(user).State = EntityState.Modified;
            context["Output"] = _context.Users.Find(user.Id);

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
