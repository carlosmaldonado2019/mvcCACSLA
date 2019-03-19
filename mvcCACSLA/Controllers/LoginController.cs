using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using mvcCACSLA.Models;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;

namespace mvcCACSLA.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Login u)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UserManager UM = new UserManager();
                    string password = UM.GetUserPassword(u.Usuario.ToUpper());

                    if (string.IsNullOrEmpty(password))
                        return Json(new { success = false });
                    //ModelState.AddModelError("", "Usuario y/o Password Incorrecto");
                    else
                    {
                        if (u.Password.Equals(password))
                        {
                            FormsAuthentication.SetAuthCookie(u.Usuario, false);
                            return Json(new { success = true });
                            //return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            return Json(new { success = false });
                            //ModelState.AddModelError("", "Password Incorrecto");
                        }
                    }

                }
            }

            catch(SqlException ex)
            {
                throw new InvalidOperationException("No se puede conectar a la base de datos!", ex);
            }
            return View(u);

        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

    }
}