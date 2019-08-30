using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TicketingSystem.Database.Context;
using TicketingSystem.Database.Entities;
using TicketingSystem.Database.IRepositories;

namespace TicketingSystem.Database.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseContext _context;
        private readonly UserManager<User> _userManager;

        public UserRepository(DatabaseContext context, UserManager<User> userManager)
        {
            this._context = context;
            _userManager = userManager;
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
            User user = 
             _context.Users.Include(t => t.Tickets)
                .ThenInclude(p => p.Priority)
                .Include(t => t.Tickets)
                .ThenInclude(t => t.TicketType)
                .Include(t => t.Tickets)
                .ThenInclude(s => s.ServiceType)
                .Include(t => t.Tickets)
                .ThenInclude(s => s.Status)
                .FirstOrDefault(u => u.Id == id);

            return user;
        }

        public User GetByName(string name)
        {
            return _context.Users.Include(u=>u.Tickets).ThenInclude(p => p.Priority)
                .Include(t => t.Tickets)
                .ThenInclude(t => t.TicketType)
                .Include(t => t.Tickets)
                .ThenInclude(s => s.ServiceType)
                .Include(t => t.Tickets)
                .ThenInclude(s => s.Status)
                .FirstOrDefault(u => u.UserName == name);
        }

        public dynamic Add(User user)
        {
            dynamic context = new Dictionary<string, object>();
            _context.Users.Add(user);
            context["Output"] = user;

            return context;
        }

        public User Register(User user, string password)
        {
            return CreateUserAsync(user, password).Result;
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

        private async Task<User> CreateUserAsync(User user, string password)
        {
            PasswordHasher<User> ph = new PasswordHasher<User>();
            user.PasswordHash = ph.HashPassword(user, password);

            var userStore = new UserStore<User>(_context);
            var result = userStore.CreateAsync(user);

            if (result.Result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "User");
            }

            await _context.SaveChangesAsync();

            return await _userManager.FindByNameAsync(user.UserName);
        }
    }
}
