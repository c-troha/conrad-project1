using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VideoGameOrderSystem.App.Models.ViewModels
{
    public class CustomerViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string FullName { get; set; }

        [Display(Name = "Birthday")]
        [DataType(DataType.Date)]
        [Required] // now default value (1/1/1 won't be accepted)
        public DateTime Birthday { get; set; }

        [Display(Name = "Current Store")]
        public Library.Location Store { get; set; }



        public CustomerViewModel(Library.Customer customer, Library.Location store)
        {
            Id = customer.Id;
            FullName = customer.FirstName + ' ' + customer.LastName;
            Birthday = customer.Birthday;
            Store = store;
        }
    }
}
