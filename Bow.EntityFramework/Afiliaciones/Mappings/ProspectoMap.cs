using Bow.Afiliaciones.Entidades;
using Bow.MappingsBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Afiliaciones.Mappings
{
   public  class ProspectoMap: MultiTenantMap<Prospecto>
    {
       public ProspectoMap()
        {
            //Llaves Foraneas
            HasMany<GestionProspecto>(prospecto => prospecto.GestionProspecto)
              .WithRequired(gestionprospecto => gestionprospecto.Prospecto)
              .HasForeignKey(gestionprospecto => gestionprospecto.ProspectoId)
              .WillCascadeOnDelete(false);

            //Tabla
            ToTable("prospecto");
        }
    }
}