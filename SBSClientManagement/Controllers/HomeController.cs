using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SBSClientManagement.Models;
using SBSClientManagement.Models.ViewModel;
using SBSClientManagement.Repository;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SBSClientManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISqlServerRepo _sqlServerRepo;
        private readonly IClientRepo _clientRepo;
        private readonly IServersRepo _serverRepo;
        private readonly IMapper _mapper;
        private readonly IVpnRepo _vpnRepo;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IVpnRepo vpnRepo, IServersRepo serverRepo, IClientRepo clientRepo, ISqlServerRepo sqlServerRepo, IMapper mapper)
        {
            _logger = logger;
            _vpnRepo = vpnRepo;
            _serverRepo = serverRepo;
            _clientRepo = clientRepo;
            _sqlServerRepo = sqlServerRepo;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Dashboard()
        {
            IEnumerable<ViewDashboardSqlServerViewModel> _sqlServers = _mapper.Map<IEnumerable<ViewDashboardSqlServerViewModel>>(_sqlServerRepo.GetSQLServers());
            IEnumerable<ViewDashboardClientViewModel> _clients = _mapper.Map<IEnumerable<ViewDashboardClientViewModel>>(_clientRepo.GetClients());
            IEnumerable<ViewDashboardServerViewModel> _servers = _mapper.Map<IEnumerable<ViewDashboardServerViewModel>>(_serverRepo.GetServers());
            IEnumerable<ViewDashboardVpnViewModel> _vpns = _mapper.Map<IEnumerable<ViewDashboardVpnViewModel>>(_vpnRepo.GetVpns());

            var dashboardViewModel = new ViewDashboardViewModel();
            dashboardViewModel.TotalClients = _clients.Count();
            dashboardViewModel.TotalServers = _servers.Count();
            dashboardViewModel.TotalSqlServer = _sqlServers.Count();
            dashboardViewModel.TotalVpns = _vpns.Count();
            foreach (var item in _servers)
            {
                item.ClientName = _clients.Where(c => c.Id == item.ClientId).FirstOrDefault().Name;
            }

            foreach (var item in _vpns)
            {
                item.ClientName = _clients.Where(c => c.Id == item.ClientId).FirstOrDefault().Name;
            }

            foreach (var item in _sqlServers)
            {
                item.ClientName = _clients.Where(c => c.Id == item.ClientId).FirstOrDefault().Name;
            }
            dashboardViewModel.Clients = _clients.Take(5);
            dashboardViewModel.Servers = _servers.Take(5);
            dashboardViewModel.SqlServers = _sqlServers.Take(5);
            dashboardViewModel.Vpns = _vpns.Take(5);
            return View(dashboardViewModel);
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
