using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketingSystem.Core.DTOs
{
    public class TicketDto
    {
        public string Id { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public string CustomerName { get; set; }
        public string OpenDateTime { get; set; }
        public string CloseDateTime { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string TicketTypeName { get; set; }
        public string ServiceTypeName { get; set; }
        public string StatusName { get; set; }
        public string PriorityName { get; set; }
    }
}
