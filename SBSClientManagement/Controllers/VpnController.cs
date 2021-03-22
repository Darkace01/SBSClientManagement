using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    public class VpnController : Controller
    {
        private readonly IVpnRepo _vpnRepo;
        private readonly IMapper _mapper;
        private readonly IClientRepo _clientRepo;

        public VpnController(IVpnRepo vpnRepo, IMapper mapper, IClientRepo clientRepo)
        {
            _vpnRepo = vpnRepo;
            _mapper = mapper;
            _clientRepo = clientRepo;
        }

        // GET: VpnController
        public IActionResult Index()
        {
            var _vpns = _vpnRepo.GetVpns();
            var _clients = _clientRepo.GetClients();

            List<ViewVpnViewModel> vpns = _mapper.Map<IEnumerable<ViewVpnViewModel>>(_vpns).ToList();
            foreach (var item in vpns)
            {
                item.ClientName = _clients.Where(c => c.Id == item.ClientId).FirstOrDefault().Name;
            }

            return View(vpns);
        }

        public IActionResult Search(string searchString)
        {
            var _vpns = _vpnRepo.GetVpns();
            var _clients = _clientRepo.GetClients();

            List<ViewVpnViewModel> vpns = _mapper.Map<IEnumerable<ViewVpnViewModel>>(_vpns).ToList();
            foreach (var item in vpns)
            {
                item.ClientName = _clients.Where(c => c.Id == item.ClientId).FirstOrDefault().Name;
            }
            if (!String.IsNullOrEmpty(searchString))
                vpns = vpns.Where(c =>
                                    c.Name.ToLower().Contains(searchString.ToLower())
                                    || c.ClientName.ToLower().Contains(searchString.ToLower())
                                    || c.Username.ToLower().Contains(searchString.ToLower())).ToList();
            return Json(vpns);
        }

        public IActionResult VpnExist(string vpnName)
        {
            if (String.IsNullOrEmpty(vpnName))
                return Json(true);
            bool exist = _vpnRepo.IsVpnExist(vpnName.ToLower());
            return Json(exist);
        }

        // GET: VpnController/Details/5
        public IActionResult Details(int id)
        {
            if (id < 0)
                return NotFound();
            var _vpn = _vpnRepo.GetById(id);
            if (_vpn == null)
                return NotFound();
            var _client = _clientRepo.GetById(_vpn.ClientId);

            ViewVpnViewModel vpn = _mapper.Map<ViewVpnViewModel>(_vpn);
            vpn.ClientName = _client.Name;

            return PartialView("_Details", vpn);
        }

        // GET: VpnController/Create
        public IActionResult Create()
        {
            var client = _mapper.Map<IEnumerable<CreateVpnClientViewModel>>(_clientRepo.GetClients());
            var server = new CreateVpnViewModel
            {
                Clients = client
            };
            return PartialView("_Create", server);
        }

        // POST: VpnController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateVpnViewModel _vpn)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(_vpn);
                _vpn.Password = EncryptionHelper.EncryptStringAES(_vpn.Password);
                var vpn = _mapper.Map<Vpn>(_vpn);

                _vpnRepo.Create(vpn);
                return Json(Ok());
            }
            catch (Exception ex)
            {
                return Json(BadRequest("Error Saving Vpn" + ex));
            }
        }

        // GET: VpnController/Edit/5
        public IActionResult Edit(int id)
        {
            if (id < 0)
                return NotFound();

            var _vpn = _vpnRepo.GetById(id);
            if (_vpn == null)
                return NotFound();
            EditVpnViewModel vpn = _mapper.Map<EditVpnViewModel>(_vpn);
            var selectedClient = _clientRepo.GetById(_vpn.ClientId);
            vpn.SelectedClient = _mapper.Map<CreateVpnClientViewModel>(selectedClient);

            vpn.Clients = _mapper.Map<IEnumerable<CreateVpnClientViewModel>>(_clientRepo.GetClients().Where(c => c.Id != selectedClient.Id));

            return PartialView("_Edit", vpn);
        }

        // POST: VpnController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EditVpnViewModel _vpn)
        {
            try
            {
                if (!ModelState.IsValid)
                    return PartialView("_Edit", _vpn);
                _vpn.Password = EncryptionHelper.EncryptStringAES(_vpn.Password);
                var vpn = _mapper.Map<Vpn>(_vpn);

                _vpnRepo.Update(vpn);

                return Json(Ok());
            }
            catch (Exception ex)
            {
                return Json(BadRequest("Error Occured while Updating VPN Details " + ex.Message));
            }
        }

        // GET: VpnController/Delete/5
        public IActionResult DeleteConfirmation(int id)
        {
            if (id < 0)
                return NotFound();
            var _vpn = _vpnRepo.GetById(id);
            if (_vpn == null)
                return NotFound();

            DeleteVpnViewModel vpn = _mapper.Map<DeleteVpnViewModel>(_vpn);

            return PartialView("_DeleteConfirmation", vpn);
        }

        // POST: VpnController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(DeleteVpnViewModel _vpn)
        {
            try
            {
                if (_vpn == null)
                    return NotFound();
                var vpn = _vpnRepo.GetById(_vpn.Id);
                if (vpn == null)
                    return NotFound();

                _vpnRepo.Delete(vpn);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
