using AutoMapper;
using SBSClientManagement.DTO;
using SBSClientManagement.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SBSClientManagement.Profiles
{
    public class VpnProfile : Profile
    {
        public VpnProfile()
        {
            CreateMap<Vpn, ViewVpnViewModel>();
            CreateMap<Client, CreateVpnClientViewModel>();
            CreateMap<CreateVpnViewModel, Vpn>();
            CreateMap<Vpn, EditVpnViewModel>();
            CreateMap<EditVpnViewModel, Vpn>();
            CreateMap<Vpn, DeleteVpnViewModel>();

        }
    }
}
