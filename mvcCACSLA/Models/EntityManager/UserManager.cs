using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;

namespace mvcCACSLA.Models
{
    public class UserManager
    {
        public string GetUserPassword(string loginName)
        {

            using (DB_9F6318_fcacacslav2Entities1 db = new DB_9F6318_fcacacslav2Entities1())
            {
                try
                {
                    var user = db.Usuarios.Where(o => o.Login.Equals(loginName));
                    if (user.Any())
                        return user.FirstOrDefault().Pass;
                    else
                        return string.Empty;
                }
                catch(SqlException ex)
                {
                    throw new InvalidOperationException("No se puede conectar a la base de datos!", ex);
                }
            }

            //return string.Empty;




        }

        public int GetUserCarrera(string loginName)
        {
            using (DB_9F6318_fcacacslav2Entities1 db = new DB_9F6318_fcacacslav2Entities1())
            {
                var carrera = db.Usuarios.Where(o => o.Login.Equals(loginName));
                if (carrera.Any())
                    return carrera.FirstOrDefault().IdCarrera;
                else
                    return 0;
            }

        }

        public int GetUserID(string loginName)
        {
            using (DB_9F6318_fcacacslav2Entities1 db = new DB_9F6318_fcacacslav2Entities1())
            {

                var iduser = db.Usuarios.Where(o => o.Login.Equals(loginName));
                if (iduser.Any())
                    return iduser.FirstOrDefault().IdUsuario;
                else
                    return 0;
            }
        }

        public int GetUserCoordinacion(String loginName)
        {
            using (DB_9F6318_fcacacslav2Entities1 db = new DB_9F6318_fcacacslav2Entities1())
            {
                var idcoord = db.Usuarios.Where(o => o.Login.Equals(loginName.ToUpper()));
                if (idcoord.Any())
                    return idcoord.FirstOrDefault().IdCoordinacion;
                else
                    return 0;
            }

        }

        public bool GetUserSubeCarreras(string loginName)
        {
            using (DB_9F6318_fcacacslav2Entities1 db = new DB_9F6318_fcacacslav2Entities1())
            {

                var sube = db.Usuarios.Where(o => o.Login.Equals(loginName.ToUpper()));
                if (sube.Any())
                    return Convert.ToBoolean(sube.FirstOrDefault().SubeCarreras);
                else
                    return false;
            }

        }

        public string GetUserRole(string loginName)
        {
            using (DB_9F6318_fcacacslav2Entities1 db = new DB_9F6318_fcacacslav2Entities1())
            {
                Usuarios us = db.Usuarios.Where(a => a.Login.Equals(loginName))?.FirstOrDefault();
                Usuarios_niveles niveles = db.Usuarios_niveles.Where(a => a.IdNivel == us.IdNivel).FirstOrDefault();


                if (niveles != null)
                {
                    return niveles.Descripcion;
                }
            }
            return string.Empty;
        }

        public bool IsUserInRole(string loginName, string roleName)
        {
            using (DB_9F6318_fcacacslav2Entities1 db = new DB_9F6318_fcacacslav2Entities1())
            {
                Usuarios us = db.Usuarios.Where(a => a.Login.Equals(loginName))?.FirstOrDefault();
                if (us != null)
                {
                    var roles = from q in db.Usuarios_niveles
                                join r in db.Usuarios on q.IdNivel equals r.IdNivel
                                where q.Descripcion.Equals(roleName) && r.IdUsuario.Equals(us.IdUsuario)
                                select q.Descripcion;
                    if (roles != null)
                    {
                        return roles.Any();
                    }
                }
            }
            return false;
        }
    }
}