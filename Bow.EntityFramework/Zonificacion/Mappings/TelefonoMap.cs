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
    public class TelefonoMap : MultiTenantMap<Telefono>
    {
        public TelefonoMap()
        {
            //Atributos
            Property(telefono => telefono.Numero).HasMaxLength(15);
            Property(telefono => telefono.Numero).IsRequired();
            Property(telefono => telefono.Extension).HasMaxLength(5);

            //Llaves Foraneas
            HasMany<EmpresaTelefono>(empresaTelefono => empresaTelefono.EmpresaTelefonos)
              .WithRequired(empresaTelefono => empresaTelefono.TelefonoEmpresaTelefono)
              .HasForeignKey(empresaTelefono => empresaTelefono.TelefonoId);

            HasMany<PersonaTelefono>(telefono => telefono.PersonaTelefonos)
              .WithRequired(PersonaTelefono => PersonaTelefono.TelefonoPersonaTelefono)
              .HasForeignKey(PersonaTelefono => PersonaTelefono.TelefonoId)
              .WillCascadeOnDelete(false);

            HasMany<Prospecto>(prospecto => prospecto.TelefonoProspecto)
              .WithOptional(ProspectoTelefono => ProspectoTelefono.Telefono)
              .HasForeignKey(ProspectoTelefono => ProspectoTelefono.TelefonoId)
              .WillCascadeOnDelete(false);

            //Tabla
            ToTable("telefono");
        }
    }
}
