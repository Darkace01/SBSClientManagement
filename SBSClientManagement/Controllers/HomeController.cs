using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SBSClientManagement.Models;
using SBSClientManagement.Models.ViewModel;
using SBSClientManagement.Repository;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SBSClientManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly IClientRepo _clientRepo;
        private readonly IServersRepo _serverRepo;
        private readonly IMapper _mapper;
        private readonly IVpnRepo _vpnRepo;
        private readonly IUserRepo _userRepo;
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<IdentityUser> _userManager;
        public HomeController(ILogger<HomeController> logger, IVpnRepo vpnRepo, IServersRepo serverRepo, IClientRepo clientRepo, IMapper mapper, UserManager<IdentityUser> userManager, IUserRepo userRepo)
        {
            _logger = logger;
            _vpnRepo = vpnRepo;
            _serverRepo = serverRepo;
            _clientRepo = clientRepo;
            _mapper = mapper;
            _userManager = userManager;
            _userRepo = userRepo;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Dashboard()
        {
            IEnumerable<ViewDashboardClientViewModel> _clients = _mapper.Map<IEnumerable<ViewDashboardClientViewModel>>(_clientRepo.GetClients());
            IEnumerable<ViewDashboardServerViewModel> _servers = _mapper.Map<IEnumerable<ViewDashboardServerViewModel>>(_serverRepo.GetServers());
            IEnumerable<ViewDashboardVpnViewModel> _vpns = _mapper.Map<IEnumerable<ViewDashboardVpnViewModel>>(_vpnRepo.GetVpns());

            var dashboardViewModel = new ViewDashboardViewModel
            {
                TotalClients = _clients.Count(),
                TotalServers = _servers.Count(),
                TotalVpns = _vpns.Count()
            };
            foreach (var item in _servers)
            {
                item.ClientName = _clients.Where(c => c.Id == item.ClientId).FirstOrDefault().Name;
            }

            foreach (var item in _vpns)
            {
                item.ClientName = _clients.Where(c => c.Id == item.ClientId).FirstOrDefault().Name;
            }
            dashboardViewModel.Clients = _clients.Take(5);
            dashboardViewModel.Servers = _servers.Take(5);
            dashboardViewModel.Vpns = _vpns.Take(5);
            return View(dashboardViewModel);
        }

        public IActionResult ValidateUser()
        {
            return PartialView("_ValidateUser");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ValidateUser(UserViewModel model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _userRepo.GetUserById(userId);
            if (user == null)
                return NotFound();

            bool valid = await _userManager.CheckPasswordAsync(user, model.Password);
            return Json(Ok(valid));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
