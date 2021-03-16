using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
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
        public IActionResult Index(string searchString)
        {
            var _clients = _clientRepo.GetClients();
            
            List<ViewClientViewModel> clients = _mapper.Map<IEnumerable<ViewClientViewModel>>(_clients).ToList();
            if (!String.IsNullOrEmpty(searchString))
                clients = clients.Where(c => c.Name.ToLower().Contains(searchString.ToLower())).ToList();
            return View(clients);
        }

        // GET: ClientController/Details/5
        public IActionResult  Details(int id)
        {
            if (id < 0)
                return NotFound();
            var _client = _clientRepo.GetByIdWithRelationship(id);
            if (_client == null)
                return NotFound();
            ViewClientDetailViewModel client = _mapper.Map<ViewClientDetailViewModel>(_client);
            return PartialView("_Details",client);
        }

        public IActionResult ClientExist(string clientName)
        {
            if (String.IsNullOrEmpty(clientName))
                return Json(true);
            bool exist = _clientRepo.IsClientExist(clientName.ToLower());
            return Json(exist);
        }

        // GET: ClientController/Create
        public IActionResult  Create()
        {
            return PartialView("_Create");
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult  Create(CreateClientViewModel _clientModel)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(_clientModel);
                var clientModel = _mapper.Map<Client>(_clientModel);
                if (_clientRepo.IsClientExist(_clientModel.Name))
                    return Json(BadRequest("Client Already Exist"));
                _clientRepo.Create(clientModel);

                return Json(Ok());
            }
            catch(Exception ex)
            {
                return Json(BadRequest("Error Saving Client" + ex));
            }
        }

        // GET: ClientController/Edit/5
        public IActionResult  Edit(int id)
        {
            if (id < 0)
                return NotFound();
            var _client = _clientRepo.GetById(id);
            if (_client == null)
                return NotFound();
            EditClientViewModel client = _mapper.Map<EditClientViewModel>(_client);

            return PartialView("_Edit",client);
        }

        // POST: ClientController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult  Edit(EditClientViewModel _clientModel)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(_clientModel);

                var client = _mapper.Map<Client>(_clientModel);

                _clientRepo.Update(client);
                return Json(Ok());
            }
            catch(Exception ex)
            {
                return Json(BadRequest("Error updating" + ex));
            }
        }

        public IActionResult  DeleteConfirmation(int id)
        {
            if (id < 0)
                return NotFound();
            var _client = _clientRepo.GetById(id);
            if (_client == null)
                return NotFound();
            DeleteClientViewModel client = _mapper.Map<DeleteClientViewModel>(_client);
            return PartialView("_DeleteConfirmation", client);
        }

        // POST: ClientController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult  Delete(DeleteClientViewModel _client)
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
