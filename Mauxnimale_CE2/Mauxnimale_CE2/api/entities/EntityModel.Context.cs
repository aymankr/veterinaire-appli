﻿

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
using System.Data.Entity;
using System.Data.Entity.Infrastructure;


public partial class PT4_S4P2C_E2Entities : DbContext
{
    public PT4_S4P2C_E2Entities()
        : base("name=PT4_S4P2C_E2Entities")
    {

    }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        throw new UnintentionalCodeFirstException();
    }


    public virtual DbSet<ANIMAL> ANIMAL { get; set; }

    public virtual DbSet<CLIENT> CLIENT { get; set; }

    public virtual DbSet<ESPECE> ESPECE { get; set; }

    public virtual DbSet<FACTURE_PRODUIT> FACTURE_PRODUIT { get; set; }

    public virtual DbSet<JOURNEE> JOURNEE { get; set; }

    public virtual DbSet<JOURNEE_SALARIE> JOURNEE_SALARIE { get; set; }

    public virtual DbSet<LIEN_MALADIE> LIEN_MALADIE { get; set; }

    public virtual DbSet<LOG> LOG { get; set; }

    public virtual DbSet<MALADIE> MALADIE { get; set; }

    public virtual DbSet<ORDONNANCE> ORDONNANCE { get; set; }

    public virtual DbSet<PRODUIT> PRODUIT { get; set; }

    public virtual DbSet<PRODUITLIES> PRODUITLIES { get; set; }

    public virtual DbSet<PRODUITVENDU> PRODUITVENDU { get; set; }

    public virtual DbSet<RACE> RACE { get; set; }

    public virtual DbSet<RENDEZ_VOUS> RENDEZ_VOUS { get; set; }

    public virtual DbSet<SALARIE> SALARIE { get; set; }

    public virtual DbSet<SOIN> SOIN { get; set; }

    public virtual DbSet<TYPE_PRODUIT> TYPE_PRODUIT { get; set; }

    public virtual DbSet<TYPE_RDV> TYPE_RDV { get; set; }

    public virtual DbSet<LIEN_SOIN> LIEN_SOIN { get; set; }

}

}

