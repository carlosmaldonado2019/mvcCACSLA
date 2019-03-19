using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace mvcCACSLA.Controllers
{
    public class HomeController : Controller
    {
        
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult NoAutorizado()
        {
            return View();
        }

        public ActionResult Generales()
        {
            return View();
        }

        public ActionResult Normatividad()
        {
            return PartialView("Normatividad");
        }

        
        public FileResult Download(String p, String d)
        {
            //string archivo = Encoding..GetString(Encoding.GetEncoding(1251).GetBytes(p));
            return File(Path.Combine(Server.MapPath("~/ArchivosPDF/"), p), System.Net.Mime.MediaTypeNames.Application.Octet, d);
        }

    }
}