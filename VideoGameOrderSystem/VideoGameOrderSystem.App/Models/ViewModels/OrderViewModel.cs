using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VideoGameOrderSystem.App.Models.ViewModels
{
    public class OrderViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Customer Name")]
        public Library.Customer Customer { get; set; }

        [Display(Name = "Time Placed")]
        [Required] 
        public DateTime TimePlaced { get; set; }

        [Display(Name = "Current Store")]
        public Library.Location Store { get; set; }

        public IEnumerable<Library.Inventory> Inventory { get; set; }
        public IEnumerable<Library.Product> ProductList { get; set; }

        public OrderViewModel(Library.Order order, 
                              Library.Customer customer, Library.Location store, 
                              IEnumerable<Library.Inventory> inventory,
                              IEnumerable<Library.Product> products)
        {
            Id = order.Id;
            Customer = customer;
            TimePlaced = DateTime.Now;
            Store = store;
            Inventory = inventory;
            ProductList = products;
        }
    }
}
