using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SBSClientManagement.Models.ViewModel
{
    public class ViewVpnViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ClientName { get; set; }
        public int ClientId { get; set; }
    }

    public class CreateVpnViewModel
    {
        public int Id { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public IEnumerable<CreateVpnClientViewModel> Clients { get; set; }
        public int ClientId { get; set; }
    }

    public class CreateVpnClientViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class EditVpnViewModel
    {
        public int Id { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public IEnumerable<CreateVpnClientViewModel> Clients { get; set; }
        public int ClientId { get; set; }
        public CreateVpnClientViewModel SelectedClient { get; set; }
    }

    public class DeleteVpnViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
