using AutoMapper;
using SBSClientManagement.DTO;
using SBSClientManagement.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SBSClientManagement.Profiles
{
    public class DashboardProfile : Profile
    {
        public DashboardProfile()
        {
            CreateMap<Client, ViewDashboardClientViewModel>();
            CreateMap<Server, ViewDashboardServerViewModel>();
            CreateMap<Vpn, ViewDashboardVpnViewModel>();
            CreateMap<SQLServer, ViewDashboardSqlServerViewModel>();
        }
    }
}
