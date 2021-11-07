using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto.Models
{
    public class UsuarioCLS
    {
        public int iidusuario { get; set; }
        public string nombreusuario { get; set; }
        public string contra { get; set; }
        public int iid { get; set; }

        public int iidrol { get; set; }
        public int bhabilitado { get; set; }


        //Propiedad adicional
        public string nombrePersona { get; set; }
        public string nombreRol { get; set; }
        public string nombreTipoEmpleado { get; set; }



    }
}