using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace TicketingSystem.Database.Entities
{
    public class User : IdentityUser
    {
        public ICollection<Ticket> Tickets { get; set; }
    }
}
