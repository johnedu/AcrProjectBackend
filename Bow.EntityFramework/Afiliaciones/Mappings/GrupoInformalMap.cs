using Bow.Afiliaciones.Entidades;
using Bow.Cartera.Entidades;
using Bow.MappingsBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Afiliaciones.Mappings
{
    public class GrupoInformalMap: MultiTenantMap<GrupoInformal>
    {
        public GrupoInformalMap()
        {
            //Atributos
            Property(gr => gr.Nombre).HasMaxLength(100);
            Property(gr => gr.Nombre).IsRequired();

            Property(gr => gr.FechaIngreso).IsRequired();

            Property(gr => gr.PorcentajeDescuento).IsRequired();

            Property(gr => gr.EncargadoExento).IsRequired();

            //Llaves Foraneas
            HasMany<GrupoInformalEmpleado>(gr => gr.GrupoInformalEmpleadoGrupoInformal)
                .WithRequired(gr => gr.GrupoInformalEmpleadoGrupoInformal)
                .HasForeignKey(gr => gr.GrupoInformalId)
                .WillCascadeOnDelete(false);

            //Tabla
            ToTable("grupo_informal");
        }
    }
}