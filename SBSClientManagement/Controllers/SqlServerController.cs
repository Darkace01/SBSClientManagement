using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SBSClientManagement.DTO;
using SBSClientManagement.Models.ViewModel;
using SBSClientManagement.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SBSClientManagement.Controllers
{
    [Authorize]
    public class SqlServerController : Controller
    { 
        private readonly ISqlServerRepo _sqlServerRepo;
        private readonly IClientRepo _clientRepo;
        private readonly IServersRepo _serverRepo;
        private readonly IMapper _mapper;

        public SqlServerController(IMapper mapper, IClientRepo clientRepo, ISqlServerRepo sqlServerRepo, IServersRepo serverRepo)
        {
            _mapper = mapper;
            _clientRepo = clientRepo;
            _sqlServerRepo = sqlServerRepo;
            _serverRepo = serverRepo;
        }

        // GET: SqlServerController
        public ActionResult Index(string searchString)
        {
            var _sqlServers = _sqlServerRepo.GetSQLServers();
            var _clients = _clientRepo.GetClients();
            var _servers = _serverRepo.GetServers();
            List<ViewSqlServerViewModel> sqlServers = _mapper.Map<IEnumerable<ViewSqlServerViewModel>>(_sqlServers).ToList();
            foreach (var item in sqlServers)
            {
                item.ClientName = _clients.Where(c => c.Id == item.ClientId).FirstOrDefault().Name;
                item.ServerName = _servers.Where(c => c.Id == item.ServerId).FirstOrDefault().Name;
            }
            if (!String.IsNullOrEmpty(searchString))
                sqlServers = sqlServers.Where(s => 
                                    s.ServerName.ToLower().Contains(searchString.ToLower())
                                    || s.InstanceName.ToLower().Contains(searchString.ToLower())
                                    || s.Username.ToLower().Contains(searchString.ToLower())
                                    || s.ClientName.ToLower().Contains(searchString.ToLower())).ToList();
            return View(sqlServers);
        }

        public ActionResult GetClientByServerId(int clientId)
        {
            if (clientId < 0)
                return NotFound();
            var _servers = _serverRepo.GetClientServers(clientId);
            List<SqlServerServerViewModel> servers = _mapper.Map<IEnumerable<SqlServerServerViewModel>>(_servers).ToList();
            return Json(servers);
        }

        // GET: SqlServerController/Details/5
        public ActionResult Details(int id)
        {
            if (id < 0)
                return NotFound();
            var _sqlServer = _sqlServerRepo.GetById(id);
            if (_sqlServer == null)
                return NotFound();
            var _server = _serverRepo.GetById(_sqlServer.ServerId);
            var _client = _clientRepo.GetById(_sqlServer.ClientId);

            ViewSqlServerViewModel sqlServer = _mapper.Map<ViewSqlServerViewModel>(_sqlServer);
            sqlServer.ClientName = _client.Name;
            sqlServer.ServerName = _server.Name;
            return PartialView("_Details", sqlServer);
        }

        // GET: SqlServerController/Create
        public ActionResult Create()
        {
            var client = _mapper.Map<IEnumerable<CreateSqlServerClientViewModel>>(_clientRepo.GetClients());
            var server = _mapper.Map<IEnumerable<CreateSqlServerServerViewModel>>(_serverRepo.GetServers());
            var sqlServer = new CreateSqlServerViewModel();
            sqlServer.Clients = client;
            sqlServer.Servers = server;

            return PartialView("_Create", sqlServer);
        }

        // POST: SqlServerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateSqlServerViewModel _sqlServer)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(_sqlServer);
                var sqlServer = _mapper.Map<SQLServer>(_sqlServer);

                _sqlServerRepo.Create(sqlServer);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: SqlServerController/Edit/5
        public ActionResult Edit(int id)
        {
            if (id < 0)
                return NotFound();
            var _sqlServer = _sqlServerRepo.GetById(id);
            if (_sqlServer == null)
                return NotFound();
            EditSqlServerViewModel sqlServer = _mapper.Map<EditSqlServerViewModel>(_sqlServer);
            var selectedClient = _clientRepo.GetById(_sqlServer.ClientId);
            var selectedServer = _serverRepo.GetById(_sqlServer.ServerId);

            sqlServer.SelectedClient = _mapper.Map<CreateSqlServerClientViewModel>(selectedClient);
            sqlServer.SelectedServer = _mapper.Map<CreateSqlServerServerViewModel>(selectedServer);

            sqlServer.Clients = _mapper.Map<IEnumerable<CreateSqlServerClientViewModel>>(_clientRepo.GetClients().Where(c => c.Id != selectedClient.Id));
            sqlServer.Servers = _mapper.Map< IEnumerable<CreateSqlServerServerViewModel>>(_serverRepo.GetServers().Where(s => s.Id != selectedServer.Id));

            return PartialView("_Edit", sqlServer);
        }

        // POST: SqlServerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditSqlServerViewModel _sqlServer)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(_sqlServer);

                var sqlServer = _mapper.Map<SQLServer>(_sqlServer);

                _sqlServerRepo.Update(sqlServer);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: SqlServerController/Delete/5
        public ActionResult DeleteConfirmation(int id)
        {
            if (id < 0)
                return NotFound();
            var _SqlServer = _sqlServerRepo.GetById(id);
            if (_SqlServer == null)
                return NotFound();
            var _server = _serverRepo.GetById(_SqlServer.ServerId);
            var _client = _clientRepo.GetById(_SqlServer.ClientId);

            DeleteSqlServerViewModel sqlServer = _mapper.Map<DeleteSqlServerViewModel>(_SqlServer);
            sqlServer.ClientName = _client.Name;
            sqlServer.ServerName = _server.Name;

            return PartialView("_DeleteConfirmation", sqlServer);
        }

        // POST: SqlServerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(DeleteSqlServerViewModel _sqlServer)
        {
            try
            {
                if (_sqlServer == null)
                    return NotFound();

                var sqlServer = _sqlServerRepo.GetById(_sqlServer.Id);
                if (sqlServer == null)
                    return NotFound();

                _sqlServerRepo.Delete(sqlServer);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
