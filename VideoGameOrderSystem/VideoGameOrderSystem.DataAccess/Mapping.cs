using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VideoGameOrderSystem.DataAccess
{
    class Mapping
    {
        public static Library.Location Map(Store store) => new Library.Location
        {
            LocationId = store.Id,
            Name = store.Name,
        };

        public static Store Map(Library.Location store) => new Store
        {
            Id = store.LocationId,
            Name = store.Name,
        };

        public static Library.Customer Map(Customer customer) => new Library.Customer
        {
            Id = customer.Id,
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            Birthday = customer.Birthday,
            StoreId = customer.StoreId,
        };

        public static Customer Map(Library.Customer customer) => new Customer
        {
            Id = customer.Id,
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            Birthday = customer.Birthday,
            StoreId = customer.StoreId,
        };

        public static Library.Order Map(Orders order) => new Library.Order
        {
            Id = order.Id,
            StoreId = order.StoreId,
            CustomerId = order.CustomerId,
            TimePlaced = order.TimePlaced
        };

        public static Orders Map(Library.Order order) => new Orders
        {
            Id = order.Id,
            StoreId = order.StoreId,
            CustomerId = order.CustomerId,
            TimePlaced = order.TimePlaced
        };

        public static Library.OrderItems Map(OrderItems order) => new Library.OrderItems
        {
            OrderId = order.OrderId,
            Quantity = order.Quantity,
            ProductId = order.ProductId
        };

        public static OrderItems Map(Library.OrderItems order) => new OrderItems
        {
            OrderId = order.OrderId,
            Quantity = order.Quantity,
            ProductId = order.ProductId
        };

        public static Library.Inventory Map(Inventory inv) => new Library.Inventory
        {
            StoreId = inv.StoreId,
            ProductId = inv.ProductId,
            Quantity = inv.Quantity
        };

        public static Inventory Map(Library.Inventory inv) => new Inventory
        {
            StoreId = inv.StoreId,
            ProductId = inv.ProductId,
            Quantity = inv.Quantity
        };

        public static Library.Product MapI(Iproduct product) => new Library.Product
        {
           Id = product.Id,
           Name = product.Name,
           Price = (float)product.Price
        };

        public static Iproduct MapI(Library.Product product) => new Iproduct
        {
            Id = product.Id,
            Name = product.Name,
            Price = (decimal)product.Price
        };

        public static Library.Product MapO(Oproduct product) => new Library.Product
        {
            Id = product.Id,
            Name = product.Name,
            Price = (float)product.Price
        };

        public static Oproduct MapO(Library.Product product) => new Oproduct
        {
            Id = product.Id,
            Name = product.Name,
            Price = (decimal)product.Price
        };

        public static IEnumerable<Library.Order> Map(IEnumerable<Orders> orders) => orders.Select(Map);
        public static IEnumerable<Orders> Map(IEnumerable<Library.Order> orders) => orders.Select(Map);
    }
}
