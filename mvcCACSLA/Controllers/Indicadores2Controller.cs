using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using mvcCACSLA.Models;

namespace mvcCACSLA.Controllers
{
    public class Indicadores2Controller : Controller
    {
        private DB_9F6318_fcacacslav2Entities1 db = new DB_9F6318_fcacacslav2Entities1();

        // GET: Indicadores2
        public ActionResult Index()
        {
            return View(db.Indicadores.ToList());
        }

        // GET: Indicadores2/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Indicadores indicadores = db.Indicadores.Find(id);
            if (indicadores == null)
            {
                return HttpNotFound();
            }
            return View(indicadores);
        }

        // GET: Indicadores2/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Indicadores2/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdEstandar,IdVariable,DescripcionEstandar,DescripcionVariable,Referente,DescripcionReferente,Anexo,ValorMax,ValorObt,IdCoordinacion")] Indicadores indicadores)
        {
            if (ModelState.IsValid)
            {
                db.Indicadores.Add(indicadores);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(indicadores);
        }

        // GET: Indicadores2/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Indicadores indicadores = db.Indicadores.Find(id);
            if (indicadores == null)
            {
                return HttpNotFound();
            }
            return View(indicadores);
        }

        // POST: Indicadores2/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdEstandar,IdVariable,DescripcionEstandar,DescripcionVariable,Referente,DescripcionReferente,Anexo,ValorMax,ValorObt,IdCoordinacion")] Indicadores indicadores)
        {
            if (ModelState.IsValid)
            {
                db.Entry(indicadores).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(indicadores);
        }

        // GET: Indicadores2/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Indicadores indicadores = db.Indicadores.Find(id);
            if (indicadores == null)
            {
                return HttpNotFound();
            }
            return View(indicadores);
        }

        // POST: Indicadores2/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Indicadores indicadores = db.Indicadores.Find(id);
            db.Indicadores.Remove(indicadores);
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
