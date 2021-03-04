using SBSClientManagement.DTO;
using System.Collections.Generic;

namespace SBSClientManagement.Repository
{
    public interface IClientRepo
    {
        void Create(Client client);
        Client GetById(int Id);
        IEnumerable<Client> GetClients();
        void Update();
        Client GetByIdWithRelationship(int Id);
        IEnumerable<Client> GetClientsWithRelationship();
    }
}