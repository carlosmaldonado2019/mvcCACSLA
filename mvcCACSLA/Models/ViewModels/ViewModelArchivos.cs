using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace mvcCACSLA.Models.ViewModels
{
    public class ViewModelArchivos
    {
        public int IdArchivo { get; set; }
        public int IdEstandar { get; set; }
        public int IdVariable { get; set; }
        public int IdCarrera { get; set; }
        public string RutaArchivo { get; set; }
        public string NombreArchivo { get; set; }
        public Nullable<System.DateTime> FechaSubido { get; set; }
        public int IdUsuario { get; set; }

        [DataType(DataType.Upload)]
        public IEnumerable<HttpPostedFileBase> Archivos { get; set; }

    }
}