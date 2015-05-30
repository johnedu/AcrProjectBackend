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
    public class PreguntaRepositorio : BowRepositoryBase<Pregunta>, IPreguntaRepositorio
    {
        public PreguntaRepositorio(IDbContextProvider<BowDbContext> dbContextProvider)
           : base(dbContextProvider)
        {

        }

        public List<Pregunta> GetAllWithJuego(int DimensionId, int JuegoId)
        {
            return GetAll().Where(p => p.DimensionId == DimensionId && p.JuegoId == JuegoId).Include(p => p.JuegoPregunta).OrderByDescending(p => p.Texto).ToList();
        }
    }
}
