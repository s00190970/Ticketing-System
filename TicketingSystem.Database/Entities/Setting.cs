using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketingSystem.Database.Entities
{
    public class Setting
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool Enabled { get; set; }
    }
}
