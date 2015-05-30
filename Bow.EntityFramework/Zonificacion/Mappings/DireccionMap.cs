using Bow.Afiliaciones.Entidades;
using Bow.Empresas.Entidades;
using Bow.MappingsBase;
using Bow.Personas.Entidades;
using Bow.Zonificacion.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Zonificacion.Mappings
{
    public class DireccionMap : MultiTenantMap<Direccion>
    {
        public DireccionMap()
        {
            //Atributos
            Property(direccion => direccion.Nombre).HasMaxLength(150);
            Property(direccion => direccion.Nombre).IsOptional();
            Property(direccion => direccion.Pista).HasMaxLength(300);
            Property(direccion => direccion.Orientacion1).IsOptional();
            Property(direccion => direccion.Porton).HasMaxLength(20);
            Property(direccion => direccion.Porton).IsOptional();
            Property(direccion => direccion.Apartamento).HasMaxLength(10);
            Property(direccion => direccion.DireccionCompleta).HasMaxLength(150);
            Property(direccion => direccion.DireccionCompleta).IsOptional();

            ////Llaves Foráneas
            HasMany<Empresa>(empresa => empresa.Empresas)
                .WithRequired(empresa => empresa.Direccion)
                .HasForeignKey(empresa => empresa.DireccionId)
                .WillCascadeOnDelete(false);

            HasMany<PersonaDireccion>(direccion => direccion.PersonaDirecciones)
             .WithRequired(PersonaDireccion => PersonaDireccion.DireccionPersonaDireccion)
             .HasForeignKey(PersonaDireccion => PersonaDireccion.DireccionId)
             .WillCascadeOnDelete(false);

            HasMany<Sucursal>(sucursal => sucursal.Sucursales)
             .WithRequired(sucursal => sucursal.SucursalDireccion)
             .HasForeignKey(sucursal => sucursal.DireccionId)
             .WillCascadeOnDelete(false);

            HasMany<Prospecto>(prospecto => prospecto.DireccionProspecto)
             .WithOptional(direccionprospecto => direccionprospecto.Direccion)
             .HasForeignKey(direccionprospecto => direccionprospecto.DireccionId)
             .WillCascadeOnDelete(false);

            //Tabla
            ToTable("direccion");
        }
    }
}
