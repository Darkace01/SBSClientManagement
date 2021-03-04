using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SBSClientManagement.DTO;
using SBSClientManagement.Models.ViewModel;
using SBSClientManagement.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SBSClientManagement.Controllers
{
    public class ClientController : Controller
    {
        private readonly IClientRepo _clientRepo;
        private readonly IMapper _mapper;

        public ClientController(IClientRepo clientRepo, IMapper mapper)
        {
            _clientRepo = clientRepo;
            _mapper = mapper;
        }

        // GET: ClientController
        public ActionResult Index()
        {
            var _clients = _clientRepo.GetClients();
            List<ViewClientViewModel> clients = _mapper.Map<IEnumerable<ViewClientViewModel>>(_clients).ToList();
            return View(clients);
        }

        // GET: ClientController/Details/5
        public ActionResult Details(int id)
        {
            if (id < 0)
                return NotFound();
            var _client = _clientRepo.GetByIdWithRelationship(id);
            if (_client == null)
                return NotFound();
            ViewClientDetailViewModel client = _mapper.Map<ViewClientDetailViewModel>(_client);
            return View(client);
        }

        // GET: ClientController/Create
        public ActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateClientViewModel _clientModel)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(_clientModel);
                var clientModel = _mapper.Map<Client>(_clientModel);
                _clientRepo.Create(clientModel);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(_clientModel);
            }
        }

        // GET: ClientController/Edit/5
        public ActionResult Edit(int id)
        {
            if (id < 0)
                return NotFound();
            var _client = _clientRepo.GetById(id);
            if (_client == null)
                return NotFound();
            EditClientViewModel client = _mapper.Map<EditClientViewModel>(_client);

            return View(client);
        }

        // POST: ClientController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditClientViewModel _clientModel)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(_clientModel);

                var client = _mapper.Map<Client>(_clientModel);

                _clientRepo.Update(client);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult DeleteConfirmation(int id)
        {
            if (id < 0)
                return NotFound();
            var _client = _clientRepo.GetById(id);
            if (_client == null)
                return NotFound();
            DeleteClientViewModel client = _mapper.Map<DeleteClientViewModel>(_client);
            return View(client);
        }

        // POST: ClientController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(DeleteClientViewModel _client)
        {
            try
            {
                if (_client.Id < 0)
                    return NotFound();
                var client = _clientRepo.GetById(_client.Id);
                if (client == null)
                    return NotFound();

                _clientRepo.Delete(client);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
