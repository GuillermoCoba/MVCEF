using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyecto.Models;
namespace Proyecto.Controllers
{
    public class BusController : Controller
    {
        // GET: Bus
        /// <summary>
        /// 
        /// </summary>
        /// <param name="oBusCLS"></param>
        /// <returns></returns>
        public ActionResult Index(BusCLS oBusCLS)
        {
            llenar();
            List<BusCLS> listaBus = new List<BusCLS>();
            List<BusCLS> respuesta = null;
            using (var db = new BDPasajeEntities())
                listaBus = (from bus in db.Bus
                            join modelo in db.Modelo
                            on bus.IIDMODELO equals modelo.IIDMODELO
                            join tipobus in db.TipoBus
                            on bus.IIDTIPOBUS equals tipobus.IIDTIPOBUS
                            join sucursal in db.Sucursal
                            on bus.IIDSUCURSAL equals sucursal.IIDSUCURSAL
                            where bus.BHABILITADO == 1
                            select new BusCLS
                            {
                                iidBus = bus.IIDBUS,
                                placa = bus.PLACA,
                                nombreModelo = modelo.NOMBRE,
                                nombreSucursal = sucursal.NOMBRE,
                                nombreTipoBus = tipobus.NOMBRE,
                                iidModelo= modelo.IIDMODELO,
                                iidSucursal=sucursal.IIDSUCURSAL,
                                iidTipoBus=tipobus.IIDTIPOBUS
                            }).ToList();
            {
                if (oBusCLS.iidBus == 0 && oBusCLS.placa == null && oBusCLS.iidModelo == 0 && oBusCLS.iidSucursal == 0
                     && oBusCLS.iidTipoBus == 0)
                {
                    respuesta = listaBus;
                }
                else
                {
                    //filtro idbus
                    if (oBusCLS.iidBus != 0)
                    {
                        listaBus = listaBus.Where(p => p.iidBus.ToString().Contains(oBusCLS.iidBus.ToString())).ToList();
                    }
                    //filtro placa
                    if (oBusCLS.placa != null)
                    {
                        listaBus = listaBus.Where(p => p.placa.Contains(oBusCLS.placa)).ToList();
                    }
                    //filtro modelo
                    if (oBusCLS.iidModelo != 0)
                    {
                        listaBus = listaBus.Where(p => p.iidModelo.ToString().Contains(oBusCLS.iidModelo.ToString())).ToList();
                    }
                    //filtro sucursal
                    if (oBusCLS.iidSucursal != 0)
                    {
                        listaBus = listaBus.Where(p => p.iidSucursal.ToString().Contains(oBusCLS.iidSucursal.ToString())).ToList();
                    }
                    //filtro tippbus
                    if (oBusCLS.iidTipoBus != 0)
                    {
                        listaBus = listaBus.Where(p => p.iidTipoBus.ToString().Contains(oBusCLS.iidTipoBus.ToString())).ToList();

                    }
                    respuesta = listaBus;
                }
            }
            return View(respuesta);
        }
        public ActionResult Agregar()
        {
            llenar();
            return View();
        }
        [HttpPost]
        public ActionResult Agregar(BusCLS oBusCLS)
        {
            int n = 0;
            string placas = oBusCLS.placa;
            using(var db = new BDPasajeEntities())
            {
                n = db.Bus.Where(p => p.PLACA.Equals(placas)).Count();
            }
          
                if (!ModelState.IsValid || n>0)
                {
                if (n > 0) oBusCLS.mensajeError = "Ya existen estas placas";
                    llenar();
                    return View(oBusCLS);
                }
              else
                { using(var db = new BDPasajeEntities())
                 {
                    Bus oBus = new Bus();
                    oBus.IIDSUCURSAL = oBusCLS.iidSucursal;
                    oBus.IIDTIPOBUS = oBusCLS.iidTipoBus;
                    oBus.FECHACOMPRA = oBusCLS.fechaCompra;
                    oBus.PLACA = oBusCLS.placa;
                    oBus.IIDMODELO = oBusCLS.iidModelo;
                    oBus.NUMEROFILAS = oBusCLS.numeroColumnas;
                    oBus.DESCRIPCION = oBusCLS.descripcion;
                    oBus.OBSERVACION = oBusCLS.observacion;
                    oBus.IIDMARCA = oBusCLS.iidmarca;
                    oBus.BHABILITADO = 1;
                    db.Bus.Add(oBus);
                    db.SaveChanges();
                   
                }
                return RedirectToAction("Index");
            }
           
        }
        public ActionResult Editar(int id)
        {
            BusCLS oBusCLS = new BusCLS();
            using (var db = new BDPasajeEntities())
            {
                llenar();
                Bus oBus = db.Bus.Where(p => p.IIDBUS.Equals(id)).First();
                oBusCLS.iidBus = oBus.IIDBUS;
                oBusCLS.iidSucursal = (int)oBus.IIDSUCURSAL;
                oBusCLS.iidTipoBus = (int)oBus.IIDTIPOBUS;
                oBusCLS.fechaCompra = (DateTime)oBus.FECHACOMPRA;
                oBusCLS.placa = oBus.PLACA;
                oBusCLS.iidModelo = (int)oBus.IIDMODELO;
                oBusCLS.numeroColumnas = (int)oBus.NUMEROFILAS;
                oBusCLS.descripcion = oBus.DESCRIPCION;
                oBusCLS.observacion = oBus.OBSERVACION;
                oBusCLS.iidmarca = (int)oBus.IIDMARCA;
            }
                return View(oBusCLS);
        }
        [HttpPost]
        public ActionResult Editar(BusCLS oBusCLS)
        {
            int id = oBusCLS.iidBus;
            int n = 0;
            string placas = oBusCLS.placa;
            using (var db = new BDPasajeEntities())
            {
                n = db.Bus.Where(p => p.PLACA.Equals(placas)&& !p.IIDBUS.Equals(id)).Count();
            }

            if (!ModelState.IsValid || n > 0)
            {
                if (n > 0) oBusCLS.mensajeError = "Ya existen estas placas";
                llenar();
                return View(oBusCLS);
            }
            else
            {
                using (var db = new BDPasajeEntities())
                {
                    Bus oBus = db.Bus.Where(p => p.IIDBUS.Equals(id)).First();
                    oBus.IIDSUCURSAL = oBusCLS.iidSucursal;
                    oBus.IIDTIPOBUS = oBusCLS.iidTipoBus;
                    oBus.FECHACOMPRA = oBusCLS.fechaCompra;
                    oBus.PLACA = oBusCLS.placa;
                    oBus.IIDMODELO = oBusCLS.iidModelo;
                    oBus.NUMEROFILAS = oBusCLS.numeroColumnas;
                    oBus.DESCRIPCION = oBusCLS.descripcion;
                    oBus.OBSERVACION = oBusCLS.observacion;
                    oBus.IIDMARCA = oBusCLS.iidmarca;
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
        }
        public void listaModelo()
        {
            List<SelectListItem> lista ;
            using(var db = new BDPasajeEntities())
            {
                lista = (from modelo in db.Modelo
                         where modelo.IIDMODELO==1
                         select new SelectListItem
                         {
                             Text = modelo.NOMBRE,
                             Value = modelo.IIDMODELO.ToString()
                         }).ToList();
                lista.Insert(0, new SelectListItem { Text = "--Seleccione--", Value = " " });
                ViewBag.listaModelo = lista;
            }
        }
        public void listaSucursal()
        {
            List<SelectListItem> lista;
            using(var db = new BDPasajeEntities())
            {
                lista = (from sucursal in db.Sucursal
                         where sucursal.BHABILITADO == 1
                         select new SelectListItem
                         {
                             Text = sucursal.NOMBRE,
                             Value = sucursal.IIDSUCURSAL.ToString()
                         }).ToList();
                lista.Insert(0, new SelectListItem { Text = "--Seleccione--", Value = " " });
                ViewBag.listaSucursal = lista;
            }
        }
        public void listaTipoBus()
        {
            List<SelectListItem> lista;
            using(var db = new BDPasajeEntities())
            {
                lista = (from tipo in db.TipoBus
                         where tipo.BHABILITADO == 1
                         select new SelectListItem
                         {
                             Text=tipo.NOMBRE,
                             Value=tipo.IIDTIPOBUS.ToString()
                         }).ToList();
                lista.Insert(0, new SelectListItem {Text="--Seleccione--",Value=" "});
                ViewBag.listaTipoBus = lista;
            }
        }
        public void listaMarca()
        {
            List<SelectListItem> lista;
            using (var db = new BDPasajeEntities())
            {
                lista = (from tipo in db.Marca
                         where tipo.BHABILITADO == 1
                         select new SelectListItem
                         {
                             Text = tipo.NOMBRE,
                             Value = tipo.IIDMARCA.ToString()
                         }).ToList();
                lista.Insert(0, new SelectListItem { Text = "--Seleccione--", Value = " " });
                ViewBag.listaMarca = lista;
            }
        }
        public void llenar()
        {
            listaModelo();
            listaSucursal();
            listaTipoBus();
            listaMarca();
        }
        [HttpPost]
        public ActionResult Eliminar(int iidBus)
        {
            using (var db = new BDPasajeEntities())
            {
                Bus oBus = db.Bus.Where(p => p.IIDBUS.Equals(iidBus)).First();
                oBus.BHABILITADO = 0;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}