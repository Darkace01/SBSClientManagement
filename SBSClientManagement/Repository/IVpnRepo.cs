using SBSClientManagement.DTO;
using System.Collections.Generic;

namespace SBSClientManagement.Repository
{
    public interface IVpnRepo
    {
        void Create(Vpn vpn);
        Vpn GetByClientId(int clientId);
        Vpn GetById(int Id);
        IEnumerable<Vpn> GetVpns();
        void Update(Vpn vpn);
        void Delete(Vpn vpn);
        bool IsVpnExist(string vpnName);
    }
}