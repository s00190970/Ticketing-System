using System;
using System.Collections.Generic;
using System.Text;
using TicketingSystem.Database.Entities;

namespace TicketingSystem.Database.IRepositories
{
    public interface ITicketTypeRepository : ITicketPropertyRepository<TicketType>
    {
    }
}
