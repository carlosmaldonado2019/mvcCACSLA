using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.Mvc;
using mvcCACSLA.Models;

namespace mvcCACSLA.Controllers
{
    public class CargarArchivoController : Controller
    {
        // GET: CargarArchivo
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult CargarArchivo(Archivo archivo)
        {
            foreach (var item in archivo.Archivos)
            {
                if (item.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(item.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/documentos"), fileName);
                    item.SaveAs(path);
                }
            }
            return RedirectToAction("Create","Indicadores_archivos",archivo);

        }
    }
}