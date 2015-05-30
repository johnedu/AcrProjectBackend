using Bow.EntityFramework.Repositories;
using Bow.Empresas.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Abp.EntityFramework;
using Bow.EntityFramework;

namespace Bow.Empresas.Repositorios
{
    public class EmpresaInfoTributariaRepositorio : BowRepositoryBase<EmpresaInfoTributaria>, IEmpresaInfoTributariaRepositorio
    {
        public EmpresaInfoTributariaRepositorio(IDbContextProvider<BowDbContext> dbContextProvider)
           : base(dbContextProvider)
        {

        }

        public List<InfoTributaria> GetAllInfoTributariaByEmpresa(int EmpresaId)
        {
            return GetAll().Where(inf => inf.EmpresaId == EmpresaId && inf.FechaFin == null).Include(inf => inf.InfoTributariaOpcionEmpresaInfoTributaria.InfoTributaria).Select(inf => inf.InfoTributariaOpcionEmpresaInfoTributaria.InfoTributaria).ToList();
        }

        public List<EmpresaInfoTributaria> GetAllOpcionesInfoTributariaByEmpresa(int EmpresaId)
        {
            return GetAll().Where(inf => inf.EmpresaId == EmpresaId).Include(inf => inf.InfoTributariaOpcionEmpresaInfoTributaria.InfoTributaria).ToList();
        }
    }
}
