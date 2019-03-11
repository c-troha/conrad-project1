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
    public class CartController : Controller
    {
        public DataAccess.Repos.ICustomerRepository CustomerRepo { get; }
        public DataAccess.Repos.IStoreRepository StoreRepo { get; }
        public DataAccess.Repos.IOrderRepository OrderRepo { get; }

        public CartController(DataAccess.Repos.ICustomerRepository cRepo,
                               DataAccess.Repos.IStoreRepository sRepo,
                               DataAccess.Repos.IOrderRepository oRepo)
        {
            CustomerRepo = cRepo;
            StoreRepo = sRepo;
            OrderRepo = oRepo;
        }

        // GET: Cart
        public ActionResult Index(int Id)
        {
            ViewBag.orderId = Id;
            Library.Order order = OrderRepo.GetOrderById(Id);
            IEnumerable<Lib.Inventory> libInv = StoreRepo.GetInventory(order.StoreId);
            IEnumerable<Lib.Product> libProducts = StoreRepo.GetInventoryProducts(libInv);

            IEnumerable<InventoryViewModel> ivm = libProducts.Select(x => new InventoryViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Price = (decimal)x.Price,
                Quantity = libInv.First(i => i.ProductId == x.Id && i.StoreId == order.StoreId).Quantity,
                StoreName = StoreRepo.GetStoreById(order.StoreId).Name,
                CustomerName = CustomerRepo.GetCustomerById(order.CustomerId).FirstName + ' ' +
                               CustomerRepo.GetCustomerById(order.CustomerId).LastName
            });
            
            IEnumerable<Lib.OrderItems> libOrderItems = OrderRepo.GetOrderItems(order.Id);
            IEnumerable<Lib.Product> libOrderProducts = OrderRepo.GetOrderProducts(libOrderItems);

            IEnumerable<CartViewModel> cvm = libOrderProducts.Select(x => new CartViewModel
            {
                Name = x.Name,
                Price = (decimal)x.Price,
                Quantity = libOrderItems.First(i => i.ProductId == x.Id && i.OrderId == order.Id).Quantity,
                Ivm = ivm,
                Total = OrderRepo.OrderTotal(order.Id)

            });

                return View(cvm);
        }

        // GET: Cart/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Cart/Create
        public ActionResult AddToOrder()
        {
            return View();
        }

        // POST: Cart/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddToOrder(int id, AddInventoryViewModel model)
        {
            try
            {
                var order = OrderRepo.GetOrderById(id);
                IEnumerable<Lib.Inventory> libInv = StoreRepo.GetInventory(order.StoreId);
                IEnumerable<Lib.Product> libProducts = StoreRepo.GetInventoryProducts(libInv);


                AddInventoryViewModel avm = new AddInventoryViewModel
                {
                    OrderId = id,
                    StoreId = order.StoreId,
                    ProductId = model.ProductId,
                    Val = model.Val
                };

                OrderRepo.AddToOrder(id, order.StoreId, avm.ProductId, avm.Val);

                return RedirectToAction("Index", "Cart", new { @id = order.Id });
            }
            catch
            {
                return RedirectToAction("Index", "Cart", new { @id = id });
            }
        }

        // GET: Cart/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Cart/Edit/5
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

        // GET: Cart/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Cart/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, RemoveInventoryViewModel model)
        {
            try
            {
                var order = OrderRepo.GetOrderById(id);
                IEnumerable<Lib.Inventory> libInv = StoreRepo.GetInventory(order.StoreId);
                IEnumerable<Lib.Product> libProducts = StoreRepo.GetInventoryProducts(libInv);


                RemoveInventoryViewModel avm = new RemoveInventoryViewModel
                {
                    OrderId = id,
                    StoreId = order.StoreId,
                    ProductId = model.ProductId,
                    Val = model.Val
                };

                OrderRepo.RemoveFromOrder(id, order.StoreId, avm.ProductId, avm.Val);

                return RedirectToAction("Index", "Cart", new { @id = order.Id });
            }
            catch
            {
                return RedirectToAction("Index", "Cart", new { @id = id });
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Submit(int id)
        {
            return RedirectToAction("Index", "Order", new { @id = id });
        }
    }
}