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
    
    public partial class Cliente
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Cliente()
        {
            this.VENTA = new HashSet<VENTA>();
        }
    
        public int IIDCLIENTE { get; set; }
        public string NOMBRE { get; set; }
        public string APPATERNO { get; set; }
        public string APMATERNO { get; set; }
        public string EMAIL { get; set; }
        public string DIRECCION { get; set; }
        public Nullable<int> IIDSEXO { get; set; }
        public string TELEFONOFIJO { get; set; }
        public string TELEFONOCELULAR { get; set; }
        public Nullable<int> BHABILITADO { get; set; }
        public Nullable<int> bTieneUsuario { get; set; }
        public string TIPOUSUARIO { get; set; }
    
        public virtual Sexo Sexo { get; set; }
        public virtual TIPOUSUARIOREGISTRO TIPOUSUARIOREGISTRO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VENTA> VENTA { get; set; }
    }
}
