﻿using SBSClientManagement.Data;
using SBSClientManagement.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SBSClientManagement.Repository
{
    public class VpnRepo : IVpnRepo
    {
        private readonly ApplicationDbContext _ctx;
        public VpnRepo(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        public void Create(Vpn vpn)
        {
            if (vpn == null)
                throw new ArgumentNullException(nameof(vpn));

            _ctx.Vpns.Add(vpn);
            SaveChanges();
        }

        public Vpn GetById(int Id)
        {
            if (Id <= 0)
                throw new ArgumentNullException("Vpn");
            return _ctx.Vpns.Where(c => c.Id == Id).FirstOrDefault();
        }

        public bool IsVpnExist(string vpnName)
        {
            if (String.IsNullOrEmpty(vpnName))
                throw new ArgumentException("Vpn is Null");
            var vpn = _ctx.Vpns.Where(c => c.Name.ToLower() == vpnName).FirstOrDefault();
            return vpn != null;
        }
        public Vpn GetByClientId(int clientId)
        {
            if (clientId > 0)
                throw new ArgumentNullException("Vpn");
            return _ctx.Vpns.Where(c => c.ClientId == clientId).FirstOrDefault();
        }

        public IEnumerable<Vpn> GetVpns()
        {
            var vpns = _ctx.Vpns.ToList();
            return vpns;
        }

        public void Update(Vpn vpn)
        {
            _ctx.Vpns.Update(vpn);
            SaveChanges();
        }

        public bool SaveChanges()
        {
            return (_ctx.SaveChanges() >= 0);
        }

        public void Delete(Vpn vpn)
        {
            _ctx.Vpns.Remove(vpn);
            SaveChanges();
        }
    }
}
