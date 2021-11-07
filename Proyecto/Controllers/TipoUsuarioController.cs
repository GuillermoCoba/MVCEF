using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyecto.Models;

namespace Proyecto.Controllers
{
    public class TipoUsuarioController : Controller
    {
        private TipoUsuarioCLS oTipoVal;
        private bool buscarTipoUsuario(TipoUsuarioCLS oTipoUsuarioCLS)
        {
            bool busquedaID = true;
            bool busquedaNombre = true;
            bool busquedaDescripcion = true;
            if (oTipoVal.iidtipousuario > 0)
                busquedaID = oTipoUsuarioCLS.iidtipousuario.ToString().Contains(oTipoVal.iidtipousuario.ToString());
            if (oTipoVal.nombre != null )
                busquedaNombre = oTipoUsuarioCLS.nombre.ToString().Contains(oTipoVal.nombre.ToString());
            if (oTipoVal.descripcion != null)
                busquedaDescripcion = oTipoUsuarioCLS.descripcion.ToString().Contains(oTipoVal.descripcion.ToString());
            return (busquedaID && busquedaNombre && busquedaDescripcion);

        }
        // GET: TipoUsuario
        public ActionResult Index(TipoUsuarioCLS oTipoUsuario)
        {
            oTipoVal = oTipoUsuario;
            List<TipoUsuarioCLS> lista = null;
            List<TipoUsuarioCLS> listafiltrado = null;

            using (var db = new BDPasajeEntities())
            {
                lista = (from tipoUsuario in db.TipoUsuario
                         where tipoUsuario.BHABILITADO == 1
                         select new TipoUsuarioCLS
                         {
                             iidtipousuario = tipoUsuario.IIDTIPOUSUARIO,
                             nombre = tipoUsuario.NOMBRE,
                             descripcion = tipoUsuario.DESCRIPCION
                         }).ToList();
                if (oTipoUsuario.iidtipousuario == 0 && oTipoUsuario.nombre == null 
                    && oTipoUsuario.descripcion == null)
                {
                    listafiltrado = lista;
                }else
                {
                    Predicate<TipoUsuarioCLS> pred = new Predicate<TipoUsuarioCLS>(buscarTipoUsuario);
                    listafiltrado = lista.FindAll(pred);
                }
            }   
            return View(listafiltrado);
        }
    }
}