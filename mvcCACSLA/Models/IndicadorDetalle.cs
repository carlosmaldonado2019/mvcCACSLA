using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace mvcCACSLA.Models
{
    public class IndicadorDetalle
    {
       
        [Required]
        public int IdArchivo { get; set; }
        [Required]
        public int IdEstandar { get; set; }
        [Required]
        public int IdVariable { get; set; }
        [Required]
        public int IdCarrera { get; set; }
        [Required]
        public int IdUsuario { get; set; }

        public string RutaArchivo { get; set; }
        public string NombreArchivo { get; set; }
        public Nullable<System.DateTime> FechaSubido { get; set; }
        public string Ini { get; set; }
    }


}