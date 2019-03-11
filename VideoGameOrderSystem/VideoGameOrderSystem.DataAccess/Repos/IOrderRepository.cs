using System;
using System.Collections.Generic;
using System.Text;

namespace VideoGameOrderSystem.DataAccess.Repos
{
    public interface IOrderRepository
    {
        // Logic to handle the placement of orders
        IEnumerable<Library.Order> GetAllOrders();
        Library.Order GetOrderById(int id);
        void AddOrder(Library.Order order);
        void RemoveOrder(int id);
        float OrderTotal(int orderID);

        IEnumerable<Library.OrderItems> GetOrderItems(int orderId);
        IEnumerable<Library.Product> GetOrderProducts(IEnumerable<Library.OrderItems> oI);
        bool OrderIsEmpty(int orderId);
        void AddOrderItems(int orderId, int productId);
        void RemoveOrderItems(int orderId, int productId);
        void AddToOrder(int orderId, int storeId, int productId, int val);
        void RemoveFromOrder(int orderId, int storeId, int productId, int val);
        void AddProduct(int orderId, Library.Product p);
        void RemoveProduct(int orderId, int id);

        Library.Product getProductByName(string name);
        bool checkProduct(int productId, int orderId);

        void PrintCustomerHistory(int customerID);
        void PrintStoreHistory(int storeID);

        // Order Statistics
        void HighestAverageCustomer();
        void HighestSumTotal();
        void MostOrders();
    }
}
