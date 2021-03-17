using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SBSClientManagement.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace SBSClientManagement.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Server> Servers { get; set; }
        public DbSet<Vpn> Vpns { get; set; }
    }
}
