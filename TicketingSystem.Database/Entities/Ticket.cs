using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketingSystem.Database.Entities
{
    public class Ticket
    {
        public string Id { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public string CustomerName { get; set; }
        public int PriorityId { get; set; }
        public DateTime OpenDateTime { get; set; }
        public DateTime CloseDateTime { get; set; }
        public TicketType TicketType { get; set; }
        public ServiceType ServiceType { get; set; }
        public Status Status { get; set; }
        public Priority Priority { get; set; }
    }
}
