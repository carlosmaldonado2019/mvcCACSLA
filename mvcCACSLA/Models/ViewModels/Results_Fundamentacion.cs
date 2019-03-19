using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using mvcCACSLA.Models;
using System.ComponentModel.DataAnnotations;

namespace mvcCACSLA.Models.ViewModels
{
    public class Results_Fundamentacion
    {
        [Key]
        public int IdEstandar { get; set; }
        [Key]
        public int IdVariable { get; set; }
        [Key]
        public int IdCarrera { get; set; }
        public string Fundamentacion { get; set; }
    }
}