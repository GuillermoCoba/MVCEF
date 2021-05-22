using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyecto.Models;

namespace Proyecto.Controllers
{
    public class RolPaginaController : Controller
    {
        public void listaRol()
        {
            List<SelectListItem> lista;
            using (var db = new BDPasajeEntities())
            {
                lista = (from item in db.Rol
                         where item.BHABILITADO == 1
                         select new SelectListItem
                         {
                             Text = item.NOMBRE,
                             Value = item.IIDROL.ToString()
                         }).ToList();
                ViewBag.listaRol = lista;
                lista.Insert(0, new SelectListItem { Text = "--Seleccione--", Value = "" });
            }
        }
        public void listaPagina()
        {
            List<SelectListItem> lista;
            using (var db = new BDPasajeEntities())
            {
                lista = (from item in db.Pagina
                         where item.BHABILITADO == 1
                         select new SelectListItem
                         {
                             Text = item.MENSAJE,
                             Value = item.IIDPAGINA.ToString()
                         }).ToList();
                ViewBag.listaPagina = lista;
                lista.Insert(0, new SelectListItem { Text = "--Seleccione--", Value = "" });
            }
        }
        // GET: RolPagina
        public ActionResult Index()
        {
            listaRol();
            listaPagina();
            List<RolPaginaCLS> lista = new List<RolPaginaCLS>();
            using (var db = new BDPasajeEntities())
            {
                lista = (from rolpagina in db.RolPagina
                         join rol in db.Rol
                         on rolpagina.IIDROL equals rol.IIDROL
                         join pagina in db.Pagina
                         on rolpagina.IIDPAGINA equals pagina.IIDPAGINA
                         where rolpagina.BHABILITADO == 1
                         select new RolPaginaCLS
                         {
                             iidrolpagina = rolpagina.IIDROLPAGINA,
                             nombreRol = rol.NOMBRE,
                             nombreMensaje = pagina.MENSAJE

                         }
                         ).ToList();
            }
            return View(lista);
        }

        public ActionResult Filtar(int? iidrol)
        {
            List<RolPaginaCLS> lista = new List<RolPaginaCLS>();
            using (var db = new BDPasajeEntities())
            {
                if (iidrol == null)
                {
                    lista = (from rolpagina in db.RolPagina
                             join rol in db.Rol
                             on rolpagina.IIDROL equals rol.IIDROL
                             join pagina in db.Pagina
                             on rolpagina.IIDPAGINA equals pagina.IIDPAGINA
                             where rolpagina.BHABILITADO == 1
                             select new RolPaginaCLS
                             {
                                 iidrolpagina = rolpagina.IIDROLPAGINA,
                                 nombreRol = rol.NOMBRE,
                                 nombreMensaje = pagina.MENSAJE

                             }
                             ).ToList();
                }
                else
                {
                    lista = (from rolpagina in db.RolPagina
                             join rol in db.Rol
                             on rolpagina.IIDROL equals rol.IIDROL
                             join pagina in db.Pagina
                             on rolpagina.IIDPAGINA equals pagina.IIDPAGINA
                             where rolpagina.BHABILITADO == 1 && rolpagina.IIDROL ==iidrol
                             select new RolPaginaCLS
                             {
                                 iidrolpagina = rolpagina.IIDROLPAGINA,
                                 nombreRol = rol.NOMBRE,
                                 nombreMensaje = pagina.MENSAJE

                             }
                             ).ToList();
                }
            }

            return PartialView("_TablaRolPagina",lista);
        }
        public int Guardar(RolPaginaCLS oRolPaginaCLS, int titulo)
        {
            int rpta = 0;
            using(var db = new BDPasajeEntities())
            {
                if(titulo == 1)
                {
                    RolPagina oRolPagina = new RolPagina();
                    oRolPagina.IIDROL = oRolPaginaCLS.iidrol;
                    oRolPagina.IIDPAGINA = oRolPaginaCLS.iidpagina;
                    oRolPagina.BHABILITADO = 1;
                    db.RolPagina.Add(oRolPagina);
                    rpta =  db.SaveChanges();
                }
            }

            return rpta;
        }
    }
}