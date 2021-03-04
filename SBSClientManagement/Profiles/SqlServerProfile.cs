using AutoMapper;
using SBSClientManagement.DTO;
using SBSClientManagement.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SBSClientManagement.Profiles
{
    public class SqlServerProfile : Profile
    {
        public SqlServerProfile()
        {
            CreateMap<SQLServer, ViewSqlServerViewModel>();
            CreateMap<Client, CreateSqlServerClientViewModel>();
            CreateMap<Server, CreateSqlServerServerViewModel>();
            CreateMap<CreateSqlServerViewModel, SQLServer>();
            CreateMap<SQLServer, EditSqlServerViewModel>();
            CreateMap<EditSqlServerViewModel, SQLServer>();
            CreateMap<SQLServer, DeleteSqlServerViewModel>();
        }
    }
}
