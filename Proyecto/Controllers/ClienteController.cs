using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyecto.Models;

namespace Proyecto.Controllers
{
    public class ClienteController : Controller
    {
        // GET: Cliente
        public ActionResult Index(ClienteCLS oClienteCLS)
        {
            List<ClienteCLS> listaCliente = null;
            llenarSexo();
            ViewBag.lista = listaSexo;
            using (var db = new BDPasajeEntities())
            {
                if (oClienteCLS.iidsexo == 0)
                {
                    listaCliente = (from cliente in db.Cliente
                                    where cliente.BHABILITADO == 1
                                    select new ClienteCLS
                                    {
                                        iidcliente = cliente.IIDCLIENTE,
                                        nombre = cliente.NOMBRE,
                                        appaterno = cliente.APPATERNO,
                                        apmaterno = cliente.APMATERNO,
                                        telefonofijo = cliente.TELEFONOFIJO
                                    }
                           ).ToList();
                }else
                {
                    listaCliente = (from cliente in db.Cliente
                                    where cliente.BHABILITADO == 1 && cliente.IIDSEXO == oClienteCLS.iidsexo
                                    select new ClienteCLS
                                    {
                                        iidcliente = cliente.IIDCLIENTE,
                                        nombre = cliente.NOMBRE,
                                        appaterno = cliente.APPATERNO,
                                        apmaterno = cliente.APMATERNO,
                                        telefonofijo = cliente.TELEFONOFIJO
                                    }
                                             ).ToList();
                }
            }
            return View(listaCliente);
        }
        List<SelectListItem> listaSexo;
        private void llenarSexo() {
            using (var bd = new BDPasajeEntities())
            {
                listaSexo = (from sexo in bd.Sexo
                             where sexo.BHABILITADO == 1
                             select new SelectListItem
                             {
                                 Text = sexo.NOMBRE,
                                 Value = sexo.IIDSEXO.ToString()
                             }).ToList();
                listaSexo.Insert(0, new SelectListItem { Text = "--Seleccione--", Value = "" });
            }
        }
        public ActionResult Agregar()
        {
            llenarSexo();
            ViewBag.lista = listaSexo;
            return View();
        }
        [HttpPost]
        public ActionResult Agregar(ClienteCLS oClienteCLS)
        {
            int n = 0;
            string nombre = oClienteCLS.nombre;
            string app = oClienteCLS.appaterno;
            string apm = oClienteCLS.apmaterno;
            
           using(var db = new BDPasajeEntities())
            {
                n = db.Cliente.Where(p => p.NOMBRE.Equals(nombre) && p.APPATERNO.Equals(app) && p.APMATERNO.Equals(apm)).Count();
            }
                if (!ModelState.IsValid || n>0)
                {
                if (n > 0) oClienteCLS.mensajeError = "Ya existe este usuario";
                    llenarSexo();
                    ViewBag.lista = listaSexo;
                    return View(oClienteCLS);
                }
                else
                {
                    using (var db = new BDPasajeEntities())
                    {
                    Cliente oCliente = new Cliente();
                    oCliente.NOMBRE = oClienteCLS.nombre;
                    oCliente.APPATERNO = oClienteCLS.appaterno;
                    oCliente.APMATERNO = oClienteCLS.apmaterno;
                    oCliente.EMAIL = oClienteCLS.email;
                    oCliente.DIRECCION = oClienteCLS.direccion;
                    oCliente.IIDSEXO = oClienteCLS.iidsexo;
                    oCliente.TELEFONOCELULAR = oClienteCLS.telefonocelular;
                    oCliente.TELEFONOFIJO = oClienteCLS.telefonofijo;
                    oCliente.BHABILITADO = 1;
                    db.Cliente.Add(oCliente);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");

            }
        }
        public ActionResult Editar(int id)
        {
            ClienteCLS oCLienteCLS = new ClienteCLS();
            using (var db = new BDPasajeEntities())
            {
                llenarSexo();
                ViewBag.lista = listaSexo;
                Cliente oCliente = db.Cliente.Where(p => p.IIDCLIENTE.Equals(id)).First();
                oCLienteCLS.iidcliente = oCliente.IIDCLIENTE;
                oCLienteCLS.nombre = oCliente.NOMBRE;
                oCLienteCLS.appaterno = oCliente.APPATERNO;
                oCLienteCLS.apmaterno = oCliente.APMATERNO;
                oCLienteCLS.direccion = oCliente.DIRECCION;
                oCLienteCLS.email = oCliente.EMAIL;
                oCLienteCLS.iidsexo = (int)oCliente.IIDSEXO;
                oCLienteCLS.telefonocelular = oCliente.TELEFONOCELULAR;
                oCLienteCLS.telefonofijo = oCliente.TELEFONOFIJO;
            }
            return View(oCLienteCLS);
        }
        [HttpPost]
        public ActionResult Editar(ClienteCLS oClienteCLS)
        {
            string nombre = oClienteCLS.nombre;
            string app = oClienteCLS.appaterno;
            string apm = oClienteCLS.apmaterno;
            int n = 0;
            int id = oClienteCLS.iidcliente;
            using (var db = new BDPasajeEntities())
            {
                n = db.Cliente.Where(p => p.NOMBRE.Equals(nombre) && p.APPATERNO.Equals(app) && p.APMATERNO.Equals(apm) && !p.IIDCLIENTE.Equals(id)).Count();
            }
                if (!ModelState.IsValid || n>0)
                {
                if (n > 0) oClienteCLS.mensajeError = "Este cliente ya existe";
                    llenarSexo();
                    ViewBag.lista = listaSexo;
                    return View(oClienteCLS);
                } else
                {
                    using (var db = new BDPasajeEntities())
                    {
                        Cliente oCliente = db.Cliente.Where(p => p.IIDCLIENTE.Equals(id)).First();
                        oCliente.NOMBRE = oClienteCLS.nombre;
                        oCliente.APPATERNO = oClienteCLS.appaterno;
                        oCliente.APMATERNO = oClienteCLS.apmaterno;
                        oCliente.EMAIL = oClienteCLS.email;
                        oCliente.DIRECCION = oClienteCLS.direccion;
                        oCliente.IIDSEXO = oClienteCLS.iidsexo;
                        oCliente.TELEFONOCELULAR = oClienteCLS.telefonocelular;
                        oCliente.TELEFONOFIJO = oClienteCLS.telefonofijo;
                        db.SaveChanges();
                    }
                    return RedirectToAction("Index");
                }
        }
        public ActionResult Eliminar (int iidcliente)
        {
            using(var db = new BDPasajeEntities())
            {
                Cliente oCliente = db.Cliente.Where(p => p.IIDCLIENTE.Equals(iidcliente)).First();
                oCliente.BHABILITADO = 0;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}