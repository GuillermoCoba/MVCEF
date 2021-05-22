using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyecto.Models;

namespace Proyecto.Controllers
{
    public class ViajeController : Controller
    {
        // GET: Viaje
        public ActionResult Index()
        {
            List<ViajeCLS> listaViaje = null;
            using(var db = new BDPasajeEntities())
            {
                listaViaje = (from viaje in db.Viaje
                              join origen in db.Lugar
                              on viaje.IIDLUGARORIGEN equals origen.IIDLUGAR
                              join destino in db.Lugar
                              on viaje.IIDLUGARDESTINO equals destino.IIDLUGAR
                              join bus in db.Bus
                              on viaje.IIDBUS equals bus.IIDBUS
                              select new ViajeCLS
                              {
                                  iidviaje = viaje.IIDVIAJE,
                                  placa = bus.PLACA,
                                  nombreDestino = destino.NOMBRE,
                                  nombreOrigen = origen.NOMBRE,
                              }

                              ).ToList();
            }
            return View(listaViaje);
        }
        public ActionResult Agregar()
        {
            Llenar();
            return View();
        }
        [HttpPost]
        public ActionResult Agregar(ViajeCLS oViajeCLS)
        {
            Viaje oViajes = new Viaje();
            if (!ModelState.IsValid)
            {
                Llenar();
                return View(oViajeCLS);
            }
            else
            {
                using(var db = new BDPasajeEntities())
                {  
                    oViajes.IIDLUGARORIGEN = oViajeCLS.iidlugarorigen;
                    oViajes.IIDLUGARDESTINO = oViajeCLS.iidlugardestino;
                    oViajes.PRECIO = (decimal)oViajeCLS.precio;
                    oViajes.IIDBUS = oViajeCLS.iidbus;
                    oViajes.FECHAVIAJE = oViajeCLS.fechaViaje;
                    oViajes.NUMEROASIENTOSDISPONIBLES = oViajeCLS.numeroasientosdisponibles;
                    db.Viaje.Add(oViajes);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
           
        }
        public ActionResult Editar(int id)
        {
            ViajeCLS oViajeCLS = new ViajeCLS();
            using(var db = new BDPasajeEntities())
            {
                Llenar();
                Viaje oViajes = db.Viaje.Where(p => p.IIDVIAJE.Equals(id)).First();
                oViajeCLS.iidviaje = oViajes.IIDVIAJE;
                oViajeCLS.iidlugarorigen = (int) oViajes.IIDLUGARORIGEN;
                oViajeCLS.iidlugardestino = (int) oViajes.IIDLUGARDESTINO;
                oViajeCLS.precio = (double) oViajes.PRECIO;
                oViajeCLS.iidbus = (int)oViajes.IIDBUS;
                oViajeCLS.fechaViaje = (DateTime)oViajes.FECHAVIAJE;
                oViajeCLS.numeroasientosdisponibles = (int)oViajes.NUMEROASIENTOSDISPONIBLES;
            }
            
            return View(oViajeCLS);
        }
        [HttpPost]
        public ActionResult Editar(ViajeCLS oViajeCLS)
        {
            int id = oViajeCLS.iidviaje;
            if (!ModelState.IsValid)
            {
                Llenar();
                return View(oViajeCLS);
            }
            else
            {
                using (var db = new BDPasajeEntities())
                {
                    Viaje oViajes = db.Viaje.Where(p => p.IIDVIAJE.Equals(id)).First();
                    oViajes.IIDLUGARORIGEN = oViajeCLS.iidlugarorigen;
                    oViajes.IIDLUGARDESTINO = oViajeCLS.iidlugardestino;
                    oViajes.PRECIO = (decimal)oViajeCLS.precio;
                    oViajes.IIDBUS = oViajeCLS.iidbus;
                    oViajes.FECHAVIAJE = oViajeCLS.fechaViaje;
                    oViajes.NUMEROASIENTOSDISPONIBLES = oViajeCLS.numeroasientosdisponibles;
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
        }
        public void ListaDestino()
        {
            List<SelectListItem> ListaDestino;
            using (var db = new BDPasajeEntities())
            {
                ListaDestino = (from origen in db.Lugar
                         where origen.BHABILITADO == 1
                         select new SelectListItem
                         {
                             Text = origen.NOMBRE,
                             Value = origen.IIDLUGAR.ToString()
                         }).ToList();
                ListaDestino.Insert(0, new SelectListItem { Text = "--Seleccione--",Value=" " });
                ViewBag.ListaDestino = ListaDestino;
            }
        }
        public void ListaBus()
        {
            List<SelectListItem> ListaBus;
            using (var db = new BDPasajeEntities())
            {
                ListaBus = (from bus in db.Bus
                                where bus.BHABILITADO == 1
                                select new SelectListItem
                                {
                                    Text = bus.PLACA,
                                    Value = bus.IIDBUS.ToString()
                                }).ToList();
                ListaBus.Insert(0, new SelectListItem { Text = "--Seleccione--", Value = " " });
                ViewBag.ListaBus = ListaBus;
            }
        }
        public void Llenar()
        {
            ListaBus();
            ListaDestino();
        }
    }
}