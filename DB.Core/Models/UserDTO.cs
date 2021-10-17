using System;
using System.Collections.Generic;
using System.Text;

namespace DB.Core.Models
{
    public class UserDTO
    {
        public int id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
