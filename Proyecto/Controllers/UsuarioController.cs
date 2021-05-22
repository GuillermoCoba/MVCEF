using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyecto.Models;
using System.Transactions;
using System.Security.Cryptography;
using System.Text;

namespace Proyecto.Controllers
{
    public class UsuarioController : Controller
    {
        public void listaPersonas()
        {
            List<SelectListItem> listaPersonas = new List<SelectListItem>();
            using (var db = new BDPasajeEntities())
            {
                List<SelectListItem> listaClientes = (from item in db.Cliente
                                                      where item.BHABILITADO == 1 && item.bTieneUsuario != 1
                                                      select new SelectListItem
                                                      {
                                                          Text = item.NOMBRE + " " + item.APPATERNO + " " + item.APMATERNO + " (C)",
                                                          Value = item.IIDCLIENTE.ToString()
                                                      }).ToList();
                List<SelectListItem> listaEmpleados = (from item in db.Empleado
                                                       where item.BHABILITADO == 1 && item.bTieneUsuario != 1
                                                       select new SelectListItem
                                                       {
                                                           Text = item.NOMBRE + " " + item.APPATERNO + " " + item.APMATERNO + " (E)",
                                                           Value = item.IIDEMPLEADO.ToString()
                                                       }).ToList();
                listaPersonas.AddRange(listaClientes);
                listaPersonas.AddRange(listaEmpleados);
                listaPersonas = listaPersonas.OrderBy(p => p.Text).ToList();
                listaPersonas.Insert(0, new SelectListItem { Text = "--Seleccione--", Value = "" });
                ViewBag.listaPersona = listaPersonas;
            }
        }
        public void listarRol()
        {
            using (var db = new BDPasajeEntities())
            {
                List<SelectListItem> listaRol = (from item in db.Rol
                                                 where item.BHABILITADO == 1
                                                 select new SelectListItem
                                                 {
                                                     Text = item.NOMBRE,
                                                     Value = item.IIDROL.ToString()
                                                 }).ToList();
                listaRol.Insert(0, new SelectListItem { Text = "--Seleccione--", Value = "" });
                ViewBag.listaRol = listaRol;

            }
        }
        // GET: Usuario
        public ActionResult Index()
        {
            listaPersonas();
            listarRol();
            return View();
        }
        public int Guardar(UsuarioCLS oUsuarioCLS,int titulo)
        {
            int rpta = 0;
            try
            {
                using (var db = new BDPasajeEntities())
                {
                    using (var transaccion = new TransactionScope())
                    {
                        if (titulo == 1)
                        {
                            Usuario oUsuario = new Usuario();
                            oUsuario.NOMBREUSUARIO = oUsuarioCLS.nombreusuario;
                            SHA256Managed sha = new SHA256Managed();
                            byte[] byteContra = Encoding.Default.GetBytes(oUsuarioCLS.contra);
                            byte[] byteContraCifrado = sha.ComputeHash(byteContra);
                            string contraCifrada =  BitConverter.ToString(byteContraCifrado).Replace("-", "");
                            oUsuario.CONTRA = contraCifrada;
                            oUsuario.TIPOUSUARIO = oUsuarioCLS.nombrePersona.Substring(oUsuarioCLS.nombrePersona.Length - 2, 1);
                            oUsuario.IID = oUsuarioCLS.iid;
                            oUsuario.IIDROL = oUsuarioCLS.iidrol;
                            oUsuario.bhabilitado = 1;
                            db.Usuario.Add(oUsuario);                           
                            if (oUsuario.TIPOUSUARIO.Equals("C"))
                            {
                                Cliente oCliente = db.Cliente.Where(p => p.IIDCLIENTE.Equals(oUsuarioCLS.iid)).First();
                                oCliente.bTieneUsuario = 1;
                            }else
                            {
                                Empleado oEmpleado = db.Empleado.Where(p => p.IIDEMPLEADO.Equals(oUsuarioCLS.iid)).First();
                                oEmpleado.bTieneUsuario = 1;
                            }
                            rpta = db.SaveChanges();
                            transaccion.Complete();
                        }
                    }
                }
            }
            catch
            {
                rpta = 0;
            }
            return rpta;
        }
    }
}