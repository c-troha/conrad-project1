using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VideoGameOrderSystem.Library;

namespace VideoGameOrderSystem.DataAccess.Repos
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly OrderSystemContext _dbContext;

        public CustomerRepository(OrderSystemContext db)
        {
            _dbContext = db ?? throw new ArgumentNullException(nameof(db));
        }

        public bool ContainsId(int id)
        {
            return _dbContext.Customer.Any(c => c.Id == id);
        }

        public void AddCustomer(Library.Customer customer)
        {
            _dbContext.Add(Mapping.Map(customer));
        }

        public IEnumerable<Library.Customer> GetAll()
        {
            var customers = new List<Library.Customer>();

            foreach (Customer c in _dbContext.Customer)
            {
                customers.Add(Mapping.Map(c));
            }

            return customers;
        }

        public Library.Customer GetCustomerById(int id)
        {
            if(!_dbContext.Customer.Any(c => c.Id == id))
            {
                return null;
            }

            return Mapping.Map(_dbContext.Customer.First(c => c.Id == id));
        }

        public Library.Customer GetCustomerByName(string fName, string lName)
        {
            throw new NotImplementedException();
        }

        public void RemoveCustomer(int id)
        {
            if(_dbContext.Customer.Any(c => c.Id == id))
            {
                _dbContext.Remove(_dbContext.Customer.First(c => c.Id == id));
            }
            else
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        public void UpdateLocation(int storeId, int customerId)
        {
            if(_dbContext.Store.Any(s => s.Id == storeId))
            {
                _dbContext.Customer.First(c => c.Id == customerId).StoreId = storeId;
            }
            else
            {
                throw new ArgumentException("ID not associated with a store...");
            }
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

    }
}
