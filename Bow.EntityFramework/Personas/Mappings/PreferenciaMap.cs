using Bow.MappingsBase;
using Bow.Personas.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Personas.Mappings
{
    public class PreferenciaMap : MultiTenantMap<Preferencia>
    { 
        public PreferenciaMap()
        {
            //Atributos
            Property(preferencia => preferencia.Nombre).HasMaxLength(80);

            //llaves foraneas
            HasMany<OpcionPreferencia>(preferencia => preferencia.OpcionesPreferencias)
                           .WithRequired(opcionPreferencia => opcionPreferencia.PreferenciaOpcion)
                           .HasForeignKey(opcionPreferencia => opcionPreferencia.PreferenciaId)
                           .WillCascadeOnDelete(false);           

            //Tabla
            ToTable("preferencia");
        }
         
    }
}
