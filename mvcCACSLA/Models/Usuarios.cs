//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace mvcCACSLA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Usuarios
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Usuarios()
        {
            this.Indicadores_archivos = new HashSet<Indicadores_archivos>();
            this.Indicadores_detalle = new HashSet<Indicadores_detalle>();
        }
    
        public int IdUsuario { get; set; }
        public string Login { get; set; }
        public string Pass { get; set; }
        public string Descripcion { get; set; }
        public int IdNivel { get; set; }
        public int Activo { get; set; }
        public int IdCarrera { get; set; }
        public int IdCoordinacion { get; set; }
        public Nullable<bool> SubeCarreras { get; set; }
    
        public virtual Carreras Carreras { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Indicadores_archivos> Indicadores_archivos { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Indicadores_detalle> Indicadores_detalle { get; set; }
        public virtual Usuarios_niveles Usuarios_niveles { get; set; }
        public virtual Coordinaciones Coordinaciones { get; set; }
    }
}
