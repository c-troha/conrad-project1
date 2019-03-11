﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VideoGameOrderSystem.Library;

namespace VideoGameOrderSystem.DataAccess.Repos
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderSystemContext _dbContext;

        public OrderRepository(OrderSystemContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public IEnumerable<Order> GetAllOrders()
        {
            List<Order> orders = new List<Order>();

            foreach (Orders order in _dbContext.Orders)
            {
                orders.Add(Mapping.Map(order));
            }

            return orders;
        }

        public Order GetOrderById(int id)
        {
            return Mapping.Map(_dbContext.Orders.First(s => s.Id == id));
        }

        public void AddOrder(Order order)
        {
            _dbContext.Add(Mapping.Map(order));
            _dbContext.SaveChanges();
        }

        public void RemoveOrder(int id)
        {
            if (_dbContext.Orders.Any(s => s.Id == id))
            {
                foreach (OrderItems oI in _dbContext.OrderItems.Where(i => i.OrderId == id))
                {
                    _dbContext.Remove(oI);
                }

                _dbContext.Remove(_dbContext.Orders.First(s => s.Id == id));
                _dbContext.SaveChanges();
            }
            else
            {
                Console.WriteLine($"No Store with ID: {id} exists.");
            }
        }

        public float OrderTotal(int orderID)
        {
            var items = GetOrderItems(orderID).ToList();
            var products = GetOrderProducts(items).ToList();
            float temp = 0;
            float total = 0;

            foreach (Library.OrderItems item in items)
            {
                if(item.OrderId == orderID)
                {
                    temp = products.First(s => s.Id == item.ProductId).Price * item.Quantity;
                    total += temp;
                }
            }

            return total;
        }

        public void PrintCustomerHistory(int customerID)
        {

            var customer = _dbContext.Customer.First(c => c.Id == customerID);
            var orders = Mapping.Map(_dbContext.Orders.Where(o => o.CustomerId == customerID)).ToList();


            Console.WriteLine();
            Console.WriteLine($"Order History: {customer.FirstName} {customer.LastName}");
            Console.WriteLine();

            foreach (Library.Order o in orders)
            {
                var items = GetOrderItems(o.Id);
                var products = GetOrderProducts(items);

                Console.WriteLine($"Order #: {o.Id}");
                Console.WriteLine("{0,-10}{1,-25}{2,-15}{3,-10}{4,-25}{5,-10}",
                    "ID", "Name", "Quantity", "Price", "Time Placed", "Store");
                Console.WriteLine("{0,-10}{1,-25}{2,-15}{3,-10}{4,-25}{5,-10}",
                    "--", "----", "--------", "-----", "-----------", "-----");

                foreach (Library.Product p in products)
                {
                    Console.WriteLine("{0,-10}{1,-25}{2,-15}{3,-10}{4,-25}{5,-10}",
                        p.Id, p.Name, items.First(i => i.ProductId == p.Id).Quantity,
                        p.Price, o.TimePlaced, _dbContext.Store.First(s => s.Id == o.StoreId).Name);
                }

                Console.WriteLine();
                Console.WriteLine($"Order Total: " +
                    $"{OrderTotal(o.Id)}");
                Console.WriteLine();
            }
        }

        public void PrintStoreHistory(int storeID)
        {
            var store = _dbContext.Store.First(c => c.Id == storeID);
            var orders = Mapping.Map(_dbContext.Orders.Where(o => o.StoreId == storeID)).ToList();
          
            Console.WriteLine();
            Console.WriteLine($"Order History: {store.Name}");
            Console.WriteLine();

            foreach (Library.Order o in orders)
            {
                var items = GetOrderItems(o.Id);
                var products = GetOrderProducts(items);

                Console.WriteLine($"Order #: {o.Id}");
                Console.WriteLine("{0,-10}{1,-25}{2,-15}{3,-10}{4,-25}{5,-10}",
                    "ID", "Name", "Quantity", "Price", "Time Placed", "Customer");
                Console.WriteLine("{0,-10}{1,-25}{2,-15}{3,-10}{4,-25}{5,-10}",
                    "--", "----", "--------", "-----", "-----------", "--------");

                foreach (Library.Product p in products)
                {
                    var customer = _dbContext.Customer.First(c => c.Id == o.CustomerId);
                    Console.WriteLine("{0,-10}{1,-25}{2,-15}{3,-10}{4,-25}{5,-10}",
                        p.Id, p.Name, items.First(i => i.ProductId == p.Id).Quantity,
                        p.Price, o.TimePlaced, customer.FirstName+ ' ' + customer.LastName);
                }

                Console.WriteLine();
                Console.WriteLine($"Order Total: " +
                    $"{OrderTotal(o.Id)}");
                Console.WriteLine();
            }
        }

        // Order handling
        public IEnumerable<Library.OrderItems> GetOrderItems(int orderId)
        {
            List<Library.OrderItems> order = new List<Library.OrderItems>();

            foreach (OrderItems item in _dbContext.OrderItems)
            {
                if (item.OrderId == orderId)
                {
                    order.Add(Mapping.Map(item));
                }
            }

            return order;
        }

        public IEnumerable<Product> GetOrderProducts(IEnumerable<Library.OrderItems> oI)
        {
            var products = new List<Library.Product>();
            var orderItems = new List<Library.OrderItems>(oI);

            foreach (Oproduct p in _dbContext.Oproduct)
            {
                foreach (Library.OrderItems i in orderItems)
                {
                    if (p.Id == i.ProductId)
                        products.Add(Mapping.MapO(p));
                }
            }

            return products;
        }

        public bool OrderIsEmpty(int orderId)
        {
            return !_dbContext.OrderItems.Any(i => i.OrderId == orderId);
        }

        public void AddOrderItems(int orderId, int productId)
        {
            var orderItems = new Library.OrderItems();

            orderItems.OrderId = orderId;
            orderItems.ProductId = productId;
            orderItems.Quantity = 0;

            _dbContext.Add(Mapping.Map(orderItems));
            _dbContext.SaveChanges();
        }

        public void RemoveOrderItems(int orderId, int productId)
        {
            _dbContext.Remove(_dbContext.OrderItems.First(i => i.ProductId == productId && i.OrderId == orderId));
            _dbContext.SaveChanges();
        }

        public void AddToOrder(int orderId, int storeId, int productId, int val)
        {
            var inv = _dbContext.Inventory.First(i => i.ProductId == productId && i.StoreId == storeId);
            var invProduct = _dbContext.Iproduct.First(p => p.Id == productId);

            if(productId == 31)
            {
                _dbContext.Inventory.First(i => i.ProductId == 21 && i.StoreId == 17).Quantity -= 1;
                _dbContext.Inventory.First(i => i.ProductId == 22 && i.StoreId == 17).Quantity -= 1;
                _dbContext.Inventory.First(i => i.ProductId == 23 && i.StoreId == 17).Quantity -= 1;
            }
            if (inv.Quantity >= val)
            {
                _dbContext.OrderItems.First(i => i.ProductId == _dbContext.Oproduct.First(op => op.Name == invProduct.Name).Id && i.OrderId == orderId).Quantity += val;
                _dbContext.Inventory.First(i => i.ProductId == productId && i.StoreId == storeId).Quantity -= val;
                _dbContext.SaveChanges();
            }
            else
            {
                Console.WriteLine("The inventory failed to update...");
                Console.WriteLine();
            }
        }

        public void RemoveFromOrder(int orderId, int storeId, int productId, int val)
        {
            var orderItems = _dbContext.OrderItems.First(i => i.ProductId == productId && i.OrderId == orderId);
            var inv = _dbContext.Inventory.First(i => i.ProductId == productId && i.StoreId == storeId);
            var invProduct = _dbContext.Iproduct.First(p => p.Id == productId);

            if (orderItems.Quantity >= val)
            {
                
                orderItems.Quantity -= val;
            }

            _dbContext.SaveChanges();
        }

        public void AddProduct(int orderId, Library.Product p)
        {
            if (!_dbContext.Oproduct.Any(s => s.Name == p.Name))
            {
                var product = new Library.Product();

                product.Name = p.Name;
                product.Price = (float)p.Price;

                _dbContext.Add(Mapping.MapO(product));
                _dbContext.SaveChanges();

                var newProduct = _dbContext.Oproduct.Last();

                AddOrderItems(orderId, newProduct.Id);
            }
            else
            {
                AddOrderItems(orderId, _dbContext.Oproduct.First(s => s.Name == p.Name).Id);
            }
        }

        public void RemoveProduct(int orderId, int id)
        {
            if (_dbContext.Oproduct.Any(s => s.Id == id))
            {
                var product = _dbContext.Oproduct.First(p => p.Id == id);

                RemoveOrderItems(orderId, product.Id);
                _dbContext.Remove(_dbContext.Oproduct.First(p => p.Id == id));
                _dbContext.SaveChanges();
            }
            else
            {
                Console.WriteLine($"No Product with ID: {id} exists.");
            }
        }

        public Library.Product getProductByName(string name)
        {
            return Mapping.MapO(_dbContext.Oproduct.First(p => p.Name.Equals(name)));
        }

        public bool checkProduct(int productId, int orderId)
        {
            return _dbContext.OrderItems.Any(i => i.ProductId == productId && i.OrderId == orderId);
        }

        public void HighestAverageCustomer()
        {
            float high = 0;
            float temp = 0;
            int count = 0;

            Library.Customer best = new Library.Customer();

            var customerRepo = new CustomerRepository(_dbContext);

            var customers = customerRepo.GetAll().ToList();
            var orders = GetAllOrders();

            foreach (Library.Customer c in customers)
            {
                foreach (Library.Order o in orders.Where(ord => ord.CustomerId == c.Id))
                {
                    var items = GetOrderItems(o.Id).ToList();
                    var products = GetOrderProducts(items);

                    var orderTotal = OrderTotal(o.Id);

                    temp += orderTotal;
                    count++;
                }

                temp = temp / count;

                if(temp > high)
                {
                    high = temp;
                    best = c;
                }
            }

            Console.WriteLine($"{best.FirstName + ' ' + best.LastName} had" +
                $" the highest average \"Order Total\": ${high}");


        }

        public void HighestSumTotal()
        {
            float high = 0;
            float temp = 0;

            Library.Customer best = new Library.Customer();

            var customerRepo = new CustomerRepository(_dbContext);

            var customers = customerRepo.GetAll().ToList();
            var orders = GetAllOrders();

            foreach (Library.Customer c in customers)
            {
                foreach (Library.Order o in orders.Where(ord => ord.CustomerId == c.Id))
                {
                    var items = GetOrderItems(o.Id).ToList();
                    var products = GetOrderProducts(items);

                    var orderTotal = OrderTotal(o.Id);

                    temp += orderTotal;
                }

                if (temp > high)
                {
                    high = temp;
                    best = c;
                }
            }

            Console.WriteLine($"{best.FirstName + ' ' + best.LastName} had" +
                $" the highest \"Order Total\" sum: ${high}");
        }

        public void MostOrders()
        {
            float high = 0;
            float temp = 0;

            Library.Customer best = new Library.Customer();

            var customerRepo = new CustomerRepository(_dbContext);

            var customers = customerRepo.GetAll().ToList();
            var orders = GetAllOrders();

            foreach (Library.Customer c in customers)
            {
                foreach (Library.Order o in orders.Where(ord => ord.CustomerId == c.Id))
                { 
                    temp++;
                }

                if (temp > high)
                {
                    high = temp;
                    best = c;
                }

                temp = 0;
            }

            Console.WriteLine($"{best.FirstName + ' ' + best.LastName} had" +
                $" the most orders with: {high}");
            Console.WriteLine();
        }
    }
}
