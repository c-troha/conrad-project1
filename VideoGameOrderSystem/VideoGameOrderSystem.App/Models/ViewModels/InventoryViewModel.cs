using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VideoGameOrderSystem.App.Models.ViewModels
{
    public class InventoryViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Product Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name="Unit Price")]
        public decimal Price { get; set; }

        [Required]
        [Display(Name = "On Hand")]
        public int Quantity { get; set; }

        public string StoreName { get; set; }
        public string CustomerName { get; set; }
    }
}
