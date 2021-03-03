using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SBSClientManagement.DTO
{
    public class Client : Entity
    {
        public string Name { get; set; }
        public ICollection<Server> Servers { get; set; }
        public ICollection<Vpn> VPN { get; set; }
        public ICollection<SQLServer> SQLServers { get; set; }
    }
}
