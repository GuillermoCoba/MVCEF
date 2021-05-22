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
            using(var bd = new BDPasajeEntities())
            {
                lista = (from rol in bd.Rol
                         where rol.BHABILITADO==1
                         select new RolCLS
                         {
                             iidRol = rol.IIDROL, 
                             nombre = rol.NOMBRE,
                             descripcion=rol.DESCRIPCION
                         }).ToList();
            }
            return View(lista);
        }
        public ActionResult Filtro (string nombre)
        {
            List<RolCLS> lista = new List<RolCLS>();
           if(nombre == null)
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
                             where rol.BHABILITADO == 1 && rol.NOMBRE.Contains(nombre)
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
        public int Guardar(RolCLS oRolCLS, int titulo)
        {
            int respuesta = 0;
            using (var db = new BDPasajeEntities())
            {
                if (titulo.Equals(1))
                {
                    Rol oRol = new Rol();
                    oRol.NOMBRE = oRolCLS.nombre;
                    oRol.DESCRIPCION = oRolCLS.descripcion;
                    oRol.BHABILITADO = 1;
                    db.Rol.Add(oRol);
                    respuesta = db.SaveChanges();
                }
            }
                return respuesta;
        }
    }
}