using SBSClientManagement.Data;
using SBSClientManagement.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SBSClientManagement.Repository
{
    public class SqlServerRepo : ISqlServerRepo
    {
        private readonly ApplicationDbContext _ctx;
        public SqlServerRepo(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        public void Create(SQLServer sqlServer)
        {
            if (sqlServer == null)
                throw new ArgumentNullException(nameof(sqlServer));

            _ctx.SQLServers.Add(sqlServer);
            SaveChanges();
        }

        public SQLServer GetById(int Id)
        {
            if (Id <= 0)
                throw new ArgumentNullException("SQLServer");
            return _ctx.SQLServers.Where(c => c.Id == Id).FirstOrDefault();
        }

        public SQLServer GetByClientId(int clientId)
        {
            if (clientId > 0)
                throw new ArgumentNullException("SQLServer");
            return _ctx.SQLServers.Where(c => c.ClientId == clientId).FirstOrDefault();
        }

        public IEnumerable<SQLServer> GetSQLServers()
        {
            var sqlServers = _ctx.SQLServers.ToList();
            return sqlServers;
        }

        public void Update(SQLServer sqlserver)
        {
            _ctx.SQLServers.Update(sqlserver);

            SaveChanges();
        }

        public bool SaveChanges()
        {
            return (_ctx.SaveChanges() >= 0);
        }

        public void Delete(SQLServer sqlserver)
        {
            _ctx.SQLServers.Remove(sqlserver);
            SaveChanges();
        }
    }
}
