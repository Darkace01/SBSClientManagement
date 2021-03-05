using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SBSClientManagement.Models.ViewModel
{
    public class ViewDashboardViewModel
    {
        public IEnumerable<ViewDashboardClientViewModel> Clients { get; set; }
        public IEnumerable<ViewDashboardServerViewModel> Servers { get; set; }
        public IEnumerable<ViewDashboardVpnViewModel> Vpns { get; set; }
        public IEnumerable<ViewDashboardSqlServerViewModel> SqlServers { get; set; }
        public int TotalClients { get; set; }
        public int TotalServers { get; set; }
        public int TotalVpns { get; set; }
        public int TotalSqlServer { get; set; }

    }

    public class ViewDashboardClientViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class ViewDashboardServerViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public Category Categories { get; set; }
        public enum Category
        {
            Test,
            Live,
            Devlopment
        }
    }
    public class ViewDashboardVpnViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ClientId { get; set; }
        public string ClientName { get; set; }
    }

    public class ViewDashboardSqlServerViewModel
    {
        public int Id { get; set; }
        public string InstanceName { get; set; }
        public int ClientId { get; set; }
        public string ClientName { get; set; }
    }
}
