using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyecto.Models;

namespace Proyecto.Controllers
{
    public class MarcaController : Controller
    {
        // GET: Marca
        public ActionResult Index(MarcaCLS oMarcaCLS)
        {
            List<MarcaCLS> listaMarca = null;
            using (var bd = new BDPasajeEntities())
            {

                if (oMarcaCLS.nombre == null)
                {
                    listaMarca = (from marca in bd.Marca
                                  where marca.BHABILITADO == 1
                                  select new MarcaCLS
                                  {
                                      iidmarca = marca.IIDMARCA,
                                      nombre = marca.NOMBRE,
                                      descripcion = marca.DESCRIPCION
                                  }).ToList();
                }
                else
                {
                    listaMarca = (from marca in bd.Marca
                                  where marca.BHABILITADO == 1 && marca.NOMBRE.Contains(oMarcaCLS.nombre)
                                  select new MarcaCLS
                                  {
                                      iidmarca = marca.IIDMARCA,
                                      nombre=marca.NOMBRE,
                                      descripcion=marca.DESCRIPCION
                                  }).ToList();
                }
            }
            
                return View(listaMarca);
        }
        public ActionResult Agregar()
        {
           
            return View();
        }
        public ActionResult Editar(int id)
        {
            MarcaCLS oMarcaCLS = new MarcaCLS();
            using (var db = new BDPasajeEntities())
            {
                Marca oMarca = db.Marca.Where(p => p.IIDMARCA.Equals(id)).First();
                oMarcaCLS.iidmarca = oMarca.IIDMARCA;
                oMarcaCLS.nombre = oMarca.NOMBRE;
                oMarcaCLS.descripcion = oMarca.DESCRIPCION;
            }
                return View(oMarcaCLS);
        }
        
        [HttpPost]
        public ActionResult Agregar(MarcaCLS oMarcaCLS)
        {
            int nregistrosEncontrados = 0;
            string nombreMarca = oMarcaCLS.nombre;
            using(var db = new BDPasajeEntities())
            {
                nregistrosEncontrados = db.Marca.Where(p => p.NOMBRE.Equals(nombreMarca)).Count();
            }
            
            if (!ModelState.IsValid || nregistrosEncontrados>0)
            {
                if (nregistrosEncontrados > 0) oMarcaCLS.mensajeError = "El nombre marca ya existe";
                return View(oMarcaCLS);
            }
            else
            {
                using(var db = new BDPasajeEntities())
                {
                    Marca oMarca = new Marca();
                    oMarca.NOMBRE = oMarcaCLS.nombre;
                    oMarca.DESCRIPCION = oMarcaCLS.descripcion;
                    oMarca.BHABILITADO = 1;
                    db.Marca.Add(oMarca);
                    db.SaveChanges();
                }

            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Editar(MarcaCLS oMarcaCLS)
        {
            int n=0;
            string nombre = oMarcaCLS.nombre;
            int id = oMarcaCLS.iidmarca;
            using(var db = new BDPasajeEntities())
            {
                n = db.Marca.Where(p => p.NOMBRE.Equals(nombre) && !p.IIDMARCA.Equals(id)).Count(); 
            }
            if (!ModelState.IsValid || n>0)
            {
                if (n > 0) oMarcaCLS.mensajeError = "Ya existe esta marca";
                return View(oMarcaCLS);
            }
            else
            {
                int idMarca = oMarcaCLS.iidmarca;
                using (var db = new BDPasajeEntities())
                {
                    Marca oMarca = db.Marca.Where(p => p.IIDMARCA.Equals(idMarca)).First();
                    oMarca.NOMBRE = oMarcaCLS.nombre;
                    oMarca.DESCRIPCION = oMarcaCLS.descripcion;
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
        }
        public ActionResult Eliminar(int id)
        {
            using(var db =new BDPasajeEntities())
            {
                Marca oMarca = db.Marca.Where(p => p.IIDMARCA.Equals(id)).First();
                oMarca.BHABILITADO = 0;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}