using SBSClientManagement.DTO;
using System.Collections.Generic;

namespace SBSClientManagement.Repository
{
    public interface IClientRepo
    {
        void Create(Client client);
        Client GetById(int Id);
        IEnumerable<Client> GetClients();
        void Update(Client client);
        void Delete(Client client);
        Client GetByIdWithRelationship(int Id);
        bool IsClientExist(string clientName);
        IEnumerable<Client> GetClientsWithRelationship();
    }
}