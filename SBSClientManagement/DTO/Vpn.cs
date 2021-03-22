using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SBSClientManagement.DTO
{
    public class Vpn : Entity
    {
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string PasswordSalt { get; set; }
        public int ClientId { get; set; }
    }
}
