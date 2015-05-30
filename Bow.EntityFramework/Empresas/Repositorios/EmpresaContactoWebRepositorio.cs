using Bow.EntityFramework.Repositories;
using Bow.Empresas.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.EntityFramework;
using Bow.EntityFramework;
using System.Data.Entity;

namespace Bow.Empresas.Repositorios
{
    public class EmpresaContactoWebRepositorio : BowRepositoryBase<EmpresaContactoWeb>, IEmpresaContactoWebRepositorio
    {
        public EmpresaContactoWebRepositorio(IDbContextProvider<BowDbContext> dbContextProvider)
           : base(dbContextProvider)
        {

        }

        public List<EmpresaContactoWeb> GetAllContactosWebWithTipo(int EmpresaId)
        {
            return GetAll().Where(emp => emp.EmpresaId == EmpresaId).Include(emp => emp.TipoRedEmpresaContactoWeb).OrderBy(emp => emp.TipoRedEmpresaContactoWeb.Nombre).ToList();
        }
    }
}
