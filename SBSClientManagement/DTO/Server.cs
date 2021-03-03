using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SBSClientManagement.DTO
{
    public class Server : Entity
    {
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
}
