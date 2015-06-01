namespace Bow.Migrations {
    using Bow.Administracion.Entidades;
    using Bow.Migrations.Data;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Bow.EntityFramework.BowDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "AcrProject";
        }

        protected override void Seed(Bow.EntityFramework.BowDbContext context)
        {
            //cargarJuegos(context);
            //cargarDimensiones(context);
            cargarTiposUsuarios(context);

            new InitialDataBuilder().Build(context);
        }

        /// /////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Cargar los diferentes juegos de la App
        /// </summary>
        /// <param name="context"></param>
        /// //////////////////////////////////////////////////////////////////////////////
        private void cargarJuegos(Bow.EntityFramework.BowDbContext context)
        {
            context.Set<Juego>().AddOrUpdate(p => p.Nombre, new Juego { Nombre = BowConsts.JUEGO_MILLONARIO });
            context.Set<Juego>().AddOrUpdate(p => p.Nombre, new Juego { Nombre = BowConsts.JUEGO_FALSO_VERDADERO });
            context.Set<Juego>().AddOrUpdate(p => p.Nombre, new Juego { Nombre = BowConsts.JUEGO_PASAPALABRAS });
        }

        /// /////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Cargar las diferentes dimensiones de la App
        /// </summary>
        /// <param name="context"></param>
        /// //////////////////////////////////////////////////////////////////////////////
        private void cargarDimensiones(Bow.EntityFramework.BowDbContext context)
        {
            context.Set<Dimension>().AddOrUpdate(p => p.Nombre, new Dimension { Nombre = BowConsts.DIMENSION_PERSONAL });
            context.Set<Dimension>().AddOrUpdate(p => p.Nombre, new Dimension { Nombre = BowConsts.DIMENSION_PRODUCTIVA });
            context.Set<Dimension>().AddOrUpdate(p => p.Nombre, new Dimension { Nombre = BowConsts.DIMENSION_FAMILIAR });
            context.Set<Dimension>().AddOrUpdate(p => p.Nombre, new Dimension { Nombre = BowConsts.DIMENSION_HABITABILIDAD });
            context.Set<Dimension>().AddOrUpdate(p => p.Nombre, new Dimension { Nombre = BowConsts.DIMENSION_SALUD });
            context.Set<Dimension>().AddOrUpdate(p => p.Nombre, new Dimension { Nombre = BowConsts.DIMENSION_EDUCATIVA });
            context.Set<Dimension>().AddOrUpdate(p => p.Nombre, new Dimension { Nombre = BowConsts.DIMENSION_CIUDADANA });
            context.Set<Dimension>().AddOrUpdate(p => p.Nombre, new Dimension { Nombre = BowConsts.DIMENSION_SEGURIDAD });
        }

        /// /////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Cargar los tipos de usuario
        /// </summary>
        /// <param name="context"></param>
        /// //////////////////////////////////////////////////////////////////////////////
        private void cargarTiposUsuarios(Bow.EntityFramework.BowDbContext context)
        {
            context.Set<Tipo>().AddOrUpdate(p => p.Nombre, new Tipo { Nombre = BowConsts.TIPO_USUARIO_PPR });
            context.Set<Tipo>().AddOrUpdate(p => p.Nombre, new Tipo { Nombre = BowConsts.TIPO_USUARIO_PROFESIONAL });
        }
    }
}
