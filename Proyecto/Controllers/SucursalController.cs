using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyecto.Models;
namespace Proyecto.Controllers
{
    public class SucursalController : Controller
    {
        // GET: Sucursal
        public ActionResult Index(SucursalCLS oSucursalCLS)
        {
            List<SucursalCLS> listaSucursal = null;
            using (var db = new BDPasajeEntities())
            {
                if (oSucursalCLS.nombre == null)
                {
                    listaSucursal = (from sucursal in db.Sucursal
                                     where sucursal.BHABILITADO == 1
                                     select new SucursalCLS
                                     {
                                         iidsucursal = sucursal.IIDSUCURSAL,
                                         nombre = sucursal.NOMBRE,
                                         telefono = sucursal.TELEFONO,
                                         email = sucursal.EMAIL

                                     }).ToList();
                }else
                {
                    listaSucursal = (from sucursal in db.Sucursal
                                     where sucursal.BHABILITADO==1 && sucursal.NOMBRE.Contains(oSucursalCLS.nombre)
                                     select new SucursalCLS
                                     {
                                         iidsucursal = sucursal.IIDSUCURSAL,
                                         nombre = sucursal.NOMBRE,
                                         telefono = sucursal.TELEFONO,
                                         email = sucursal.EMAIL

                                     }).ToList();
                }
            }
            return View(listaSucursal);
        }
        public ActionResult Agregar()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Agregar(SucursalCLS oSucursalCLS)
        {
            int n=0;
            string nombre = oSucursalCLS.nombre;
            using (var db = new BDPasajeEntities())
            {
                n = db.Marca.Where(p => p.NOMBRE.Equals(nombre)).Count();
            }
            if (!ModelState.IsValid || n>0)
            {
                if (n > 0) oSucursalCLS.mensajeError = "Ya existe esta sucursal";
                return View(oSucursalCLS);
            }
            else
            {

                using (var db = new BDPasajeEntities())
                {
                    Sucursal oSucursal = new Sucursal();
                    oSucursal.NOMBRE = oSucursalCLS.nombre;
                    oSucursal.DIRECCION = oSucursalCLS.direccion;
                    oSucursal.TELEFONO = oSucursalCLS.telefono;
                    oSucursal.EMAIL = oSucursalCLS.email;
                    oSucursal.FECHAAPERTURA = oSucursalCLS.fechaApertura;
                    oSucursal.BHABILITADO = 1;
                    db.Sucursal.Add(oSucursal);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
        }
        public ActionResult Editar(int id)
        {
            SucursalCLS oSucursalCLS = new SucursalCLS();
            using (var db = new BDPasajeEntities())
            {
                Sucursal oSucursal = db.Sucursal.Where(p => p.IIDSUCURSAL.Equals(id)).First();
                oSucursalCLS.iidsucursal = oSucursal.IIDSUCURSAL;
                oSucursalCLS.nombre = oSucursal.NOMBRE;
                oSucursalCLS.direccion = oSucursal.DIRECCION;
                oSucursalCLS.telefono = oSucursal.TELEFONO;
                oSucursalCLS.email = oSucursal.EMAIL;
                oSucursalCLS.fechaApertura = (DateTime)oSucursal.FECHAAPERTURA;

            }
            return View(oSucursalCLS);
        }
        [HttpPost]
        public ActionResult Editar(SucursalCLS oSucursalCLS)
        {
            int n = 0;
            string nombre=oSucursalCLS.nombre;
            int id = oSucursalCLS.iidsucursal;
            using (var db = new BDPasajeEntities())
            {
                n = db.Sucursal.Where(p => p.NOMBRE.Equals(nombre)&& !p.IIDSUCURSAL.Equals(id)).Count();
            }
                if (!ModelState.IsValid || n>0)
                {
                if (n > 0) oSucursalCLS.mensajeError = "Ya existe esta sucursal";
                    return View(oSucursalCLS);
                } else
                {
                    using (var db = new BDPasajeEntities())
                    {
                        Sucursal oSucursal = db.Sucursal.Where(p => p.IIDSUCURSAL.Equals(id)).First();
                        oSucursal.NOMBRE = oSucursalCLS.nombre;
                        oSucursal.DIRECCION = oSucursalCLS.direccion;
                        oSucursal.TELEFONO = oSucursalCLS.telefono;
                        oSucursal.EMAIL = oSucursalCLS.email;
                        oSucursal.FECHAAPERTURA = oSucursalCLS.fechaApertura;
                        db.SaveChanges();
                    }
                    return RedirectToAction("Index");
                }
        }
        public ActionResult Eliminar(int id)
        {
            using(var db = new BDPasajeEntities())
            {
                Sucursal oSucursal = db.Sucursal.Where(p => p.IIDSUCURSAL.Equals(id)).First();
                oSucursal.BHABILITADO = 0;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}