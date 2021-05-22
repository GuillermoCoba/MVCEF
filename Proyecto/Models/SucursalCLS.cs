using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Proyecto.Models
{
    public class SucursalCLS
    {
        [Display (Name ="Id Sucursal")]
        public int iidsucursal { get; set; }
        [Display(Name = "Nombre Sucursal")]
        [Required]
        [StringLength(100,ErrorMessage ="La longitud maxima 100")]
        public string nombre { get; set; }
        [Display(Name = "Dirección")]
        [Required]
        [StringLength(200, ErrorMessage = "La longitud maxima 200")]
        public string direccion { get; set; }
        [Display(Name = "Telefono de la sucursal")]
        [StringLength(10, ErrorMessage = "La longitud maxima 10")]
        [Required]
        public string telefono { get; set; }
        [Display(Name = "Email de la sucursal")]
        [Required]
        [EmailAddress(ErrorMessage ="Ingrese un email valido")]
        [StringLength(100, ErrorMessage = "La longitud maxima 100")]
        public string email { get; set; }
        [Display(Name = "Fecha Apertura")]
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:yyyy-MM-dd}",ApplyFormatInEditMode =true)]
        public DateTime fechaApertura { get; set; }
        public int bhabilitado { get; set; }
        public string mensajeError { get; set; }


    }
}