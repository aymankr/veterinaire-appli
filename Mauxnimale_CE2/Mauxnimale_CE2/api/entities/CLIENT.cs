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
    
    public partial class CLIENT
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CLIENT()
        {
            this.ANIMAL = new HashSet<ANIMAL>();
            this.FACTURE_PRODUIT = new HashSet<FACTURE_PRODUIT>();
            this.RENDEZ_VOUS = new HashSet<RENDEZ_VOUS>();
        }
    
        public int IDCLIENT { get; set; }
        public string NOMCLIENT { get; set; }
        public string PRENOMCLIENT { get; set; }
        public string TELCLIENT { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ANIMAL> ANIMAL { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FACTURE_PRODUIT> FACTURE_PRODUIT { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RENDEZ_VOUS> RENDEZ_VOUS { get; set; }

        public override string ToString()
        {
            return NOMCLIENT + " " + PRENOMCLIENT;
        }
    }
}
