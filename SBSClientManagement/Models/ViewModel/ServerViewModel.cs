using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SBSClientManagement.Models.ViewModel
{
    public class ViewServerViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public Category Categories { get; set; }
        public int ClientId { get; set; }
        public string ClientName { get; set; }

        public enum Category
        {
            Test,
            Live,
            DR
        }
    }

    public class CreateServerViewModel
    {
        public int Id { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public Category Categories { get; set; }
        public int ClientId { get; set; }
        public IEnumerable<CreateClientServerClientViewModel> Client { get; set; }

        public enum Category
        {
            Test,
            Live,
            DR
        }
    }

    public class CreateClientServerClientViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class EditServerViewModel
    {
        public int Id { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string SelectedCategory { get; set; }
        public Category Categories { get; set; }
        public int ClientId { get; set; }
        public IEnumerable<CreateClientServerClientViewModel> Clients { get; set; }
        public CreateClientServerClientViewModel SelectedClient { get; set; }

        public enum Category
        {
            Test,
            Live,
            DR
        }
    }

    public class DeleteServerViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public string Category { get; set; }
        public string ClientName { get; set; }
    }

}
