using Abp.EntityFramework;
using Abp.Zero.EntityFramework;
using Bow.Administracion.Mappings;
using Bow.Seguridad;
using Bow.Seguridad.Autorizacion;
using Bow.Seguridad.MultiTenancy;
using Bow.Seguridad.Usuarios;

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

            modelBuilder.HasDefaultSchema("AcrProject");
            modelBuilder.Configurations.Add(new PreguntaFrecuenteMap());
            modelBuilder.Configurations.Add(new JuegoMap());
            modelBuilder.Configurations.Add(new DimensionMap());
            modelBuilder.Configurations.Add(new TipoMap());
            modelBuilder.Configurations.Add(new UsuarioMap());
            modelBuilder.Configurations.Add(new PreguntaMap());
            modelBuilder.Configurations.Add(new MensajeMap());
            modelBuilder.Configurations.Add(new PuntajeMap());
            modelBuilder.Configurations.Add(new RespuestaMap());
            modelBuilder.Configurations.Add(new EntidadMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}
