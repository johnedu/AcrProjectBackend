using Bow.Afiliaciones.Entidades;
using Bow.Empresas.Entidades;
using Bow.MappingsBase;
using Bow.Parametros.Entidades;
using Bow.Personas.Entidades;
using Bow.Zonificacion.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Parametros.Mappings
{
    public class TipoMap : MultiTenantMap<Tipo>
    {
        public TipoMap()
        {
            //Atributos
            Property(tipo => tipo.Nombre).HasMaxLength(100);
            Property(tipo => tipo.Nombre).IsRequired();
            Property(tipo => tipo.Descripcion).HasMaxLength(300);

            //Llaves Foráneas
            HasMany<Telefono>(tipo => tipo.Telefonos)
                .WithRequired(telefono => telefono.TipoTelefono)
                .HasForeignKey(telefono => telefono.TipoId);

            HasMany<Zona>(tipo => tipo.Zonas)
               .WithRequired(zona => zona.TipoZona)
               .HasForeignKey(zona => zona.TipoId);

            HasMany<Persona>(tipo => tipo.PersonasTipoEstadoCivil)
                .WithRequired(persona => persona.TipoEstadoCivil)
                .HasForeignKey(persona => persona.TipoEstadoCivilId)
                .WillCascadeOnDelete(false);

            HasMany<Persona>(tipo => tipo.PersonasTipoProfesion)
               .WithRequired(persona => persona.TipoProfesion)
               .HasForeignKey(persona => persona.TipoProfesionId)
               .WillCascadeOnDelete(false);

            HasMany<EmpresaContactoWeb>(empresaContactoWeb => empresaContactoWeb.EmpresaContactosWeb)
              .WithRequired(empresaContactoWeb => empresaContactoWeb.TipoRedEmpresaContactoWeb)
              .HasForeignKey(empresaContactoWeb => empresaContactoWeb.TipoRedId);

            HasMany<InfoTributaria>(infoTributaria => infoTributaria.InfoTributarias)
              .WithOptional(infoTributaria => infoTributaria.TipoValor)
              .HasForeignKey(infoTributaria => infoTributaria.TipoValorId);

            HasMany<Empresa>(empresa => empresa.Empresas)
              .WithRequired(empresa => empresa.TipoNaturaleza)
              .HasForeignKey(empresa => empresa.TipoNaturalezaId);

            HasMany<PersonaTelefono>(tipo => tipo.PersonaTelefonoTipoUbicacion)
              .WithRequired(PersonaTelefono => PersonaTelefono.TipoUbicacion)
              .HasForeignKey(PersonaTelefono => PersonaTelefono.TipoUbicacionId)
              .WillCascadeOnDelete(false);

            HasMany<PersonaDireccion>(tipo => tipo.PersonaDireccionTipoUbicacion)
             .WithRequired(PersonaDireccion => PersonaDireccion.TipoUbicacion)
             .HasForeignKey(PersonaDireccion => PersonaDireccion.TipoUbicacionId)
             .WillCascadeOnDelete(false);

            HasMany<PersonaContactoWeb>(tipo => tipo.PersonaContactoWebTipoRed)
             .WithRequired(PersonaContactoWeb => PersonaContactoWeb.TipoPersonaContactoWeb)
             .HasForeignKey(PersonaContactoWeb => PersonaContactoWeb.TipoId)
             .WillCascadeOnDelete(false);

            HasMany<EmpresaContacto>(empresaContacto => empresaContacto.EmpresaContactos)
              .WithRequired(empresaContacto => empresaContacto.TipoAreaEmpresaContacto)
              .HasForeignKey(empresaContacto => empresaContacto.TipoAreaEmpresaId)
              .WillCascadeOnDelete(false);

            HasMany<Sucursal>(sucursal => sucursal.Sucursales)
              .WithRequired(sucursal => sucursal.SucursalTipo)
              .HasForeignKey(sucursal => sucursal.TipoId)
              .WillCascadeOnDelete(false);

            HasMany<ZonaEmpleado>(tipo => tipo.TiposZonaEmpleado)
             .WithRequired(zonaEmpleado => zonaEmpleado.TipoZonaEmpleado)
             .HasForeignKey(zonaEmpleado => zonaEmpleado.TipoId)
             .WillCascadeOnDelete(false);

            //Tabla
            ToTable("tipo");
        }
    }
}
