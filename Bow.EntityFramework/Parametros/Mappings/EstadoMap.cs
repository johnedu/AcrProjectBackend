using Bow.Afiliaciones.Entidades;
using Bow.Empleados.Entidades;
using Bow.Empresas.Entidades;
using Bow.MappingsBase;
using Bow.Parametros.Entidades;
using Bow.Personas.Entidades;
using Bow.Zonificacion.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Parametros.Mappings
{
    public class EstadoMap : MultiTenantMap<Estado>
    {
        public EstadoMap()
        {
            //Atributos
            Property(estado => estado.Motivo).HasMaxLength(50);
            Property(estado => estado.Motivo).IsRequired();

            //Llaves Foráneas
            HasMany<Preferencia>(estado => estado.Preferencias)
                .WithRequired(estado => estado.EstadoPreferencia)
                .HasForeignKey(estado => estado.EstadoId)
                .WillCascadeOnDelete(false);

            HasMany<InfoTributaria>(estado => estado.InfoTributarias)
             .WithRequired(infoTributaria => infoTributaria.Estado)
             .HasForeignKey(infoTributaria => infoTributaria.EstadoId);

            HasMany<PersonaTelefono>(estado => estado.EstadosPersonaTelefono)
              .WithRequired(PersonaTelefono => PersonaTelefono.Estado)
              .HasForeignKey(PersonaTelefono => PersonaTelefono.EstadoId)
              .WillCascadeOnDelete(false);

            HasMany<PersonaDireccion>(estado => estado.EstadosPersonaDireccion)
             .WithRequired(PersonaTelefono => PersonaTelefono.Estado)
             .HasForeignKey(PersonaTelefono => PersonaTelefono.EstadoId)
             .WillCascadeOnDelete(false);

            HasMany<EmpresaOrganizacion>(estado => estado.EstadosEmpresaOrganizacion)
              .WithRequired(estado => estado.EstadoEmpresaOrganizacion)
              .HasForeignKey(estado => estado.EstadoId)
              .WillCascadeOnDelete(false);

            HasMany<Sucursal>(estado => estado.Sucursales)
              .WithRequired(sucursal => sucursal.SucursalEstado)
              .HasForeignKey(sucursal => sucursal.EstadoId)
              .WillCascadeOnDelete(false);

            HasMany<Empleado>(estado => estado.EstadosEmpleado)
             .WithRequired(Empleado => Empleado.EstadoEmpleado)
             .HasForeignKey(Empleado => Empleado.EstadoId)
             .WillCascadeOnDelete(false);

            HasMany<ZonaEmpleado>(estado => estado.EstadoZonaEmpleado)
            .WithRequired(zonaEmpleado => zonaEmpleado.EstadoZonaEmpleado)
            .HasForeignKey(zonaEmpleado => zonaEmpleado.EstadoId)
            .WillCascadeOnDelete(false);

            HasMany<PlanExequial>(estado => estado.EstadoPlanesExequiales)
            .WithRequired(planExequial => planExequial.EstadoPlanExequial)
            .HasForeignKey(planExequial => planExequial.EstadoId)
            .WillCascadeOnDelete(false);

            HasMany<GrupoFamiliar>(estado => estado.EstadoGruposFamiliares)
              .WithRequired(grupoFamiliar => grupoFamiliar.EstadoGrupoFamiliar)
              .HasForeignKey(grupoFamiliar => grupoFamiliar.EstadoId)
              .WillCascadeOnDelete(false);

            HasMany<BeneficioPlanExequial>(estado => estado.EstadoBeneficioPlanExequial)
              .WithRequired(beneficio => beneficio.EstadoBeneficioPlanExequial)
              .HasForeignKey(beneficio => beneficio.EstadoId)
              .WillCascadeOnDelete(false);

            HasMany<BeneficioAdicionalPlanExequial>(estado => estado.EstadoBeneficiosAdicionalesPlanExequial)
              .WithRequired(beneficio => beneficio.EstadoBeneficioAdicionalPlanExequial)
              .HasForeignKey(beneficio => beneficio.EstadoId)
              .WillCascadeOnDelete(false);

            HasMany<GestionProspecto>(estado => estado.EstadoGestionProspecto)
             .WithOptional(gestionprospecto => gestionprospecto.EstadoNoAfiliacion)
             .HasForeignKey(gestionprospecto => gestionprospecto.EstadoNoAfiliacionId)
             .WillCascadeOnDelete(false);

            HasMany<GrupoInformal>(estado => estado.EstadoGrupoInformal)
             .WithRequired(gr => gr.EstadoGrupoInformal)
             .HasForeignKey(gr => gr.EstadoId)
             .WillCascadeOnDelete(false);

            HasMany<GrupoInformalEmpleado>(estado => estado.EstadoGrupoInformalEmpleado)
                .WithRequired(gr => gr.GrupoInformalEmpleadoEstado)
                .HasForeignKey(gr => gr.EstadoId)
                .WillCascadeOnDelete(false);

            //Tabla
            ToTable("estado");
        }
    }
}
