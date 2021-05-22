using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Proyecto.Models
{
    public class ViajeCLS
    {
        [Display(Name ="Id viaje")]
        [Required]
        public int iidviaje { get; set; }
        [Display(Name = "Lugar Origen")]
        [Required]
        public int iidlugarorigen { get; set; }
        [Display(Name = "Destino")]
        [Required]
        public int iidlugardestino { get; set; }
        [Display(Name = "Precio")]
        [Required]
        [Range (0,100000,ErrorMessage ="Rango superado")]
        public double precio { get; set; }
        [Display(Name = "Fecha Viaje")]
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:yyyy-MM-dd}",ApplyFormatInEditMode =true)]
        public DateTime fechaViaje { get; set; }
        [Display(Name = "Bus")]
        [Required]
        public int iidbus{get;set;}
        [Display(Name = "Asientos")]
        [Required]
        public int  numeroasientosdisponibles { get; set; }
        public int bhabilitados { get; set; }

        //ESPECIALES AUX
        [Display(Name = "Lugar Origen")]
        public string nombreOrigen { get; set; }
        [Display(Name = "Lugar Destino")]
        public string nombreDestino { get; set; }
        [Display(Name = "Placas Bus")]
        public string placa { get; set; }


    }
}