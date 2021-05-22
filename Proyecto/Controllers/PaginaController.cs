using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyecto.Models;

namespace Proyecto.Controllers
{
    public class PaginaController : Controller
    {
        // GET: Pagina
        public ActionResult Index()
        {
            List<PaginaCLS> lista = new List<PaginaCLS>();
            using(var db = new BDPasajeEntities())
            {
                lista = (from pagina in db.Pagina
                         where pagina.BHABILITADO==1
                         select new PaginaCLS
                         {
                             iidpagina=pagina.IIDPAGINA,
                             mensaje=pagina.MENSAJE,
                             controlador=pagina.CONTROLADOR,
                             accion=pagina.ACCION
                         }).ToList();

            }
            return View(lista);
        }
        public ActionResult Filtrar(PaginaCLS oPaginaCLS)
        {
            string mensaje = oPaginaCLS.mensaje;
            List<PaginaCLS> lista = new List<PaginaCLS>();
            using (var db = new BDPasajeEntities())
            {
                if(mensaje == null)
                {
                    lista = (from pagina in db.Pagina
                             where pagina.BHABILITADO == 1
                             select new PaginaCLS
                             {
                                 iidpagina = pagina.IIDPAGINA,
                                 mensaje = pagina.MENSAJE,
                                 controlador = pagina.CONTROLADOR,
                                 accion = pagina.ACCION
                             }).ToList();
                }
                else
                {
                    lista = (from pagina in db.Pagina
                             where pagina.BHABILITADO == 1 &&
                             pagina.MENSAJE.Contains(mensaje)
                             select new PaginaCLS
                             {
                                 iidpagina = pagina.IIDPAGINA,
                                 mensaje = pagina.MENSAJE,
                                 controlador = pagina.CONTROLADOR,
                                 accion = pagina.ACCION
                             }).ToList();
                }
            }
            return PartialView("_TablaPagina",lista);
        }
        public int Guardar(PaginaCLS oPaginaCLS,int titulo)
        {
            int rpta = 0;
            using (var db = new BDPasajeEntities())
            {
                if (titulo == 1)
                {
                    Pagina oPagina = new Pagina();
                    oPagina.MENSAJE = oPaginaCLS.mensaje;
                    oPagina.ACCION = oPaginaCLS.accion;
                    oPagina.CONTROLADOR = oPaginaCLS.controlador;
                    oPagina.BHABILITADO = 1;
                    db.Pagina.Add(oPagina);
                    rpta = db.SaveChanges();
                }
            }
            return rpta;

        }
    }
}