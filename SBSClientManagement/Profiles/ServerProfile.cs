using AutoMapper;
using SBSClientManagement.DTO;
using SBSClientManagement.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SBSClientManagement.Profiles
{
    public class ServerProfile : Profile
    {
        public ServerProfile()
        {
            CreateMap<Server, ViewServerViewModel>();
            CreateMap<CreateClientServerViewModel, Server>();
            CreateMap<Client, CreateClientServerViewModel>();
            CreateMap<CreateServerViewModel, Server>();
            CreateMap<Server, EditServerViewModel>();
            CreateMap<EditServerViewModel, Server>();
            CreateMap<Server, DeleteServerViewModel>();
        }
    }
}
