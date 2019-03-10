using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VideoGameOrderSystem.App.Models.ViewModels;
using VideoGameOrderSystem.Library;
using Lib = VideoGameOrderSystem.Library;

namespace VideoGameOrderSystem.App
{
    public class CustomerController : Controller
    {
        public DataAccess.Repos.ICustomerRepository CustomerRepo { get; }
        public DataAccess.Repos.IStoreRepository StoreRepo { get; }

        public CustomerController(DataAccess.Repos.ICustomerRepository cRepo,
                                  DataAccess.Repos.IStoreRepository sRepo)
        {
            CustomerRepo = cRepo;
            StoreRepo = sRepo;
        }

        // GET: Customer
        public ActionResult Index()
        {
            IEnumerable<Lib.Customer> libCustomers = CustomerRepo.GetAll();
            IEnumerable<Customer> webCustomers = libCustomers.Select(x => new Customer
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Birthday = x.Birthday
            });
            return View(webCustomers);
        }

        // GET: Customer/Details/5
        public ActionResult Details(int id)
        {
            Lib.Customer libCustomer = CustomerRepo.GetCustomerById(id);
            Lib.Location libStore = StoreRepo.GetAllStores().First();
            var webCust = new Customer
            {
                Id = libCustomer.Id,
                FirstName = libCustomer.FirstName,
                LastName = libCustomer.LastName,
                Birthday = libCustomer.Birthday,
                StoreId = libCustomer.StoreId
            };

            CustomerViewModel cvm = new CustomerViewModel(libCustomer, libStore);

            return View(cvm);
        }

        // GET: Customer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customer/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Customer customer)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Lib.Location libStore = StoreRepo.GetAllStores().First();
                    CustomerRepo.AddCustomer(new Lib.Customer
                    {
                        FirstName = customer.FirstName,
                        LastName = customer.LastName,
                        Birthday = customer.Birthday,
                        StoreId = libStore.LocationId
                    });
                    CustomerRepo.Save();

                    return RedirectToAction(nameof(Index));
                }
                return View(customer);
            }
            catch
            {
                return View();
            }
        }

        // GET: Customer/Edit/5
        public ActionResult Edit(int id)
        {
            Lib.Customer libCustomer = CustomerRepo.GetCustomerById(id);
            var webCustomer = new Customer
            {
                Id = libCustomer.Id,
                FirstName = libCustomer.FirstName,
                LastName = libCustomer.LastName,
                Birthday = libCustomer.Birthday,
                StoreId = libCustomer.StoreId
            };
            return View(webCustomer);
        }

        // POST: Customer/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Customer customer)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    CustomerRepo.UpdateLocation(customer.StoreId, customer.Id);
                    CustomerRepo.Save();

                    return RedirectToAction(nameof(Index));
                }
                return View(customer);
            }
            catch (Exception)
            {
                return View(customer);
            }
        }

        // GET: Customer/Delete/5
        public ActionResult Delete(int id)
        {
            Lib.Customer libCustomer = CustomerRepo.GetCustomerById(id);
            var webCustomer = new Customer
            {
                Id = libCustomer.Id,
                FirstName = libCustomer.LastName,
                Birthday = libCustomer.Birthday,
                StoreId = libCustomer.StoreId
            };
            return View(webCustomer);
        }

        // POST: Customer/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                CustomerRepo.RemoveCustomer(id);
                CustomerRepo.Save();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}