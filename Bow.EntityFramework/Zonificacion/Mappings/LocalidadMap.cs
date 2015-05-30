using Bow.Afiliaciones.Entidades;
using Bow.Empresas.Entidades;
using Bow.MappingsBase;
using Bow.Zonificacion.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Zonificacion.Mappings
{
    public class LocalidadMap : MultiTenantMap<Localidad>
    {
        public LocalidadMap()
        {
            //Atributos
            Property(localidad => localidad.Nombre).HasMaxLength(30);
            Property(localidad => localidad.Nombre).IsRequired();

            //Llaves Foráneas
            HasMany<Barrio>(localidad => localidad.Barrios)
              .WithRequired(barrio => barrio.Localidad)
              .HasForeignKey(barrio => barrio.LocalidadId)
              .WillCascadeOnDelete(false);

            HasMany<Zona>(localidad => localidad.Zonas)
               .WithRequired(zona => zona.LocalidadZona)
               .HasForeignKey(zona => zona.LocalidadId)
               .WillCascadeOnDelete(false);

            HasMany<TorieLocalidad>(localidad => localidad.TiposOrientacionLocalidad)
                .WithRequired(torieLocalidad => torieLocalidad.LocalidadTorieLocalidad)
                .HasForeignKey(torieLocalidad => torieLocalidad.LocalidadId);

            HasMany<SufijoLocalidad>(localidad => localidad.SufijosLocalidad)
                .WithRequired(sufijoLocalidad => sufijoLocalidad.LocalidadSufijoLocalidad)
                .HasForeignKey(sufijoLocalidad => sufijoLocalidad.LocalidadId);

            HasMany<Telefono>(localidad => localidad.Telefonos)
                .WithRequired(telefono => telefono.LocalidadTelefono)
                .HasForeignKey(telefono => telefono.LocalidadId)
                .WillCascadeOnDelete(false);

            HasMany<InfoTributariaLocalidad>(localidad => localidad.InfoTributariaLocalidades)
                .WithRequired(info => info.Localidad)
                .HasForeignKey(info => info.LocalidadId)
                .WillCascadeOnDelete(false);

            HasMany<RecaudoMasivoCobertura>(localidad => localidad.RecaudoMasivoCobertura)
                .WithRequired(rec => rec.LocalidadRecaudoMasivo)
                .HasForeignKey(rec => rec.LocalidadId)
                .WillCascadeOnDelete(false);

            HasMany<AfiliadoProspecto>(localidad => localidad.AfiliadosProspecto)
                .WithRequired(afiliadoprospecto => afiliadoprospecto.CiudadResidencia)
                .HasForeignKey(afiliadoprospecto => afiliadoprospecto.CiudadResidenciaId)
                .WillCascadeOnDelete(false);

            HasMany<GestionProspecto>(localidad => localidad.LocalidadGestionProspecto)
                .WithRequired(gestionprospecto => gestionprospecto.Localidad)
                .HasForeignKey(gestionprospecto => gestionprospecto.LocalidadId)
                .WillCascadeOnDelete(false);

            //Tabla
            ToTable("localidad");

        }
    }
}
