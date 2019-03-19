using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.IO;
using System.Text;
using System.Web.Mvc;
using mvcCACSLA.Models;
using mvcCACSLA.Models.Constantes;
using System.Text.RegularExpressions;

namespace mvcCACSLA.Controllers
{
    [Authorize]
    public class Indicadores_archivosController : Controller
    {
        private DB_9F6318_fcacacslav2Entities1 db = new DB_9F6318_fcacacslav2Entities1();

        public ActionResult Index(int? idestandar, int? idvariable)
        {
            ViewBag.Idestandar = idestandar;
            ViewBag.Idvariable = idvariable;
            UserManager UM = new UserManager();

            if (UM.IsUserInRole(User.Identity.Name, EstandarNum.RoleAdmin))
            {
                //var indicadores_archivos = db.Indicadores_archivos.Include(i => i.Carreras).Include(i => i.Indicadores).Include(i => i.Usuarios).OrderBy(i => i.IdVariable).ThenBy(i => i.FechaSubido);
                var indicadores_archivos = db.Indicadores_archivos.Include(i => i.Carreras).Include(i => i.Indicadores).Include(i => i.Usuarios).Where(a => a.IdEstandar == idestandar && a.IdVariable == idvariable).OrderBy(i => i.IdCarrera).ThenBy(i => i.IdArchivo);
                return View(indicadores_archivos.ToList());
            }
            else
            {
                int idcarrera = UM.GetUserCarrera(User.Identity.Name);
                bool b = UM.GetUserSubeCarreras(User.Identity.Name);
                if (b == EstandarNum.SubeCarreas)
                {
                    //var indicadores_archivos = db.Indicadores_archivos.Include(i => i.Carreras).Include(i => i.Indicadores).Include(i => i.Usuarios).OrderBy(i => i.IdVariable).ThenBy(i => i.FechaSubido);
                    var indicadores_archivos = db.Indicadores_archivos.Include(i => i.Carreras).Include(i => i.Indicadores).Include(i => i.Usuarios).Where(a => a.IdEstandar == idestandar && a.IdVariable == idvariable).OrderBy(i => i.IdCarrera).ThenBy(i => i.IdArchivo);
                    return View(indicadores_archivos.ToList());
                }
                else
                {
                    var indicadores_archivos = db.Indicadores_archivos.Include(i => i.Carreras).Include(i => i.Indicadores).Include(i => i.Usuarios).Where(a => a.IdEstandar == idestandar && a.IdVariable == idvariable && a.IdCarrera == idcarrera).OrderBy(i => i.IdCarrera).ThenBy(i => i.IdArchivo);
                    return View(indicadores_archivos.ToList());
                }


            }

        }


        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Indicadores_archivos indicadores_archivos = db.Indicadores_archivos.Find(id);
            if (indicadores_archivos == null)
            {
                return HttpNotFound();
            }
            return View(indicadores_archivos);
        }


        public ActionResult Create(int? idestandar, int? idvariable)
        {
            UserManager UM = new UserManager();
            if (UM.IsUserInRole(User.Identity.Name, EstandarNum.RoleAdmin))
            {
                ViewBag.IdCarrera = new SelectList(db.Carreras, "IdCarrera", "Descripcion");
                ViewBag.IdUsuario = new SelectList(db.Usuarios, "IdUsuario", "Login");
            }
            else
            {
                bool b = UM.GetUserSubeCarreras(User.Identity.Name);

                int idcarrera = UM.GetUserCarrera(User.Identity.Name);
                int idusuario = UM.GetUserID(User.Identity.Name);
                if (b == EstandarNum.SubeCarreas)
                {
                    ViewBag.IdCarrera = new SelectList(db.Carreras, "IdCarrera", "Descripcion");
                }
                else
                {
                    ViewBag.IdCarrera = new SelectList(db.Carreras.Where(A => A.IdCarrera == idcarrera), "IdCarrera", "Descripcion");
                }

                ViewBag.IdUsuario = new SelectList(db.Usuarios.Where(a => a.IdUsuario == idusuario), "IdUsuario", "Login");
            }


            ViewBag.IdEstandar = idestandar;
            ViewBag.IdVariable = idvariable;
            return PartialView("Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdArchivo,IdEstandar,IdVariable,IdCarrera,RutaArchivo,NombreArchivo,FechaSubido,IdUsuario")] Indicadores_archivos indicadores_archivos)

        {
            int idestandar, idvariable;
            if (ModelState.IsValid)
            {
                idestandar = indicadores_archivos.IdEstandar;
                idvariable = indicadores_archivos.IdVariable;

                var httpPostedFileBase = Request.Files["Files"];
                var filesize = Request.Files["Files"].ContentLength;

                for (int i = 0; i < Request.Files.Count; i++)
                {
                    var file = Request.Files[i];
                    if (file != null && file.ContentLength > 0)
                    {

                        var fileName = indicadores_archivos.IdEstandar.ToString() + "-" + indicadores_archivos.IdVariable.ToString() + "-" + indicadores_archivos.IdCarrera.ToString() + " " + Path.GetFileName(file.FileName);
                        var path = Path.Combine(Server.MapPath("~/Content/documentos/"), fileName);
                        //RemoveSpecialCharacters(file.FileName);
                        indicadores_archivos.NombreArchivo = fileName;
                        indicadores_archivos.RutaArchivo = path;
                        file.SaveAs(indicadores_archivos.RutaArchivo);
                        indicadores_archivos.FechaSubido = DateTime.Now;
                        db.Indicadores_archivos.Add(indicadores_archivos);
                        db.SaveChanges();
                    }
                }
                //ViewData["Message"] = "The file has uploaded successully";
                //return RedirectToAction("Index", new {idestandar = indicadores_archivos.IdEstandar,idvariable = indicadores_archivos.IdVariable});
               // return Json(new { success = true });
               return RedirectToAction("Index", new { idestandar = idestandar, idvariable = idvariable });
            }

            ViewBag.IdCarrera = new SelectList(db.Carreras, "IdCarrera", "Descripcion", indicadores_archivos.IdCarrera);
            ViewBag.IdEstandar = new SelectList(db.Indicadores, "IdEstandar", "DescripcionEstandar", indicadores_archivos.IdEstandar);
            ViewBag.IdUsuario = new SelectList(db.Usuarios, "IdUsuario", "Login", indicadores_archivos.IdUsuario);
            return View(indicadores_archivos);
        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Indicadores_archivos indicadores_archivos = db.Indicadores_archivos.Find(id);
            if (indicadores_archivos == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdCarrera = new SelectList(db.Carreras, "IdCarrera", "Descripcion", indicadores_archivos.IdCarrera);
            ViewBag.IdEstandar = new SelectList(db.Indicadores, "IdEstandar", "DescripcionEstandar", indicadores_archivos.IdEstandar);
            ViewBag.IdUsuario = new SelectList(db.Usuarios, "IdUsuario", "Login", indicadores_archivos.IdUsuario);
            return View(indicadores_archivos);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdArchivo,IdEstandar,IdVariable,IdCarrera,RutaArchivo,NombreArchivo,FechaSubido,IdUsuario")] Indicadores_archivos indicadores_archivos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(indicadores_archivos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdCarrera = new SelectList(db.Carreras, "IdCarrera", "Descripcion", indicadores_archivos.IdCarrera);
            ViewBag.IdEstandar = new SelectList(db.Indicadores, "IdEstandar", "DescripcionEstandar", indicadores_archivos.IdEstandar);
            ViewBag.IdUsuario = new SelectList(db.Usuarios, "IdUsuario", "Login", indicadores_archivos.IdUsuario);
            return View(indicadores_archivos);
        }


        public ActionResult EliminarArchivo(int id)
        {

            Indicadores_archivos indicadores_archivos = db.Indicadores_archivos.Find(id);
            ViewBag.idestandar = indicadores_archivos.IdEstandar;
            ViewBag.idvariable = indicadores_archivos.IdVariable;
            var path = Path.Combine(Server.MapPath("~/Content/documentos/"), indicadores_archivos.NombreArchivo);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
                db.Indicadores_archivos.Remove(indicadores_archivos);
                db.SaveChanges();
                return Json(new { success = true });
                //return RedirectToAction("Index", new { idestandar = ViewBag.idestandar, idvariable = ViewBag.idvariable });
            }

            return View();

        }

        public FileResult Download(String p, String d)
        {
            //string archivo = Encoding..GetString(Encoding.GetEncoding(1251).GetBytes(p));
            string pathSource = (Path.Combine(Server.MapPath("~/Content/documentos/"), p));
            FileStream fsSource = new FileStream(pathSource, FileMode.Open, FileAccess.Read);
           // return new FileStreamResult(fsSource, "application/pdf");
            
            return File(Path.Combine(Server.MapPath("~/Content/documentos/"), p), System.Net.Mime.MediaTypeNames.Application.Octet, d);
        }

        //public static string RemoverSignosAcentos(string texto)
        //{
        //    string con = "�";
        //    string sin = "_";
        //    StringBuilder textoSinAcentos = new StringBuilder(texto.Length);
        //    int indexConAcento;
        //    foreach (char caracter in texto)
        //    {
        //        indexConAcento = con.IndexOf(caracter);
        //        if (indexConAcento > -1)
        //            textoSinAcentos.Append(sin.Substring(indexConAcento, 1));
        //        else
        //            textoSinAcentos.Append(caracter);
        //    }
        //    return textoSinAcentos.ToString();
        //}

        //private string RemoveSpecialCharacters(string str)
        //{
        //    return Regex.Replace(str, @"[^0-9A-Za-z]", "", RegexOptions.None);
        //}

        [HttpPost]
        public void Upload()
        {
            for (int i = 0; i < Request.Files.Count; i++)
            {
                var file = Request.Files[i];

                var fileName = Path.GetFileName(file.FileName);

                var path = Path.Combine(Server.MapPath("~/Junk/"), fileName);
                file.SaveAs(path);
            }

        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
