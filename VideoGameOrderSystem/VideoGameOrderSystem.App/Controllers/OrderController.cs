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
        public ActionResult Index(int Id)
        {
            if (CustomerRepo.ContainsId(Id))
            {
                ViewBag.customerId = Id;
                Library.Customer customer = CustomerRepo.GetCustomerById(Id);
                IEnumerable<Lib.Inventory> libInv = StoreRepo.GetInventory(customer.StoreId);
                IEnumerable<Lib.Product> libProducts = StoreRepo.GetInventoryProducts(libInv);

                IEnumerable<InventoryViewModel> ivm = libProducts.Select(x => new InventoryViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Price = (decimal)x.Price,
                    Quantity = libInv.First(i => i.ProductId == x.Id && i.StoreId == customer.StoreId).Quantity,
                    StoreName = StoreRepo.GetStoreById(customer.StoreId).Name,
                    CustomerName = customer.FirstName + ' ' + customer.LastName
                });
                return View(ivm);

            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: Order/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult AddOrder()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddOrder(int Id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Library.Customer customer = CustomerRepo.GetCustomerById(Id);
                    OrderRepo.AddOrder(new Lib.Order
                    {
                        CustomerId = customer.Id,
                        StoreId = customer.StoreId,
                        TimePlaced = DateTime.Now
                    });

                    var order = OrderRepo.GetAllOrders().Last();
                    var inv = StoreRepo.GetInventoryProducts(StoreRepo.GetInventory(customer.StoreId));
                    foreach (Lib.Product item in inv)
                    {
                        OrderRepo.AddProduct(order.Id, item);
                    }

                    return RedirectToAction("Index", "Cart", 
                        new { @id = order.Id });
                }
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

    }
}