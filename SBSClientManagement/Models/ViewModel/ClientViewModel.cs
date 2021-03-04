using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SBSClientManagement.Models.ViewModel
{
    public class ViewClientViewModel
    {
        public int Id { get; set; }
        public DateTime DateCreate { get; set; }
        public string Name { get; set; }
        public IEnumerable<ViewServer> Servers { get; set; }
        public IEnumerable<ViewVpn> VPN { get; set; }
        public IEnumerable<ViewSQLServer> SQLServers { get; set; }
    }

    public class ViewServer
    {
        public int Id { get; set; }
        public DateTime DateCreate { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public Category Categories { get; set; }
        public int ClientId { get; set; }

        public enum Category
        {
            Test,
            Live,
            Devlopment
        }
    }

    public class ViewVpn
    {
        public int Id { get; set; }
        public DateTime DateCreate { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int ClientId { get; set; }
    }

    public class ViewSQLServer
    {
        public int Id { get; set; }
        public DateTime DateCreate { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string InstanceName { get; set; }
        public string ServerId { get; set; }
        public int ClientId { get; set; }
        public bool HasServer { get; set; } = false;
    }
}
