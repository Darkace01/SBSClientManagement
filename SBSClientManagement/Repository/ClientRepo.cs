using Microsoft.EntityFrameworkCore;
using SBSClientManagement.Data;
using SBSClientManagement.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SBSClientManagement.Repository
{
    public class ClientRepo : IClientRepo
    {
        private readonly ApplicationDbContext _ctx;
        public ClientRepo(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        public void Create(Client client)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));

            _ctx.Clients.Add(client);
            SaveChanges();
        }

        public Client GetById(int Id)
        {
            if (Id <= 0)
                throw new ArgumentNullException("Client");
            return _ctx.Clients.Where(c => c.Id == Id).FirstOrDefault();
        }

        public Client GetByIdWithRelationship(int Id)
        {
            if (Id <= 0)
                throw new ArgumentNullException("Client");
            return _ctx.Clients.Where(c => c.Id == Id)
                .Include(x => x.Servers)
                .Include(x => x.SQLServers)
                .Include(x => x.VPN)
                .FirstOrDefault();
        }

        public IEnumerable<Client> GetClients()
        {
            var clients = _ctx.Clients.ToList();
            return clients;
        }

        public IEnumerable<Client> GetClientsWithRelationship()
        {
            var clients = _ctx.Clients
                .Include(x => x.Servers)
                .Include(x => x.SQLServers)
                .Include(x => x.VPN)
                .ToList();
            return clients;
        }

        public void Update(Client client)
        {
            _ctx.Clients.Update(client);
            SaveChanges();
        }

        public void Delete(Client client)
        {
            _ctx.Clients.Remove(client);

            SaveChanges();
        }

        public bool SaveChanges()
        {
            return (_ctx.SaveChanges() >= 0);
        }

    }
}
