using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VideoGameOrderSystem.App.Models;
using VideoGameOrderSystem.App.Controllers;
using VideoGameOrderSystem.App.Models.ViewModels;

namespace VideoGameOrderSystem.App.Controllers
{
    public class HomeController : Controller
    {

        public DataAccess.Repos.IOrderRepository OrderRepo { get; }

        public HomeController(DataAccess.Repos.IOrderRepository oRepo)
        {
            OrderRepo = oRepo;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginModel login)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index", "Order", new {@id=login.Id});
            }
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
