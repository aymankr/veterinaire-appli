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
    
    public partial class PRODUIT
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PRODUIT()
        {
            this.PRODUITLIES = new HashSet<PRODUITLIES>();
            this.PRODUITVENDU = new HashSet<PRODUITVENDU>();
        }
    
        public int IDPRODUIT { get; set; }
        public int IDTYPE { get; set; }
        public int QUANTITEENSTOCK { get; set; }
        public string NOMPRODUIT { get; set; }
        public byte[] IMAGEPRODUIT { get; set; }
        public decimal PRIXDEVENTECLIENT { get; set; }
        public decimal PRIXDACHAT { get; set; }
        public System.DateTime DATEPEREMPTION { get; set; }
    
        public virtual TYPE_PRODUIT TYPE_PRODUIT { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRODUITLIES> PRODUITLIES { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRODUITVENDU> PRODUITVENDU { get; set; }
    }
}
