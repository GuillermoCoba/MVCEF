//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Proyecto.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Asiento
    {
        public int IIDASIENTO { get; set; }
        public Nullable<int> IIDVIAJE { get; set; }
        public Nullable<int> INDICEFILA { get; set; }
        public Nullable<int> INDICECOLUMNA { get; set; }
        public Nullable<int> BHABILITADO { get; set; }
    
        public virtual Viaje Viaje { get; set;}
    }
}
