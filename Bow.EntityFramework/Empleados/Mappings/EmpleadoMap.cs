using Bow.Afiliaciones.Entidades;
using Bow.Empleados.Entidades;
using Bow.MappingsBase;
using Bow.Zonificacion.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Empleados.Mappings
{
    public class EmpleadoMap : MultiTenantMap<Empleado>
    {
        public EmpleadoMap()
        {
            //Atributos
            Property(empleado => empleado.Codigo).IsRequired();

            //Llaves Foraneas
            HasMany<ZonaEmpleado>(empleado => empleado.ZonasEmpleado)
                .WithRequired(zonaEmpleado => zonaEmpleado.EmpleadoZonaEmpleado)
                .HasForeignKey(zonaEmpleado => zonaEmpleado.EmpleadoId)
                .WillCascadeOnDelete(false);

            HasMany<GestionProspecto>(empleado => empleado.EmpleadoGestionProspecto)
                .WithRequired(gestionprospecto => gestionprospecto.Empleado)
                .HasForeignKey(gestionprospecto => gestionprospecto.EmpleadoId)
                .WillCascadeOnDelete(false);

            HasMany<GrupoInformalEmpleado>(empleado => empleado.EmpleadoGrupoInformalEmpleado)
                .WithRequired(gr => gr.GrupoInformalEmpleadoEmpleado)
                .HasForeignKey(gr => gr.EmpleadoId)
                .WillCascadeOnDelete(false);

            //Tabla
            ToTable("empleado");
        }
    }
}
