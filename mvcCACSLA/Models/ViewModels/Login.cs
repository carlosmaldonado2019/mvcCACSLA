using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using mvcCACSLA.Models;
using System.ComponentModel.DataAnnotations;

namespace mvcCACSLA.Models
{
    public class Login
    {
        [Required(ErrorMessage ="Ingrese su Usuario")]
        [StringLength(50)]
   
        public string Usuario { get; set; }

        [Required(ErrorMessage = "Ingrese su Password")]
        [StringLength(50)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}