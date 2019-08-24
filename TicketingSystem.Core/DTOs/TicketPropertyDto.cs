using System;
using System.Collections.Generic;
using System.Text;

namespace TicketingSystem.Core.DTOs
{
    public class TicketPropertyDto
    {
        public TicketPropertyDto(string name)
        {
            Name = name;
        }

        public TicketPropertyDto()
        {
        }

        public string Id { get; set; }
        public string Name { get; set; }
    }
}
