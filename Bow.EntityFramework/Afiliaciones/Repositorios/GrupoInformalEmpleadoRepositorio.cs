using Bow.EntityFramework.Repositories;
using Bow.Afiliaciones.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.EntityFramework;
using Bow.EntityFramework;
using System.Data.Entity;

namespace Bow.Afiliaciones.Repositorios
{
    public class GrupoInformalEmpleadoRepositorio : BowRepositoryBase<GrupoInformalEmpleado>, IGrupoInformalEmpleadoRepositorio
    {
        public GrupoInformalEmpleadoRepositorio(IDbContextProvider<BowDbContext> dbContextProvider)
           : base(dbContextProvider)
        {

        }

        public List<GrupoInformalEmpleado> GetAllEmpleadosByGrupoInformal(int GrupoInformalId)
        {
            return GetAll().Where(b => b.GrupoInformalId == GrupoInformalId).Include(to => to.GrupoInformalEmpleadoEmpleado.PersonaEmpleado).Include(to => to.GrupoInformalEmpleadoEmpleado.SucursalEmpleado.SucursalDireccion.BarrioDireccion.Localidad).ToList();
        }
    }
}
