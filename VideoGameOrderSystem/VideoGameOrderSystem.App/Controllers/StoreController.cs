using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VideoGameOrderSystem.App.Models.ViewModels;
using VideoGameOrderSystem.DataAccess;
using Lib = VideoGameOrderSystem.Library;

namespace VideoGameOrderSystem.App.Controllers
{
    public class StoreController : Controller
    {
        public DataAccess.Repos.ICustomerRepository CustomerRepo { get; }
        public DataAccess.Repos.IStoreRepository StoreRepo { get; }

        public StoreController(DataAccess.Repos.ICustomerRepository cRepo,
                               DataAccess.Repos.IStoreRepository sRepo)
        {
            CustomerRepo = cRepo;
            StoreRepo = sRepo;
        }

        // GET: Store
        public ActionResult Index()
        {
            IEnumerable<Lib.Location> libStore = StoreRepo.GetAllStores();
            var webStores = libStore.Select(x => new StoreViewModel(x)
            {
                Id = x.LocationId,
                Name = x.Name,
            }).ToList();
            return View(webStores);
        }

        // GET: Store/Details/5
        public ActionResult Details(int id)
        {
            Lib.Location libStore = StoreRepo.GetStoreById(id);

            var webStore = new Store
            {
                Id = libStore.LocationId,
                Name = libStore.Name,
            };

            StoreViewModel svm = new StoreViewModel(libStore);

            return View(svm);
        }

        // GET: Store/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Store/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Store store)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    StoreRepo.AddStore(new Lib.Location
                    {
                        Name = store.Name,
                    });
                    StoreRepo.Save();



                    return RedirectToAction(nameof(Index));
                }
                return View(store);
            }
            catch
            {
                return View();
            }
        }

        // GET: Store/Delete/5
        public ActionResult Delete(int id)
        {
            Lib.Location libStore = StoreRepo.GetStoreById(id);
            var webStore = new Store
            {
                Id = libStore.LocationId,
                Name = libStore.Name,
            };
            return View(libStore);
        }

        // POST: Store/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                StoreRepo.RemoveStore(id);
                StoreRepo.Save();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}