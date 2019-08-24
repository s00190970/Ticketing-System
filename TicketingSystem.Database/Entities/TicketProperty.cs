using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketingSystem.Database.Entities
{
    public class TicketProperty
    {
        public TicketProperty(string name)
        {
            Name = name;
        }

        public TicketProperty()
        {
        }

        public string Id { get; set; }
        public string Name { get; set; }
    }
}
