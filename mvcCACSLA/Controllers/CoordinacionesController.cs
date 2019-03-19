using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using mvcCACSLA.Models;
using mvcCACSLA.Security;
using PagedList;
using System.IO;
using CrystalDecisions.CrystalReports.Engine;



namespace mvcCACSLA.Controllers
{
    [Authorize]
    [AuthorizeRoles("Administrador")]
    public class CoordinacionesController : Controller
    {
        private DB_9F6318_fcacacslav2Entities1 db = new DB_9F6318_fcacacslav2Entities1();

        // GET: Coordinaciones
        public ActionResult Index(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            var coord = db.Coordinaciones.OrderBy(a => a.IdCoordinacion);
            return View(coord.ToPagedList(pageNumber,pageSize));
        }

        // GET: Coordinaciones/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Coordinaciones coordinaciones = db.Coordinaciones.Find(id);
            if (coordinaciones == null)
            {
                return HttpNotFound();
            }
            return View(coordinaciones);
        }

        // GET: Coordinaciones/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Coordinaciones/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdCoordinacion,Descripcion")] Coordinaciones coordinaciones)
        {
            if (ModelState.IsValid)
            {
                db.Coordinaciones.Add(coordinaciones);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(coordinaciones);
        }

        // GET: Coordinaciones/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Coordinaciones coordinaciones = db.Coordinaciones.Find(id);
            if (coordinaciones == null)
            {
                return HttpNotFound();
            }
            return View(coordinaciones);
        }

        // POST: Coordinaciones/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdCoordinacion,Descripcion")] Coordinaciones coordinaciones)
        {
            if (ModelState.IsValid)
            {
                db.Entry(coordinaciones).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(coordinaciones);
        }

        // GET: Coordinaciones/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Coordinaciones coordinaciones = db.Coordinaciones.Find(id);
            if (coordinaciones == null)
            {
                return HttpNotFound();
            }
            return View(coordinaciones);
        }

        // POST: Coordinaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Coordinaciones coordinaciones = db.Coordinaciones.Find(id);
            db.Coordinaciones.Remove(coordinaciones);
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
