using System;
using System.Collections.Generic;
using System.Text;
using VideoGameOrderSystem.DataAccess;

namespace VideoGameOrderSystem.DataAccess.Repos
{
    public interface IStoreRepository
    {
        IEnumerable<Library.Location> GetAllStores();
        Library.Location GetStoreById(int id);
        Library.Location GetStoreByName(string name);
        void AddStore(Library.Location store);
        void RemoveStore(int id);

        //Inventory Handling
        IEnumerable<Library.Inventory> GetInventory(int storeId);
        IEnumerable<Library.Product> GetInventoryProducts(IEnumerable<Library.Inventory> inv);
        bool InventoryIsEmpty(int storeId);
        void AddInventory(int storeId, int productId);
        void RemoveInventory(int storeId, int productId);
        void AddProduct(int storeId);
        void RemoveProduct(int storeId, int id);
        void AddToInventory(int storeId, int productId, int val);
        void RemoveFromInventory(int storeId, int productId, int val);

        void Save();
    }
}
