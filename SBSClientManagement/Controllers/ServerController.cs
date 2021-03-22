using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SBSClientManagement.DTO;
using SBSClientManagement.Helpers;
using SBSClientManagement.Models.ViewModel;
using SBSClientManagement.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SBSClientManagement.Controllers
{
    [Authorize]
    public class ServerController : Controller
    {
        private readonly IServersRepo _serverRepo;
        private readonly IClientRepo _clientRepo;
        private readonly IMapper _mapper;

        public ServerController(IServersRepo serverRepo, IMapper mapper, IClientRepo clientRepo)
        {
            _serverRepo = serverRepo;
            _mapper = mapper;
            _clientRepo = clientRepo;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Search(string searchString)
        {
            var _servers = _serverRepo.GetServers();
            var _clients = _clientRepo.GetClients();
            List<ViewServerViewModel> servers = _mapper.Map<IEnumerable<ViewServerViewModel>>(_servers).ToList();
            foreach (var item in servers)
            {
                item.ClientName = _clients.Where(c => c.Id == item.ClientId).FirstOrDefault().Name;
            }
            if (!String.IsNullOrEmpty(searchString))
                servers = servers.Where(
                            s => s.Name.ToLower().Contains(searchString.ToLower())
                            || s.Username.ToLower().Contains(searchString.ToLower())
                            || s.ClientName.ToLower().Contains(searchString.ToLower())
                            || s.Categories.ToString().ToLower().Contains(searchString.ToLower()))
                            .ToList();

            return Json(servers);
        }

        public IActionResult Details(int id)
        {
            if (id < 0)
                return NotFound();
            var _server = _serverRepo.GetById(id);
            if (_server == null)
                return NotFound();
            var _client = _clientRepo.GetById(_server.ClientId);
            ViewServerViewModel server = _mapper.Map<ViewServerViewModel>(_server);

            server.ClientName = _client.Name;

            return PartialView("_Details", server);
        }

        public IActionResult ServerExist(string serverName)
        {
            if (String.IsNullOrEmpty(serverName))
                return Json(true);
            bool exist = _serverRepo.IsServerExist(serverName.ToLower());
            return Json(exist);
        }

        public IActionResult Create()
        {
            var client = _mapper.Map<IEnumerable<CreateClientServerClientViewModel>>(_clientRepo.GetClients());
            var server = new CreateServerViewModel
            {
                Client = client
            };
            return PartialView("_Create", server);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateServerViewModel _serverModel)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(_serverModel);
                _serverModel.Password = EncryptionHelper.EncryptStringAES(_serverModel.Password);
                var serverModel = _mapper.Map<Server>(_serverModel);

                _serverRepo.Create(serverModel);

                return Json(Ok());
            }
            catch (Exception ex)
            {
                return Json(BadRequest("Error Saving Server" + ex));
            }
        }

        public IActionResult Edit(int id)
        {
            if (id < 0)
                return NotFound();
            var _server = _serverRepo.GetById(id);
            if (_server == null)
                return NotFound();
            _server.Password = EncryptionHelper.DecryptStringAES(_server.Password);
            EditServerViewModel server = _mapper.Map<EditServerViewModel>(_server);
            var selectedClient = _clientRepo.GetById(_server.ClientId);
            server.SelectedClient = _mapper.Map<CreateClientServerClientViewModel>(selectedClient);

            server.SelectedCategory = _server.Categories.ToString();
            server.Clients = _mapper.Map<IEnumerable<CreateClientServerClientViewModel>>(_clientRepo.GetClients()
                .Where(c => c.Id != selectedClient.Id));

            return PartialView("_Edit", server);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EditServerViewModel _serverModel)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(_serverModel);
                _serverModel.Password = EncryptionHelper.EncryptStringAES(_serverModel.Password);
                var server = _mapper.Map<Server>(_serverModel);

                _serverRepo.Update(server);

                return Json(Ok());
            }
            catch (Exception ex)
            {

                return Json(BadRequest("Error Updating Server " + ex));
            }
        }

        public IActionResult DeleteConfirmation(int id)
        {
            if (id < 0)
                return NotFound();
            var _server = _serverRepo.GetById(id);
            if (_server == null)
                return NotFound();
            var _client = _clientRepo.GetById(_server.ClientId);
            DeleteServerViewModel server = _mapper.Map<DeleteServerViewModel>(_server);
            server.ClientName = _client.Name;
            return PartialView("_DeleteConfirmation", server);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(DeleteServerViewModel _server)
        {
            try
            {
                if (_server == null)
                    return NotFound();
                var server = _serverRepo.GetById(_server.Id);
                if (server == null)
                    return NotFound();

                _serverRepo.Delete(server);
                return RedirectToAction(nameof(Index));
            }
            catch
            {

                return RedirectToAction(nameof(Index));
            }
        }

    }
}
