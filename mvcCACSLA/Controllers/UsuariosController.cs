
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

namespace mvcCACSLA.Controllers
{
    [Authorize]
    [AuthorizeRoles("Administrador")]
    public class UsuariosController : Controller
    {
        private DB_9F6318_fcacacslav2Entities1 db = new DB_9F6318_fcacacslav2Entities1();

        // GET: Usuarios
        public ActionResult Index(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            var usuarios = db.Usuarios.Include(u => u.Carreras).Include(u => u.Usuarios_niveles).Include(u => u.Coordinaciones).OrderBy(u => u.IdUsuario);
            //return View(db.Usuarios.ToList());
            //return View("Index");
           return View(usuarios.ToPagedList(pageNumber, pageSize));
        }

        // GET: Usuarios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuarios usuarios = db.Usuarios.Find(id);
            if (usuarios == null)
            {
                return HttpNotFound();
            }
            return View(usuarios);
        }

        // GET: Usuarios/Create
        public ActionResult Create()
        {
            ViewBag.IdCarrera = new SelectList(db.Carreras, "IdCarrera", "Descripcion");
            ViewBag.IdNivel = new SelectList(db.Usuarios_niveles, "IdNivel", "Descripcion");
            ViewBag.IdCoordinacion = new SelectList(db.Coordinaciones, "IdCoordinacion", "Descripcion");
            return View();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdUsuario,Login,Pass,Descripcion,IdNivel,Activo,IdCarrera,IdCoordinacion,SubeCarreras")] Usuarios usuarios)
        {
            if (ModelState.IsValid)
            {
                db.Usuarios.Add(usuarios);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdCarrera = new SelectList(db.Carreras, "IdCarrera", "Descripcion", usuarios.IdCarrera);
            ViewBag.IdNivel = new SelectList(db.Usuarios_niveles, "IdNivel", "Descripcion", usuarios.IdNivel);
            ViewBag.IdCoordinacion = new SelectList(db.Coordinaciones, "IdCoordinacion", "Descripcion", usuarios.IdCoordinacion);
            return View(usuarios);
        }

        // GET: Usuarios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuarios usuarios = db.Usuarios.Find(id);
            if (usuarios == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdCarrera = new SelectList(db.Carreras, "IdCarrera", "Descripcion", usuarios.IdCarrera);
            ViewBag.IdNivel = new SelectList(db.Usuarios_niveles, "IdNivel", "Descripcion", usuarios.IdNivel);
            ViewBag.IdCoordinacion = new SelectList(db.Coordinaciones, "IdCoordinacion", "Descripcion", usuarios.IdCoordinacion);
            return View(usuarios);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdUsuario,Login,Pass,Descripcion,IdNivel,Activo,IdCarrera,IdCoordinacion,SubeCarreras")] Usuarios usuarios)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usuarios).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdCarrera = new SelectList(db.Carreras, "IdCarrera", "Descripcion", usuarios.IdCarrera);
            ViewBag.IdNivel = new SelectList(db.Usuarios_niveles, "IdNivel", "Descripcion", usuarios.IdNivel);
            ViewBag.IdCoordinacion = new SelectList(db.Coordinaciones, "IdCoordinacion", "Descripcion", usuarios.IdCoordinacion);
            return View(usuarios);
        }

        // GET: Usuarios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuarios usuarios = db.Usuarios.Find(id);
            if (usuarios == null)
            {
                return HttpNotFound();
            }
            return View(usuarios);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Usuarios usuarios = db.Usuarios.Find(id);
            db.Usuarios.Remove(usuarios);
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
