using System;
using System.Collections.Generic;
using System.Text;

namespace TicketingSystem.Core.DTOs
{
    public class UserDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public ICollection<TicketDto> Tickets { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
    }
}
