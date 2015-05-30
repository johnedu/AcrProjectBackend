using Bow.EntityFramework.Repositories;
using Bow.Personas.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Abp.EntityFramework;
using Bow.EntityFramework;

namespace Bow.Personas.Repositorios
{
    public class PreferenciaRepositorio : BowRepositoryBase<Preferencia>, IPreferenciaRepositorio
    {
        public PreferenciaRepositorio(IDbContextProvider<BowDbContext> dbContextProvider)
           : base(dbContextProvider)
        {

        }

        public List<Preferencia> GetAllListWithOpcionPreferenciaByPreferencia()
        {
            return GetAll().Include(pre => pre.OpcionesPreferencias).ToList();
        }

        public List<Preferencia> GetAllListWithNombreEstado()
        {
            return GetAll().Include(pre => pre.EstadoPreferencia.EstadoNombreEstado).ToList();
        }
       
    }
}
