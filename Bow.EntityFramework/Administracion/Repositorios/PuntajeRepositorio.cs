using Abp.EntityFramework;
using Bow.EntityFramework;
using Bow.EntityFramework.Repositories;
using Bow.Administracion.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Bow.Administracion.Repositorios
{
    public class PuntajeRepositorio : BowRepositoryBase<Puntaje>, IPuntajeRepositorio
    {
        public PuntajeRepositorio(IDbContextProvider<BowDbContext> dbContextProvider)
           : base(dbContextProvider)
        {

        }

        public List<Puntaje> GetAllHistorialPuntajesByUsuario(string Usuario)
        {
            return GetAll().Where(p => p.UsuarioPuntaje.Coda == Usuario).Include(p => p.PreguntaPuntaje.DimensionPregunta).Include(p => p.PreguntaPuntaje.JuegoPregunta).Take(10).OrderBy(p => p.Id).ToList();

        }

    }
}
