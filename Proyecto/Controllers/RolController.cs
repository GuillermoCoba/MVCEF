using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyecto.Models;

namespace Proyecto.Controllers
{
    public class RolController : Controller
    {
        // GET: Rol
        public ActionResult Index()
        {
            List<RolCLS> lista = new List<RolCLS>();
            using (var bd = new BDPasajeEntities())
            {
                lista = (from rol in bd.Rol
                         where rol.BHABILITADO == 1
                         select new RolCLS
                         {
                             iidRol = rol.IIDROL,
                             nombre = rol.NOMBRE,
                             descripcion = rol.DESCRIPCION
                         }).ToList();
            }
            return View(lista);
        }
        public ActionResult Filtro(string nombreRol)
        {
            List<RolCLS> lista = new List<RolCLS>();
            if (nombreRol == null)
            {
                using (var db = new BDPasajeEntities())
                {
                    lista = (from rol in db.Rol
                             where rol.BHABILITADO == 1
                             select new RolCLS
                             {
                                 iidRol = rol.IIDROL,
                                 nombre = rol.NOMBRE,
                                 descripcion = rol.DESCRIPCION
                             }
                             ).ToList();
                }
            }
            else
            {
                using (var db = new BDPasajeEntities())
                {
                    lista = (from rol in db.Rol
                             where rol.BHABILITADO == 1 && rol.NOMBRE.Contains(nombreRol)
                             select new RolCLS
                             {
                                 iidRol = rol.IIDROL,
                                 nombre = rol.NOMBRE,
                                 descripcion = rol.DESCRIPCION
                             }
                             ).ToList();
                }
            }
            return PartialView("_TablaRol", lista);
        }
        public string Guardar(RolCLS oRolCLS, int titulo)
        {
            string respuesta = "";
            try
            {
                if (!ModelState.IsValid)
                {
                    var query = (from state in ModelState.Values
                                 from error in state.Errors
                                 select error.ErrorMessage).ToList();
                    respuesta += "<ul class='list-group'>";
                    foreach (var item in query)
                    {
                        respuesta += "<li class='list-group-item'>" + item + "</li>";
                    }
                    respuesta += "</ul>";

                }
                else
                {
                    using (var db = new BDPasajeEntities())
                    {
                        if (titulo.Equals(-1))
                        {
                            Rol oRol = new Rol();
                            oRol.NOMBRE = oRolCLS.nombre;
                            oRol.DESCRIPCION = oRolCLS.descripcion;
                            oRol.BHABILITADO = 1;
                            db.Rol.Add(oRol);
                            respuesta = db.SaveChanges().ToString();
                            if (respuesta == "0") respuesta = "";
                            
                        }
                        else
                        {
                            Rol oRol = db.Rol.Where(p => p.IIDROL == titulo).First();
                            oRol.NOMBRE = oRolCLS.nombre;
                            oRol.DESCRIPCION = oRolCLS.descripcion;
                            respuesta = db.SaveChanges().ToString();

                        }
                    }
                }
            }
            catch(Exception ex)
            {
                respuesta = "";
            }
            return respuesta; 
        } 
        public JsonResult recuperarDatos(int titulo)
        {
            RolCLS oRolCLS = new RolCLS();
            using (var db = new BDPasajeEntities())
            {
                Rol oRol = db.Rol.Where(p => p.IIDROL == titulo).First();
                oRolCLS.nombre = oRol.NOMBRE;
                oRolCLS.descripcion = oRol.DESCRIPCION;
            }
            return Json(oRolCLS,JsonRequestBehavior.AllowGet);
        }
    }
}