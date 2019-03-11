using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VideoGameOrderSystem.App.Models.ViewModels
{
    public class StoreViewModel
    {
        [Required]
        [Display(Name = "Store ID")]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Store Name")]
        public string Name { get; set; }



        public StoreViewModel(Library.Location store)
        {
            Id = store.LocationId;
            Name = store.Name;
        }
    }
}
