using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Proyecto.Models
{
    public class TipoUsuarioCLS
    {
        [Display(Name = "Id Tipo Usuario")]

        public int iidtipousuario { get; set; }
        [Display(Name = "Nombre")]
        [Required]
        [StringLength(150, ErrorMessage = "Longitud maxima de 150")]


        public string nombre { get; set; }
        [Display(Name = "Descripción")]
        [Required]
    [StringLength(250,ErrorMessage ="Longitud maxima de 250")]

        public string descripcion { get; set; }
        public int bhabilitado { get; set; }


    }
}