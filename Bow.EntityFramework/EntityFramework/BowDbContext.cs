using Abp.EntityFramework;
using Abp.Zero.EntityFramework;
using Bow.Afiliaciones.Mappings;
using Bow.Cartera.Mappings;
using Bow.Empleados.Mappings;
using Bow.Empresas.Mappings;
using Bow.Parametros.Mappings;
using Bow.Personas.Mappings;
using Bow.Seguridad;
using Bow.Seguridad.Autorizacion;
using Bow.Seguridad.MultiTenancy;
using Bow.Seguridad.Usuarios;
using Bow.Zonificacion.Mappings;

namespace Bow.EntityFramework
{
    public class BowDbContext : AbpZeroDbContext<Tenant, Role, User>
    {
        //TODO: Define an IDbSet for each Entity...

        //Example:
        //public virtual IDbSet<User> Users { get; set; }

        /* NOTE: 
         *   Setting "Default" to base class helps us when working migration commands on Package Manager Console.
         *   But it may cause problems when working Migrate.exe of EF. If you will apply migrations on command line, do not
         *   pass connection string name to base classes. ABP works either way.
         */
        public BowDbContext()
            : base("Default")
        {

        }

        /* NOTE:
         *   This constructor is used by ABP to pass connection string defined in BowDataModule.PreInitialize.
         *   Notice that, actually you will not directly create an instance of BowDbContext since ABP automatically handles it.
         */
        public BowDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }

        protected override void OnModelCreating(System.Data.Entity.DbModelBuilder modelBuilder)
        {

            modelBuilder.HasDefaultSchema("bow");
            modelBuilder.Configurations.Add(new PaisMap());
            modelBuilder.Configurations.Add(new DepartamentoMap());
            modelBuilder.Configurations.Add(new LocalidadMap());
            modelBuilder.Configurations.Add(new ZonaMap());
            modelBuilder.Configurations.Add(new BarrioMap());
            modelBuilder.Configurations.Add(new ManzanaMap());
            modelBuilder.Configurations.Add(new DireccionMap());
            modelBuilder.Configurations.Add(new AvenidaMap());
            modelBuilder.Configurations.Add(new SufijoLocalidadMap());
            modelBuilder.Configurations.Add(new SufijoMap());
            modelBuilder.Configurations.Add(new TelefonoMap());
            modelBuilder.Configurations.Add(new TipoOrientacionMap());
            modelBuilder.Configurations.Add(new TorieLocalidadMap());
            modelBuilder.Configurations.Add(new ParametroMap());
            modelBuilder.Configurations.Add(new TipoMap());
            modelBuilder.Configurations.Add(new EstadoMap());
            modelBuilder.Configurations.Add(new NombreEstadoMap());
            modelBuilder.Configurations.Add(new PreferenciaMap());
            modelBuilder.Configurations.Add(new OpcionPreferenciaMap());
            modelBuilder.Configurations.Add(new PersonaMap());
            modelBuilder.Configurations.Add(new TipoDocumentoPersonaMap());
            modelBuilder.Configurations.Add(new EmpresaMap());
            modelBuilder.Configurations.Add(new EmpresaTelefonoMap());
            modelBuilder.Configurations.Add(new EmpresaContactoMap());
            modelBuilder.Configurations.Add(new EmpresaContactoWebMap());
            modelBuilder.Configurations.Add(new InfoTributariaMap());
            modelBuilder.Configurations.Add(new InfoTributariaOpcionMap());
            modelBuilder.Configurations.Add(new InfoTributariaLocalidadMap());
            modelBuilder.Configurations.Add(new OrganizacionMap());
            modelBuilder.Configurations.Add(new EmpresaOrganizacionMap());
            modelBuilder.Configurations.Add(new ActividadEconomicaMap());
            modelBuilder.Configurations.Add(new PersonaTelefonoMap());
            modelBuilder.Configurations.Add(new ZonaBarrioMap());
            modelBuilder.Configurations.Add(new PersonaDireccionMap());
            modelBuilder.Configurations.Add(new PersonaContactoWebMap());
            modelBuilder.Configurations.Add(new EmpresaInfoTributariaMap());
            modelBuilder.Configurations.Add(new PersonaPreferenciaMap());
            modelBuilder.Configurations.Add(new PersonaAuditoriaMap());
            modelBuilder.Configurations.Add(new SucursalMap());
            modelBuilder.Configurations.Add(new SucursalTelefonoMap());
            modelBuilder.Configurations.Add(new EmpleadoMap());
            modelBuilder.Configurations.Add(new ZonaEmpleadoMap());
            modelBuilder.Configurations.Add(new MonedaMap());
            modelBuilder.Configurations.Add(new PlanExequialMap());
            modelBuilder.Configurations.Add(new BeneficioMap());
            modelBuilder.Configurations.Add(new GrupoFamiliarMap());
            modelBuilder.Configurations.Add(new ParentescoMap());
            modelBuilder.Configurations.Add(new GrupoFamiliarParentescoMap());
            modelBuilder.Configurations.Add(new GrupoParentescoRangoMap());
            modelBuilder.Configurations.Add(new PeriodoVentaMap());
            modelBuilder.Configurations.Add(new BeneficioPlanExequialMap());
            modelBuilder.Configurations.Add(new BeneficioAdicionalPlanExequialMap());
            modelBuilder.Configurations.Add(new PlanExequialSucursalMap());
            modelBuilder.Configurations.Add(new RecaudoMasivoMap());
            modelBuilder.Configurations.Add(new RecaudoMasivoCoberturaMap());
            modelBuilder.Configurations.Add(new PlanExequialRecaudoMasivoMap());
            modelBuilder.Configurations.Add(new ProspectoMap());
            modelBuilder.Configurations.Add(new AfiliadoProspectoMap());
            modelBuilder.Configurations.Add(new BeneficiosGestionProspectoMap());
            modelBuilder.Configurations.Add(new FunerariaProspectoMap());
            modelBuilder.Configurations.Add(new GestionProspectoMap());
            modelBuilder.Configurations.Add(new GrupoInformalMap());
            modelBuilder.Configurations.Add(new GrupoInformalEmpleadoMap());
            modelBuilder.Configurations.Add(new TenantMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}
