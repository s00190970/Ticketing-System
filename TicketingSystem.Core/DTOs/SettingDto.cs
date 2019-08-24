using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketingSystem.Core.DTOs
{
    public class SettingDto
    {
        public string Id { get; set; }
        public bool CanModifyTicketType { get; set; }
        public bool CanModifyCustomerName { get; set; }
        public bool CanModifyServiceType { get; set; }
    }
}
