using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SBSClientManagement.Models.ViewModel
{
    public class ViewClientViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class ViewClientDetailViewModel
    {
        public int Id { get; set; }
        public DateTime DateCreated { get; set; }
        public string Name { get; set; }
        public IEnumerable<ViewServer> Servers { get; set; }
        public IEnumerable<ViewVpn> VPN { get; set; }
    }

    public class ViewServer
    {
        public int Id { get; set; }
        public DateTime DateCreated { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public Category Categories { get; set; }
        public int ClientId { get; set; }

        public enum Category
        {
            Test,
            Live,
            DR
        }
    }

    public class ViewVpn
    {
        public int Id { get; set; }
        public DateTime DateCreated { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int ClientId { get; set; }
    }


    public class CreateClientViewModel
    {
        [Required]
        public string Name { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
    }

    public class EditClientViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }

    public class DeleteClientViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }

}
