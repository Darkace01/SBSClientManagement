using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SBSClientManagement.DTO
{
    public class SQLServer : Entity
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string InstanceName { get; set; }
        public string ServerId { get; set; }
        public int ClientId { get; set; }
        public bool HasServer { get; set; } = false;
    }
}
