using mvcCACSLA.Models;
using mvcCACSLA.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using mvcCACSLA.Models.Constantes;

namespace mvcCACSLA.Controllers
{
    [Authorize]
    public class Indicadores_detalleController : Controller
    {
        private DB_9F6318_fcacacslav2Entities1 db = new DB_9F6318_fcacacslav2Entities1();
        public ActionResult Index()
        {
            var indicadores_detalle = db.Indicadores_detalle.Include(i => i.Carreras).Include(i => i.Indicadores).Include(i => i.Usuarios);
            return View(indicadores_detalle.ToList());
        }


        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Indicadores_detalle indicadores_detalle = db.Indicadores_detalle.Find(id);
            if (indicadores_detalle == null)
            {
                return HttpNotFound();
            }
            return View(indicadores_detalle);
        }

        [HttpGet]
        public ActionResult Create(int? idestandar, int? idvariable, int? idcarrera)
        {
            ViewBag.IdCarrera = new SelectList(db.Carreras, "IdCarrera", "Descripcion");
            ViewBag.IdEstandar = idestandar;
            ViewBag.IdVariable = idvariable;
            ViewBag.IdUsuario = new SelectList(db.Usuarios, "IdUsuario", "Login");
            
            return View();
        }

        public ActionResult VerFundamentacion(int? idestandar, int? idvariable )
        {
            UserManager UM = new UserManager();
            int idcarrera = UM.GetUserCarrera(User.Identity.Name);
            int iduser = UM.GetUserID(User.Identity.Name);

            var result = from a in db.Indicadores_detalle
                         where a.IdEstandar == idestandar && a.IdVariable == idvariable
                         && a.IdCarrera == idcarrera
                         select new Indicadores_detalle { IdFundamentacion = a.IdFundamentacion, Descripcion = a.Descripcion, IdEstandar = a.IdEstandar, IdVariable = a.IdVariable, IdCarrera = a.IdCarrera, IdUsuario = a.IdUsuario };

            if (UM.IsUserInRole(User.Identity.Name,EstandarNum.RoleAdmin))
            {
                ViewBag.IdCarrera = new SelectList(db.Carreras, "IdCarrera", "Descripcion");
                ViewBag.IdUsuario = new SelectList(db.Usuarios, "IdUsuario", "Login");
            }
            else
            {
                ViewBag.IdCarrera = new SelectList(db.Carreras.Where(a => a.IdCarrera == idcarrera), "IdCarrera", "Descripcion");
                ViewBag.IdUsuario = new SelectList(db.Usuarios.Where(a=> a.IdUsuario == iduser), "IdUsuario", "Login");
            }

            return PartialView("_Fundamentacion", db.Indicadores_detalle.SingleOrDefault(a => a.IdEstandar == idestandar && a.IdVariable == idvariable && a.IdCarrera == idcarrera));

            //if (idcarrera == null)
            //{
            //    return PartialView("_Fundamentacion", db.Indicadores_detalle.SingleOrDefault(a => a.IdEstandar == idestandar && a.IdVariable == idvariable && a.IdCarrera == idcarrera));

            //}
            //else
            //{

            //   var result = from a in db.Indicadores_detalle
            //                 where a.IdEstandar == idestandar && a.IdVariable == idvariable
            //                 && a.IdCarrera == idcarrera
            //                 select new Indicadores_detalle {IdFundamentacion = a.IdFundamentacion, Descripcion = a.Descripcion, IdEstandar = a.IdEstandar, IdVariable = a.IdVariable, IdCarrera = a.IdCarrera,IdUsuario = a.IdUsuario};


            //    ViewBag.IdCarrera = new SelectList(db.Carreras, "IdCarrera", "Descripcion",idcarrera);
            //    ViewBag.IdUsuario = new SelectList(db.Usuarios, "IdUsuario", "Login", 1);
            //    return PartialView("_Fundamentacion",db.Indicadores_detalle.SingleOrDefault(a => a.IdEstandar ==idestandar && a.IdVariable ==idvariable && a.IdCarrera ==idcarrera));
            //}

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GuardarFundamentacion(Indicadores_detalle indicadores_detalle)
        {
     
                ViewBag.idestandar = indicadores_detalle.IdEstandar;
                ViewBag.idvariable = indicadores_detalle.IdVariable;
                ViewBag.idcarrera = indicadores_detalle.IdCarrera;
                ViewBag.Fundamentacion = indicadores_detalle.Descripcion;
                int? id;
                id = indicadores_detalle.IdFundamentacion;

                    if(id == 0)
                    {
                        db.Indicadores_detalle.Add(indicadores_detalle);
                        db.SaveChanges();
                        return Json(new { success = true });
                    }

                if (ModelState.IsValid)
                 {
                        db.Entry(indicadores_detalle).State = EntityState.Modified;
                        db.SaveChanges();
                        return Json(new { success = true });
                 }
                    else
                        return PartialView("_Fundamentacion");
                }
          
   
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdFundamentacion,Descripcion,IdEstandar,IdVariable,IdCarrera,IdUsuario")] Indicadores_detalle indicadores_detalle)
        {
            if (ModelState.IsValid)
            {
                db.Indicadores_detalle.Add(indicadores_detalle);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdCarrera = new SelectList(db.Carreras, "IdCarrera", "Descripcion", indicadores_detalle.IdCarrera);
            ViewBag.IdEstandar = new SelectList(db.Indicadores, "IdEstandar", "DescripcionEstandar", indicadores_detalle.IdEstandar);
            ViewBag.IdUsuario = new SelectList(db.Usuarios, "IdUsuario", "Login", indicadores_detalle.IdUsuario);
            return View(indicadores_detalle);
        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Indicadores_detalle indicadores_detalle = db.Indicadores_detalle.Find(id);
            if (indicadores_detalle == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdCarrera = new SelectList(db.Carreras, "IdCarrera", "Descripcion", indicadores_detalle.IdCarrera);
            //ViewBag.IdEstandar = new SelectList(db.Indicadores, "IdEstandar", "DescripcionEstandar", indicadores_detalle.IdEstandar);
            ViewBag.IdUsuario = new SelectList(db.Usuarios, "IdUsuario", "Login", indicadores_detalle.IdUsuario);
            return View(indicadores_detalle);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdFundamentacion,Descripcion,IdEstandar,IdVariable,IdCarrera,IdUsuario")] Indicadores_detalle indicadores_detalle)
        {
            if (ModelState.IsValid)
            {
                db.Entry(indicadores_detalle).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdCarrera = new SelectList(db.Carreras, "IdCarrera", "Descripcion", indicadores_detalle.IdCarrera);
            ViewBag.IdEstandar = new SelectList(db.Indicadores, "IdEstandar", "DescripcionEstandar", indicadores_detalle.IdEstandar);
            ViewBag.IdUsuario = new SelectList(db.Usuarios, "IdUsuario", "Login", indicadores_detalle.IdUsuario);
            return View(indicadores_detalle);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Indicadores_detalle indicadores_detalle = db.Indicadores_detalle.Find(id);
            if (indicadores_detalle == null)
            {
                return HttpNotFound();
            }
            return View(indicadores_detalle);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Indicadores_detalle indicadores_detalle = db.Indicadores_detalle.Find(id);
            db.Indicadores_detalle.Remove(indicadores_detalle);
            db.SaveChanges();
            return RedirectToAction("Index");
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
