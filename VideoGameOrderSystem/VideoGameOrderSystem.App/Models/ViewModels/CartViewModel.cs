﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using VideoGameOrderSystem.Library;

namespace VideoGameOrderSystem.App.Models.ViewModels
{
    public class CartViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Product Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Unit Price")]
        public decimal Price { get; set; }

        [Required]
        [Display(Name = "# in Cart")]
        public int Quantity { get; set; }

        [Display(Name = "Cart Total")]
        public float Total { get; set; }

        public IEnumerable<InventoryViewModel> Ivm { get; set; }
    }
}
