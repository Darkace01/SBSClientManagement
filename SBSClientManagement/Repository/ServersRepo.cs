﻿using SBSClientManagement.Data;
using SBSClientManagement.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SBSClientManagement.Repository
{
    public class ServersRepo : IServersRepo
    {
        private readonly ApplicationDbContext _ctx;
        public ServersRepo(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        public void Create(Server server)
        {
            if (server == null)
                throw new ArgumentNullException(nameof(server));

            _ctx.Servers.Add(server);
            SaveChanges();
        }

        public Server GetById(int Id)
        {
            if (Id > 0)
                throw new ArgumentNullException("Server");
            return _ctx.Servers.Where(c => c.Id == Id).FirstOrDefault();
        }

        public Server GetByClientId(int clientId)
        {
            if (clientId > 0)
                throw new ArgumentNullException("Server");
            return _ctx.Servers.Where(c => c.ClientId == clientId).FirstOrDefault();
        }

        public IEnumerable<Server> GetServers()
        {
            var servers = _ctx.Servers.ToList();
            return servers;
        }

        public void Update()
        {
            SaveChanges();
        }

        public bool SaveChanges()
        {
            return (_ctx.SaveChanges() >= 0);
        }
    }
}