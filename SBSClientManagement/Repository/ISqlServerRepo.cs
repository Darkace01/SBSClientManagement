using SBSClientManagement.DTO;
using System.Collections.Generic;

namespace SBSClientManagement.Repository
{
    public interface ISqlServerRepo
    {
        void Create(SQLServer sqlServer);
        SQLServer GetByClientId(int clientId);
        SQLServer GetById(int Id);
        IEnumerable<SQLServer> GetSQLServers();
        void Update(SQLServer sqlserver);
        void Delete(SQLServer sqlserver);
    }
}