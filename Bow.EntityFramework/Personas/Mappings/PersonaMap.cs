using Bow.Afiliaciones.Entidades;
using Bow.Empleados.Entidades;
using Bow.Empresas.Entidades;
using Bow.MappingsBase;
using Bow.Personas.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Personas.Mappings
{
    public class PersonaMap : MultiTenantMap<Persona>
    {
        public PersonaMap()
        {
            //Atributos
            Property(persona => persona.TieneDocumento).IsRequired();

            Property(persona => persona.NumeroDocumento).HasMaxLength(30);
            Property(persona => persona.NumeroDocumento).IsOptional();

            Property(persona => persona.FechaExpDocumento).IsOptional();

            Property(persona => persona.Nombre).HasMaxLength(50);
            Property(persona => persona.Nombre).IsRequired();

            Property(persona => persona.Apellido1).HasMaxLength(50);
            Property(persona => persona.Apellido1).IsRequired();

            Property(persona => persona.Apellido2).HasMaxLength(50);
            Property(persona => persona.Apellido2).IsOptional();

            Property(persona => persona.TieneFechaNacimiento).IsRequired();

            Property(persona => persona.FechaNacimiento).IsOptional();

            Property(persona => persona.Genero).HasMaxLength(1);
            Property(persona => persona.Genero).IsRequired();

            Property(persona => persona.CorreoElectronico).HasMaxLength(80);

            Property(persona => persona.ContactarCorreo).IsRequired();

            Property(persona => persona.ContactarTelefono).IsRequired();

            Property(persona => persona.FechaIngreso).IsRequired();

            Property(persona => persona.Discapacitada).IsRequired();

            Property(persona => persona.FechaFallecimiento).IsOptional();

            Property(persona => persona.FechaUltActualizacion).IsRequired();

            Property(persona => persona.Usuario).HasMaxLength(10);
            Property(persona => persona.TieneFechaNacimiento).IsOptional();

            Ignore(persona => persona.edad);

            ////Llaves Foráneas
            HasMany<Empresa>(empresa => empresa.Empresas)
              .WithOptional(empresa => empresa.Persona)
              .HasForeignKey(empresa => empresa.PersonaId)
              .WillCascadeOnDelete(false);

            //Llaves Foraneas
            HasMany<PersonaTelefono>(persona => persona.PersonaTelefonos)
              .WithRequired(PersonaTelefono => PersonaTelefono.PersonaPersonaTelefono)
              .HasForeignKey(PersonaTelefono => PersonaTelefono.PersonaId)
              .WillCascadeOnDelete(false);

            //Llaves Foraneas
            HasMany<PersonaDireccion>(persona => persona.PersonaDirecciones)
              .WithRequired(PersonaDireccion => PersonaDireccion.PersonaPersonaDireccion)
              .HasForeignKey(PersonaDireccion => PersonaDireccion.PersonaId)
              .WillCascadeOnDelete(false);

            HasMany<PersonaContactoWeb>(persona => persona.PersonaContactoWeb)
              .WithRequired(PersonaContactoWeb => PersonaContactoWeb.PersonaPersonaContactoWeb)
              .HasForeignKey(PersonaContactoWeb => PersonaContactoWeb.PersonaId)
              .WillCascadeOnDelete(false);

            HasMany<EmpresaContacto>(personaEmpresaContacto => personaEmpresaContacto.PersonaEmpresaContactos)
              .WithRequired(personaEmpresaContacto => personaEmpresaContacto.PersonaEmpresaContacto)
              .HasForeignKey(personaEmpresaContacto => personaEmpresaContacto.PersonaId)
              .WillCascadeOnDelete(false);

            HasMany<PersonaPreferencia>(persona => persona.PersonaPreferencia)
             .WithRequired(personaOpcionPreferencia => personaOpcionPreferencia.PersonaPersonaPreferencia)
             .HasForeignKey(personaOpcionPreferencia => personaOpcionPreferencia.PersonaId)
             .WillCascadeOnDelete(false);

            HasMany<PersonaAuditoria>(persona => persona.PersonaAuditoria)
             .WithRequired(personaAuditoria => personaAuditoria.PersonaPersonaAuditoria)
             .HasForeignKey(personaAuditoria => personaAuditoria.PersonaId)
             .WillCascadeOnDelete(false);

            HasMany<Empleado>(persona => persona.PersonaEmpleado)
             .WithRequired(personaEmpleado => personaEmpleado.PersonaEmpleado)
             .HasForeignKey(personaEmpleado => personaEmpleado.PersonaId)
             .WillCascadeOnDelete(false);

            HasMany<GestionProspecto>(persona => persona.PersonaGestionProspecto)
             .WithOptional(gestionprospecto => gestionprospecto.Persona)
             .HasForeignKey(gestionprospecto => gestionprospecto.PersonaId)
             .WillCascadeOnDelete(false);

            HasMany<GrupoInformal>(persona => persona.PersonaGrupoInformal)
             .WithRequired(gr => gr.PersonaGrupoInformal)
             .HasForeignKey(gr => gr.PersonaId)
             .WillCascadeOnDelete(false);

            //Tabla
            ToTable("persona");
        }
    }
}