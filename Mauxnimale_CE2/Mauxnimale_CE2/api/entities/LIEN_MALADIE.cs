
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
    
public partial class LIEN_MALADIE
{

    public int IDLIEN { get; set; }

    public int IDMALADIE { get; set; }

    public int IDANIMAL { get; set; }



    public virtual ANIMAL ANIMAL { get; set; }

    public virtual MALADIE MALADIE { get; set; }

}

}
