using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mvcCACSLA.Models;

namespace mvcCACSLA.Controllers
{
    public class PruebasController : Controller
    {
        private DB_9F6318_fcacacslav2Entities1 db = new DB_9F6318_fcacacslav2Entities1();
        // GET: Pruebas
        public ActionResult Index()
        {
            var indicador = (from a in db.Indicadores_archivos select a).OrderBy(a => a.IdArchivo);

            return PartialView(indicador.ToList());
        }
    }
}