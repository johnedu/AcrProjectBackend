using Bow.Empresas.Entidades;
using Bow.MappingsBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Empresas.Mappings
{
    public class InfoTributariaOpcionMap: MultiTenantMap<InfoTributariaOpcion>
    {
        public InfoTributariaOpcionMap()
        {
            //Atributos
            Property(infoTributaria => infoTributaria.Nombre).HasMaxLength(50);
            Property(infoTributaria => infoTributaria.Nombre).IsRequired();

            //Llaves Foraneas

            HasMany<EmpresaInfoTributaria>(empresaInfoTributaria => empresaInfoTributaria.InfoTributariaOpcionEmpresas)
              .WithRequired(empresaInfoTributaria => empresaInfoTributaria.InfoTributariaOpcionEmpresaInfoTributaria)
              .HasForeignKey(empresaInfoTributaria => empresaInfoTributaria.InfoTributariaOpcionId)
              .WillCascadeOnDelete(false);

            //Tabla
            ToTable("info_tributaria_opcion");
        }
    }
}