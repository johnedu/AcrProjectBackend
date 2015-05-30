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
    public class EmpresaContactoRepositorio : BowRepositoryBase<EmpresaContacto>, IEmpresaContactoRepositorio
    {
        public EmpresaContactoRepositorio(IDbContextProvider<BowDbContext> dbContextProvider)
           : base(dbContextProvider)
        {

        }

        public List<EmpresaContacto> GetAllContactosEmpresaWithTipo(int EmpresaId)
        {
            return GetAll().Where(emp => emp.EmpresaId == EmpresaId).Include(emp => emp.TipoAreaEmpresaContacto).Include(per => per.PersonaEmpresaContacto).OrderBy(emp => emp.TipoAreaEmpresaContacto.Nombre).ToList();
        }
    }
}
