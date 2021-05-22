using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyecto.Models;
namespace Proyecto.Controllers
{
    public class EmpleadoController : Controller
    {
        // GET: Empleado
        public ActionResult Index(EmpleadoCLS oEmpleadoCLS) 
        {
            int idTipoUsuario = oEmpleadoCLS.iidtipoUsuario;
            List<EmpleadoCLS> listaEmpleado = null;
            listaUsuario();
            using (var db = new BDPasajeEntities())
            {
                if (idTipoUsuario == 0)
                {
                    listaEmpleado = (from empleado in db.Empleado
                                     join tipousuario in db.TipoUsuario
                                     on empleado.IIDTIPOUSUARIO equals tipousuario.IIDTIPOUSUARIO
                                     join tipocontrato in db.TipoContrato
                                     on empleado.IIDTIPOCONTRATO equals tipocontrato.IIDTIPOCONTRATO
                                     where empleado.BHABILITADO == 1
                                     select new EmpleadoCLS
                                     {
                                         iidEmpleado = empleado.IIDEMPLEADO,
                                         nombre = empleado.NOMBRE,
                                         apPaterno = empleado.APPATERNO,
                                         apMaterno = empleado.APMATERNO,
                                         nombreTipoUsuario = tipousuario.NOMBRE,
                                         nombreTipoContrato = tipocontrato.NOMBRE
                                     }
                                     ).ToList();
                }else
                {
                    listaEmpleado = (from empleado in db.Empleado
                                     join tipousuario in db.TipoUsuario
                                     on empleado.IIDTIPOUSUARIO equals tipousuario.IIDTIPOUSUARIO
                                     join tipocontrato in db.TipoContrato
                                     on empleado.IIDTIPOCONTRATO equals tipocontrato.IIDTIPOCONTRATO
                                     where empleado.BHABILITADO == 1 && empleado.IIDTIPOUSUARIO == idTipoUsuario
                                     select new EmpleadoCLS
                                     {
                                         iidEmpleado = empleado.IIDEMPLEADO,
                                         nombre = empleado.NOMBRE,
                                         apPaterno = empleado.APPATERNO,
                                         apMaterno = empleado.APMATERNO,
                                         nombreTipoUsuario = tipousuario.NOMBRE,
                                         nombreTipoContrato = tipocontrato.NOMBRE
                                     }
                                                       ).ToList();
                }
            }
            return View(listaEmpleado);
        }
        public ActionResult Agregar()
        {
            llenar();
            return View();
        }
        [HttpPost]
        public ActionResult Agregar(EmpleadoCLS oEmpleadoCLS)
        {
            int n = 0;
            string nombre = oEmpleadoCLS.nombre;
            string app = oEmpleadoCLS.apPaterno;
            string apm = oEmpleadoCLS.apMaterno;
            using (var db = new BDPasajeEntities())
            {
                n = db.Empleado.Where(p => p.NOMBRE.Equals(nombre) && p.APPATERNO.Equals(app) && p.APMATERNO.Equals(apm)).Count();
            }

            if (!ModelState.IsValid || n>0)
                {
                if (n > 0) oEmpleadoCLS.mensajeError = "Ya existe este empleado";
                    llenar();
                    return View(oEmpleadoCLS);
                } else
                {
                    Empleado oEmpleado = new Empleado();
                    using (var db = new BDPasajeEntities())
                    {
                        oEmpleado.NOMBRE = oEmpleadoCLS.nombre;
                        oEmpleado.APPATERNO = oEmpleadoCLS.apPaterno;
                        oEmpleado.APMATERNO = oEmpleadoCLS.apMaterno;
                        oEmpleado.FECHACONTRATO = oEmpleadoCLS.fechaContrato;
                        oEmpleado.SUELDO = oEmpleadoCLS.sueldo;
                        oEmpleado.IIDTIPOUSUARIO = oEmpleadoCLS.iidtipoUsuario;
                        oEmpleado.IIDTIPOCONTRATO = oEmpleadoCLS.iidtipoContrato;
                        oEmpleado.IIDSEXO = oEmpleadoCLS.iidsexo;
                        oEmpleado.BHABILITADO = 1;
                        db.Empleado.Add(oEmpleado);
                        db.SaveChanges();

                    }
                    return RedirectToAction("Index");
                }
           
        }
        public ActionResult Editar(int id)
        {
            EmpleadoCLS oEmpleadoCLS = new EmpleadoCLS();
            using(var db = new BDPasajeEntities())
            {
                llenar();
                Empleado oEmpleado = db.Empleado.Where(p => p.IIDEMPLEADO.Equals(id)).First();
                oEmpleadoCLS.iidEmpleado = oEmpleado.IIDEMPLEADO;
                oEmpleadoCLS.nombre = oEmpleado.NOMBRE;
                oEmpleadoCLS.apPaterno = oEmpleado.APPATERNO;
                oEmpleadoCLS.apMaterno = oEmpleado.APMATERNO;
                oEmpleadoCLS.fechaContrato = (DateTime)oEmpleado.FECHACONTRATO;
                oEmpleadoCLS.sueldo = (decimal)oEmpleado.SUELDO;
                oEmpleadoCLS.iidtipoUsuario = (int)oEmpleado.IIDTIPOUSUARIO;
                oEmpleadoCLS.iidtipoContrato = (int)oEmpleado.IIDTIPOCONTRATO;
                oEmpleadoCLS.iidsexo = (int)oEmpleado.IIDSEXO;

            }
            return View(oEmpleadoCLS);
        }
        [HttpPost]
        public ActionResult Editar(EmpleadoCLS oEmpleadoCLS)
        {
            int id = oEmpleadoCLS.iidEmpleado;
            int n = 0;
            string nombre = oEmpleadoCLS.nombre;
            string app = oEmpleadoCLS.apPaterno;
            string apm = oEmpleadoCLS.apMaterno;
            using (var db = new BDPasajeEntities())
            {
                n = db.Empleado.Where(p => p.NOMBRE.Equals(nombre) && p.APPATERNO.Equals(app) && p.APMATERNO.Equals(apm)&& !p.IIDEMPLEADO.Equals(id)).Count();
            }
            if (!ModelState.IsValid || n > 0)
            {
                if (n > 0) oEmpleadoCLS.mensajeError = "Ya existe este empleado";
                llenar();
                return View(oEmpleadoCLS);
            }
            else
            {
                using(var db = new BDPasajeEntities())
                {
                    Empleado oEmpleado = db.Empleado.Where(p => p.IIDEMPLEADO.Equals(id)).First();
                    oEmpleado.NOMBRE = oEmpleadoCLS.nombre;
                    oEmpleado.APPATERNO = oEmpleadoCLS.apPaterno;
                    oEmpleado.APMATERNO = oEmpleadoCLS.apMaterno;
                    oEmpleado.FECHACONTRATO = oEmpleadoCLS.fechaContrato;
                    oEmpleado.SUELDO = oEmpleadoCLS.sueldo;
                    oEmpleado.IIDTIPOUSUARIO = oEmpleadoCLS.iidtipoUsuario;
                    oEmpleado.IIDTIPOCONTRATO = oEmpleadoCLS.iidtipoContrato;
                    oEmpleado.IIDSEXO = oEmpleadoCLS.iidsexo;
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
        }
        public void listaSexo()
        {
            List<SelectListItem> lista;
            using(var db = new BDPasajeEntities())
            {
                lista = (from sexo in db.Sexo
                         where sexo.BHABILITADO == 1
                         select new SelectListItem
                         {
                             Text = sexo.NOMBRE,
                             Value = sexo.IIDSEXO.ToString()
                         }).ToList();
                lista.Insert(0, new SelectListItem { Text = "--Seleccione--", Value = "" });
                ViewBag.listaSexo = lista;
            }
        }
        public void listaContrato()
        {
            List<SelectListItem> lista;
            using(var db = new BDPasajeEntities())
            {
                lista = (from contrato in db.TipoContrato
                         where contrato.BHABILITADO == 1
                         select new SelectListItem
                         {
                             Text = contrato.NOMBRE,
                             Value = contrato.IIDTIPOCONTRATO.ToString()
                         }).ToList();
                ViewBag.listaContrato = lista;
                lista.Insert(0, new SelectListItem {Text="--Seleccione--",Value="" });
            }
        }
        public void listaUsuario()
        {
            List<SelectListItem> lista;
            using (var db = new BDPasajeEntities())
            {
                lista = (from usuario in db.TipoUsuario
                         where usuario.BHABILITADO == 1
                         select new SelectListItem
                         {
                             Text = usuario.NOMBRE,
                             Value = usuario.IIDTIPOUSUARIO.ToString()
                         }).ToList();
                ViewBag.listaUsuario = lista;
                lista.Insert(0, new SelectListItem { Text = "--Seleccione--", Value = "" });
            }
        }
        public void llenar()
        {
            listaSexo();
            listaContrato();
            listaUsuario();

        }
        [HttpPost]
        public ActionResult Eliminar(int iidEmpleado)
        {
            using (var db = new BDPasajeEntities())
            {
                Empleado oEmpleado = db.Empleado.Where(p => p.IIDEMPLEADO.Equals(iidEmpleado)).First();
                oEmpleado.BHABILITADO = 0;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}