using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ChatApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using ChatApp.Mappings;
using ChatApp.Repositories.Interfaces;

namespace ChatApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<ChatUser> _userManager;
        private readonly ILogger<HomeController> _logger;        

        public HomeController(ILogger<HomeController> logger, UserManager<ChatUser> userManager)
        {
            _logger = logger;
            _userManager = userManager;
        }

        [Authorize]
        public IActionResult Index()
        {
            var signedInUsers = ConnectionMapping<string>.GetAllUserNames();

            ViewBag.SignedInUsers = signedInUsers;

            return View();
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
