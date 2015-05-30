using Bow.Empresas.Entidades;
using Bow.MappingsBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Empresas.Mappings
{
    public class InfoTributariaMap: MultiTenantMap<InfoTributaria>
    {
        public InfoTributariaMap()
        {
            //Atributos
            Property(infoTributaria => infoTributaria.Nombre).HasMaxLength(100);
            Property(infoTributaria => infoTributaria.Nombre).IsRequired();

            //Llaves Foraneas
            HasMany<InfoTributariaOpcion>(infoTributariaOpcion => infoTributariaOpcion.InfoTributariaOpciones)
              .WithRequired(infoTributariaOpcion => infoTributariaOpcion.InfoTributaria)
              .HasForeignKey(infoTributariaOpcion => infoTributariaOpcion.InfoTributariaId)
              .WillCascadeOnDelete(false);

            HasMany<InfoTributariaLocalidad>(infoTributariaLocalidad => infoTributariaLocalidad.InfoTributariaLocalidades)
              .WithRequired(infoTributariaLocalidad => infoTributariaLocalidad.InfoTributaria)
              .HasForeignKey(infoTributariaLocalidad => infoTributariaLocalidad.InfoTributariaId)
              .WillCascadeOnDelete(false);

            //Tabla
            ToTable("info_tributaria");
        }
    }
}