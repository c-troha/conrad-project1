using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VideoGameOrderSystem.App.Models.ViewModels
{
    public class RemoveInventoryViewModel
    {
        [Required]
        [Display(Name = "Product Name")]
        public string ProductName { get; set; }

        public int OrderId { get; set; }
        public int StoreId { get; set; }

        [Required]
        [Display(Name = "Product ID:")]
        public int ProductId { get; set; }

        [Required]
        [Display(Name = "How man would you like to remove?")]
        public int Val { get; set; }
    }
}
