using AutoMapper;
using SBSClientManagement.DTO;
using SBSClientManagement.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SBSClientManagement.Profiles
{
    public class ClientProfile : Profile
    {
        public ClientProfile()
        {
            CreateMap<Client, ViewClientViewModel>();
            CreateMap<Server, ViewServer>();
            CreateMap<Vpn, ViewVpn>();
            CreateMap<SQLServer, ViewSQLServer>();
        }
    }

}
