using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VideoGameOrderSystem.App.Models.ViewModels
{
    public class LoginModel
    {
        [Required]
        public int Id { get; set; }
    }
}
