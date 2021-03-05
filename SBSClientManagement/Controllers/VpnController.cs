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
        public ActionResult Index()
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

        // GET: VpnController/Details/5
        public ActionResult Details(int id)
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
        public ActionResult Create()
        {
            var client = _mapper.Map<IEnumerable<CreateVpnClientViewModel>>(_clientRepo.GetClients());
            var server = new CreateVpnViewModel();
            server.Clients = client;
            return PartialView("_Create", server);
        }

        // POST: VpnController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateVpnViewModel _vpn)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(_vpn);
                var vpn = _mapper.Map<Vpn>(_vpn);

                _vpnRepo.Create(vpn);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: VpnController/Edit/5
        public ActionResult Edit(int id)
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
        public ActionResult Edit(EditVpnViewModel _vpn)
        {
            try
            {
                if (!ModelState.IsValid)
                    return PartialView("_Edit",_vpn);

                var vpn = _mapper.Map<Vpn>(_vpn);

                _vpnRepo.Update(vpn);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: VpnController/Delete/5
        public ActionResult DeleteConfirmation(int id)
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
        public ActionResult Delete(DeleteVpnViewModel _vpn)
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
