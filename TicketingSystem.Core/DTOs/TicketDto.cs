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
        public int PriorityId { get; set; }
        public DateTime OpenDateTime { get; set; }
        public DateTime CloseDateTime { get; set; }
        public TicketTypeDto TicketTypeDto { get; set; }
        public ServiceTypeDto ServiceTypeDto { get; set; }
        public StatusDto StatusDto { get; set; }
        public PriorityDto PriorityDto { get; set; }
    }
}
