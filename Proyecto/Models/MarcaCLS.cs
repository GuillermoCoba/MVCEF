using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Proyecto.Models
{
    public class MarcaCLS
    {
        [Display(Name = "Id Marca")]
        public int iidmarca { get; set; }
        [Display(Name = "Nombre Marca")]
        [Required]
        [StringLength(100,ErrorMessage ="La longitud maxima es 100")]
        public string nombre { get; set; }
        [Required]
        [StringLength(200, ErrorMessage = "La longitud maxima es 200")]
        [Display(Name = "Descripción Marca")]
        [DataType(DataType.MultilineText)]
        public string descripcion { get; set; }
        public int bhabilitado { get; set; }

        //PROPIEDAD (ERRORES DE VALIDACION)
        public string mensajeError { get; set; }
    }
}