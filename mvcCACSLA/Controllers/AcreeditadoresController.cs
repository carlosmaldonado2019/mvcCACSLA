using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mvcCACSLA.Models;
using mvcCACSLA.Security;
using mvcCACSLA.Models.Constantes;

namespace mvcCACSLA.Controllers
{
    [Authorize]
    [AuthorizeRoles("Acreeditador")]
    public class AcreeditadoresController : Controller
    {
        private DB_9F6318_fcacacslav2Entities1 db = new DB_9F6318_fcacacslav2Entities1();
        // GET: Acreeditadores
        public ActionResult Index()
        {
            var indicador = (from a in db.Indicadores
                             select a).Where(a => a.Observado ==true).OrderBy(a => a.Referente).ThenBy(a => a.IdVariable);
            ViewBag.Title = "Indicadores Observados";
            return View(indicador.ToList());
        }

        // GET: Acreeditadores/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Acreeditadores/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Acreeditadores/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Acreeditadores/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Acreeditadores/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Acreeditadores/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Acreeditadores/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
