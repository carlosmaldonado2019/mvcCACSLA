
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Security;
using PagedList;
using System.Web.Mvc;
using mvcCACSLA.Models;

using mvcCACSLA.Security;
using mvcCACSLA.Models.Constantes;


namespace mvcCACSLA.Controllers
{
    [Authorize]
    public class IndicadoresController : Controller
    {
        private DB_9F6318_fcacacslav2Entities1 db = new DB_9F6318_fcacacslav2Entities1();

        public ActionResult Index(int? id)
        {
            
            var indicador = (from a in db.Indicadores
                            select a).OrderBy(a => a.Referente);

            if (id == null)
            {
                ViewBag.Title = "Estandares";
                return PartialView(indicador.ToList());
            }
            else
            {
                UserManager UM = new UserManager();
                string role = UM.GetUserRole(User.Identity.Name);
                switch (role)
                {
                    case EstandarNum.RoleAdmin:
                    case EstandarNum.RoleCoord:              
                            indicador = indicador.Where(a => a.IdEstandar == id).OrderBy(a => a.Referente).ThenBy(a => a.IdVariable);
                            break;
                    case EstandarNum.RoleUser:
                        int? idcoord = UM.GetUserCoordinacion(User.Identity.Name);
                        if (idcoord == EstandarNum._Idcoord) //todas las coordinaciones.
                            indicador = indicador.Where(a => a.IdEstandar == id).OrderBy(a => a.Referente).ThenBy(a => a.IdVariable);
                        else
                            indicador = indicador.Where(a => a.IdEstandar == id && a.IdCoordinacion == idcoord).OrderBy(a => a.Referente).ThenBy(a => a.IdVariable);
                        break;
                }

                if (indicador.Count() == 0)
                {
                    return PartialView("NoEncontrado");
                }
                ViewBag.Title = indicador.FirstOrDefault().DescripcionEstandar;

                //return View(indicador.ToList());
                return PartialView(indicador.ToList());
            }
        }


        public ActionResult Edit(int? idestandar, int? idvariable)
        {
            if (idestandar == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Indicadores indicadores = db.Indicadores.Find(idestandar,idvariable);
            if (indicadores == null)
            {
                return HttpNotFound();
            }
            return View(indicadores);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
       
        public ActionResult GuardarValores(string[] valores,string[] variables, int estandar,int pagina)
        {

            int[] id = null;
            int[] id2 = null;
            if (valores != null)
            { 
                id = new int[valores.Length];
                id2 = new int[variables.Length];
                int j = 0;
                for (j = 0; j <= valores.Length-1; j++)
                {
                    int.TryParse(valores[j], out id[j]);
                    int.TryParse(variables[j], out id2[j]);
                    
                    Indicadores indicador = db.Indicadores.Find(estandar, id2[j]);
                    indicador.ValorObt = id[j];
                    db.Entry(indicador).State = EntityState.Modified;
                    db.SaveChanges();
                    
                }

            }
            TempData["Message"] = "Registros Guardados Correctamente!";
            
            return RedirectToAction("Index",new {id = estandar, page = pagina});
        }

        public ActionResult Details(int idestandar,int idvariable)
        {

            Indicadores indicadores = db.Indicadores.Find(idestandar, idvariable);
            
            if (indicadores == null)
            {
                return HttpNotFound();
            }
            return View(indicadores);
        }

        [AuthorizeRoles("Administrador")]
        public ActionResult Mantenimiento()
        {
            var indicador = (from a in db.Indicadores
                             select a).OrderBy(a => a.Referente);
            return View(indicador.ToList());
        }


        public ActionResult Editar(int? idestandar, int? idvariable)
        {
            if (idestandar == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Indicadores indicadores = db.Indicadores.Find(idestandar, idvariable);
            ViewBag.IdCoordinacion = new SelectList(db.Coordinaciones, "IdCoordinacion", "Descripcion",indicadores.IdCoordinacion);
          
            if (indicadores == null)
            {
                return HttpNotFound();
            }
            return View(indicadores);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Indicadores Indicador)
        {
            if (ModelState.IsValid)
            {
                db.Entry(Indicador).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Mantenimiento");
            }
            else
            {

            }
            return View();
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
