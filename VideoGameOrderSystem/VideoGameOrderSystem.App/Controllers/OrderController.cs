using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VideoGameOrderSystem.DataAccess;
using Lib = VideoGameOrderSystem.Library;

namespace VideoGameOrderSystem.App.Controllers
{
    [Route("Order/{id?}")]
    public class OrderController : Controller
    {
        public DataAccess.Repos.ICustomerRepository CustomerRepo { get; }
        public DataAccess.Repos.IStoreRepository StoreRepo { get; }
        public DataAccess.Repos.IOrderRepository OrderRepo { get; }

        public OrderController(DataAccess.Repos.ICustomerRepository cRepo,
                               DataAccess.Repos.IStoreRepository sRepo,
                               DataAccess.Repos.IOrderRepository oRepo)
        {
            CustomerRepo = cRepo;
            StoreRepo = sRepo;
            OrderRepo = oRepo;
        }

        // GET: Order
        public ActionResult Index([FromQuery]string search = "")
        {
            //IEnumerable<Lib.Product> libProducts = StoreRepo.GetInventoryProducts();
            //IEnumerable<Customer> webCustomers = libCustomers.Select(x => new Customer
            //{
            //    Id = x.Id,
            //    FirstName = x.FirstName,
            //    LastName = x.LastName,
            //    Birthday = x.Birthday
            //});
            //return View(webCustomers);
            return View();
        }

        // GET: Order/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Order/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Order/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Order/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Order/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Order/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Order/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}