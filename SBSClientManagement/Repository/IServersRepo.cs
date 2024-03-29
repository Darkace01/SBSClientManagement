﻿using SBSClientManagement.DTO;
using System.Collections.Generic;

namespace SBSClientManagement.Repository
{
    public interface IServersRepo
    {
        void Create(Server server);
        Server GetByClientId(int clientId);
        Server GetById(int Id);
        IEnumerable<Server> GetServers();
        void Update(Server server);
        void Delete(Server server);
        IEnumerable<Server> GetClientServers(int clientId);
        bool IsServerExist(string serverName);
    }
}