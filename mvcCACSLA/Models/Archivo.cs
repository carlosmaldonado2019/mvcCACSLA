using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mvcCACSLA.Models
{
    public class Archivo
    {
        public IEnumerable<HttpPostedFileBase> Archivos { get; set; }
    }
}