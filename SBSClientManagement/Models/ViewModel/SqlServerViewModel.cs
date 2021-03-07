using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SBSClientManagement.Models.ViewModel
{
    public class ViewSqlServerViewModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string InstanceName { get; set; }
        public int ServerId { get; set; }
        public string ServerName { get; set; }
        public int ClientId { get; set; }
        public string ClientName { get; set; }

    }

    public class CreateSqlServerViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string InstanceName { get; set; }
        [Required]
        public int ServerId { get; set; }
        public IEnumerable<CreateSqlServerServerViewModel> Servers { get; set; }
        public int ClientId { get; set; }
        public IEnumerable<CreateSqlServerClientViewModel> Clients { get; set; }
    }

    public class CreateSqlServerServerViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class CreateSqlServerClientViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class EditSqlServerViewModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string InstanceName { get; set; }
        [Required]
        public int ServerId { get; set; }
        public IEnumerable<CreateSqlServerServerViewModel> Servers { get; set; }
        public CreateSqlServerServerViewModel SelectedServer { get; set; }
        public int ClientId { get; set; }
        public IEnumerable<CreateSqlServerClientViewModel> Clients { get; set; }
        public CreateSqlServerClientViewModel SelectedClient { get; set; }
    }

    public class DeleteSqlServerViewModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string InstanceName { get; set; }
        public string ServerName { get; set; }
        public string ClientName { get; set; }

    }
}
