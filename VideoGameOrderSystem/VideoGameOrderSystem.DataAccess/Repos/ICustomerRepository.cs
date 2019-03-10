using System;
using System.Collections.Generic;
using System.Text;

namespace VideoGameOrderSystem.DataAccess.Repos
{
    public interface ICustomerRepository
    {
        IEnumerable<Library.Customer> GetAll();
        Library.Customer GetCustomerById(int id);
        Library.Customer GetCustomerByName(string fName, string lName);
        void AddCustomer(Library.Customer customer);
        void RemoveCustomer(int id);
        bool ContainsId(int id);
        //Library.Customer CreateNewCustomer();
        void UpdateLocation(int storeId, int customerId);
        void Save();
    }
}
