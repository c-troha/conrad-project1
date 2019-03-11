using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using VideoGameOrderSystem.Library;

namespace VideoGameOrderSystem.DataAccess.Repos
{
    public class StoreRepository : IStoreRepository
    {
        private readonly OrderSystemContext _dbContext;

        public StoreRepository(OrderSystemContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public void AddStore(Library.Location store)
        {
            _dbContext.Add(Mapping.Map(store));
        }

        public IEnumerable<Library.Location> GetAllStores()
        {
            List<Location> stores = new List<Location>();

            foreach (Store store in _dbContext.Store)
            {
                stores.Add(Mapping.Map(store));
            }

            return stores;
        }

        public Library.Location GetStoreById(int id)
        {
            return Mapping.Map(_dbContext.Store.First(s => s.Id == id));
        }

        public Library.Location GetStoreByName(string name)
        {
            return Mapping.Map(_dbContext.Store.First(s => s.Name.Equals(name)));
        }

        public void RemoveStore(int id)
        {
            if (_dbContext.Store.Any(s => s.Id == id))
            {
                foreach (Inventory inventory in _dbContext.Inventory.Where(i => i.StoreId == id))
                {
                    _dbContext.Remove(inventory);
                }

                _dbContext.Remove(_dbContext.Store.First(s => s.Id == id));
            }
            else
            {
                Console.WriteLine($"No Store with ID: {id} exists.");
            }
        }

        //---------------------- Inventory Handling ----------------------------------------
        public IEnumerable<Library.Inventory> GetInventory(int storeId)
        {
            List<Library.Inventory> inv = new List<Library.Inventory>();

            foreach (Inventory item in _dbContext.Inventory)
            {
                if(item.StoreId == storeId)
                {
                    inv.Add(Mapping.Map(item));
                }
            }

            return inv;
        }

        public IEnumerable<Library.Product> GetInventoryProducts(IEnumerable<Library.Inventory> inv)
        {
            var products = new List<Library.Product>();
            var inventory = new List<Library.Inventory>(inv);

            foreach (Iproduct p in _dbContext.Iproduct)
            {
                foreach (Library.Inventory i in inventory)
                {
                    if(p.Id == i.ProductId)
                        products.Add(Mapping.MapI(p));
                }
            }

            return products;
        }

        public bool InventoryIsEmpty(int storeId)
        {
            return !_dbContext.Inventory.Any(i => i.StoreId == storeId);
        }

        public void AddInventory(int storeId, int productId)
        {
            var inventory = new Library.Inventory();

            inventory.StoreId = storeId;
            inventory.ProductId = productId;
            inventory.Quantity = 0;

            _dbContext.Add(Mapping.Map(inventory));
        }

        public void RemoveInventory(int storeId, int productId)
        {
            _dbContext.Remove(_dbContext.Inventory.First(i => i.ProductId == productId && i.StoreId == storeId));
        }

        public void AddProduct(int storeId)
        {
            var product = new Library.Product();

            string name;
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Please enter the product name:");

                name = Console.ReadLine();
                if (name.Equals("") || name.Equals(null))
                {
                    Console.WriteLine();
                    Console.WriteLine("Invalid Product Name...");
                }
                else
                {
                    product.Name = name;
                    break;
                }
            }

            Console.WriteLine();
            Console.WriteLine("Please enter the product price:");

            float price;
            bool input = float.TryParse(Console.ReadLine(), out price);

            if(input)
            {
                product.Price = price;
            }

            _dbContext.Add(Mapping.MapI(product));

            var newProduct = _dbContext.Iproduct.Last();

            AddInventory(storeId, newProduct.Id);
        }

        public void RemoveProduct(int storeId, int id)
        {
            if (_dbContext.Iproduct.Any(s => s.Id == id))
            {
                var product = _dbContext.Iproduct.First(p => p.Id == id);

                RemoveInventory(storeId, product.Id);
                _dbContext.Remove(_dbContext.Iproduct.First(p => p.Id == id));
            }
            else
            {
                Console.WriteLine($"No Product with ID: {id} exists.");
            }
        }

        public void AddToInventory(int storeId, int productId, int val)
        {
            _dbContext.Inventory.First(i => i.ProductId == productId && i.StoreId == storeId).Quantity += val;
        }

        public void RemoveFromInventory(int storeId, int productId, int val)
        {
            var inventory = _dbContext.Inventory.First(i => i.ProductId == productId && i.StoreId == storeId);

            if (inventory.Quantity >= val)
            {
                inventory.Quantity -= val;
            }

        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
