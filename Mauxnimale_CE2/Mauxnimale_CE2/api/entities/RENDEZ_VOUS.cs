//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Mauxnimale_CE2.api.entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class RENDEZ_VOUS
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RENDEZ_VOUS()
        {
            this.ANIMAL = new HashSet<ANIMAL>();
            this.ORDONNANCE = new HashSet<ORDONNANCE>();
        }
    
        public int IDRDV { get; set; }
        public int IDTYPE { get; set; }
        public int IDCLIENT { get; set; }
        public int IDJOURNEE { get; set; }
        public System.TimeSpan HEUREDEBUT { get; set; }
        public System.TimeSpan HEUREFIN { get; set; }
        public string RAISON { get; set; }
    
        public virtual CLIENT CLIENT { get; set; }
        public virtual JOURNEE JOURNEE { get; set; }
        public virtual TYPE_RDV TYPE_RDV { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ANIMAL> ANIMAL { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ORDONNANCE> ORDONNANCE { get; set; }
    }
}
